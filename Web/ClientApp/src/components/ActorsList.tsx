import React, { useEffect, useState } from 'react';
import { Grid, GridColumn, GridPageChangeEvent, GridFilterChangeEvent } from '@progress/kendo-react-grid';
import { ActorsClient, ActorsDto } from '../client';
import { FilterDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { useDebounce } from 'use-debounce';
import { Loading } from './Loading';


export const ActorsList = () => {

    const [actors, setActors] = useState<ActorsDto[] | undefined>([]);
    const [loading, setLoading] = useState(true);
    const [skip, setSkip] = useState<number>(0);
    const [take, setTake] = useState<number>(10);
    const [total, setTotal] = useState(0);
    const [filter, setFilter] = useState<CompositeFilterDescriptor>();
    const [currentFilter] = useDebounce(filter, 450);
    const client = new ActorsClient();

    const page = async (skipParam: number, takeParam: number, name?: string, surname?: string) => {
        setLoading(true);
        const vm = await client.get(skipParam, takeParam, name, surname);
        setActors(vm.actors);
        setTotal(vm.total);
        setSkip(skipParam);
        setTake(takeParam);
        setLoading(false);
    }



    useEffect(() => {
        const { name, surname } = getParams(currentFilter);
        page(skip, take, name, surname);
    }, [currentFilter]);

    const pageChange = (event: GridPageChangeEvent) => {
        const { name, surname } = getParams(currentFilter);
        page(event.page.skip, event.page.take, name, surname);
    }

    const filterChange = (e: GridFilterChangeEvent) => {
        setFilter(e.filter);
    }




    const renderTable = (actors: ActorsDto[] | undefined) => {
        return (
            <Grid
                data={actors || []}
                skip={skip}
                take={take}
                total={total}
                pageable={true}
                onPageChange={pageChange}
                onFilterChange={filterChange}
                filterable
                filter={filter}
                filterOperators={{
                    'text': [
                        { text: 'grid.filterStartsWithOperator', operator: 'startswith' }
                    ],
                    'numeric': [
                        { text: 'grid.filterEqOperator', operator: 'eq' }
                    ],
                    'date': [
                        { text: 'grid.filterEqOperator', operator: 'eq' }
                    ],
                    'boolean': [
                        { text: 'grid.filterEqOperator', operator: 'eq' }
                    ]
                }}
            >
                <GridColumn field="name" title="Name" />
                <GridColumn field="surname" title="Surname" />
            </Grid>

        );
    }

    return (
        <div>
            {loading && <Loading/>}
            <h1 id="tabelLabel">Actors</h1>
            {renderTable(actors)}
        </div>
    );
}

function getParams(filter: CompositeFilterDescriptor | undefined) {
    const filters = filter?.filters as FilterDescriptor[];
    const name = filters?.find(x => x.field === "name")?.value;
    const surname = filters?.find(x => x.field === "surname")?.value;
    return { name, surname };
}
