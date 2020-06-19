import React, { useEffect, useState } from 'react';
import { Grid, GridColumn, GridPageChangeEvent, GridFilterChangeEvent, GridFilterCellProps, GridCellProps } from '@progress/kendo-react-grid';
import { MoviesClient, MoviesDto, Location, ActorLookupDto, DirectorLookupDto } from '../client';
import { FilterDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { useDebounce } from 'use-debounce';
import { Loading } from './Loading';
import multiDropdownFilterCell, { IDropdownFilter } from './DropDownFilterCell';
import { EnumHelpers, EnumNameAndValue } from './enumhelpers';
import { Link, useHistory } from 'react-router-dom';
import { Button } from '@progress/kendo-react-buttons';
import { AreYouSureDialog } from './AreYouSureDialog';
import { DialogCloseEvent } from '@progress/kendo-react-dialogs';
import { toast } from 'react-toastify';


export const MovieList = () => {
    const [movies, setMovies] = useState<MoviesDto[] | undefined>([]);
    const [actorLookups, setActorLookups] = useState<ActorLookupDto[] | undefined>([]);
    const [directorLookups, setDirectorLookups] = useState<DirectorLookupDto[] | undefined>([]);
    const [loading, setLoading] = useState(true);
    const [dialogVisible, setDialogVisible] = useState(false);
    const [currentMovie, setCurrentMovie] = useState<string>();
    const [skip, setSkip] = useState<number>(0);
    const [take, setTake] = useState<number>(10);
    const [total, setTotal] = useState(0);
    const [filter, setFilter] = useState<CompositeFilterDescriptor>();
    const [currentFilter] = useDebounce(filter, 450);
    const client = new MoviesClient();
    const history = useHistory();





    const page = async (skipParam: number, takeParam: number, directorIds: string[], actorIds: string[], title?: string, year?: number, locationIds?: Location[]) => {
        setLoading(true);
        const vm = await client.getAll(skipParam, takeParam, title, year, locationIds, directorIds, actorIds);
        setMovies(vm.movies);
        setActorLookups(vm.actorLookups);
        setDirectorLookups(vm.directorLookups);
        setTotal(vm.total);
        setSkip(skipParam);
        setTake(takeParam);
        setLoading(false);
    }


    useEffect(() => {
        const { title, year, locationIds, actorIds, directorIds } = getParams(currentFilter);
        page(skip, take, directorIds, actorIds, title, year, locationIds);
    }, [currentFilter]);

    const pageChange = (event: GridPageChangeEvent) => {
        const { title, year, locationIds, actorIds, directorIds } = getParams(currentFilter);
        page(event.page.skip, event.page.take, directorIds, actorIds, title, year, locationIds);
    }

    const filterChange = (e: GridFilterChangeEvent) => {
        setFilter(e.filter);
    }

    const closeDialog = () => {
        setCurrentMovie("")
    }

    const deleteMovie = async () => {
        if (currentMovie) {
            try {
                setLoading(true);
                await client.delete(currentMovie);
                setLoading(false);
                toast.success("Deleted successfully");
            } catch (e) {
                toast.error("Something went wrong");
            }
        }
        closeDialog();
        const { title, year, locationIds, actorIds, directorIds } = getParams(currentFilter);
        page(skip, take, directorIds, actorIds, title, year, locationIds);
    }

    const openDialog = (id: string) => {
        setCurrentMovie(id);
    }


    const EditLinkCell = (props: GridCellProps) => {

        return (
            <td>
                <Link to={`movies/edit/${props.dataItem.id}`}>
                    {props.dataItem.title}
                </Link>
                <span className="k-icon k-i-delete grid-delete-icon" onClick={() => openDialog(props.dataItem.id)}></span>
            </td>
        );
    }


    const LocationCell = (props: GridCellProps) => {
        return (
            <td>
                {EnumHelpers.toString(props.dataItem.location, Location)}
            </td>
        );
    }

    const gotoCreate = () => {
        history.push(`/movies/create/`);
    }


    const renderTable = (movies: MoviesDto[] | undefined) => {
        const actorFilterCell = multiDropdownFilterCell(actorLookups, 'Select Actors', "id", "fullName");
        const directorFilterCell = multiDropdownFilterCell(directorLookups, 'Select Directors', "id", "fullName");
        let locationLookups = EnumHelpers.getNamesAndValues(Location);
        const LocationFilterCell = multiDropdownFilterCell(locationLookups, 'Select Locations', "value", "name");
        return (
            <>
                <Grid
                    data={movies || []}
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
                    <GridColumn field="title" title="Title" cell={EditLinkCell} />
                    <GridColumn field="year" title="Year" />
                    <GridColumn field="location" title="Location" cell={LocationCell} filterCell={LocationFilterCell} />
                    <GridColumn field="actorString" title="Actors" filterCell={actorFilterCell} />
                    <GridColumn field="directorString" title="Directors" filterCell={directorFilterCell} />
                </Grid>
                <div className="grid-bottom-buttons">
                    <Button
                        primary
                        onClick={gotoCreate}
                    >Create</Button>
                </div>
                {currentMovie && <AreYouSureDialog onYes={deleteMovie} onNo={closeDialog} onClose={closeDialog} />}
            </>
        );
    }

    return (
        <div>
            {loading && <Loading />}
            <h1 id="tabelLabel">Movies</h1>
            {renderTable(movies)}
        </div>
    );
}

function getParams(filter: CompositeFilterDescriptor | undefined) {
    const filters = filter?.filters as FilterDescriptor[];
    const title = filters?.find(x => x.field === "title")?.value;
    const year = filters?.find(x => x.field === "year")?.value;
    const locations: EnumNameAndValue<Location>[] = filters?.find(x => x.field === "location")?.value;
    const locationIds = locations?.map(x => x.value) || []
    const actors: ActorLookupDto[] | undefined = filters?.find(x => x.field === "actorString")?.value;
    const actorIds = actors?.map(x => x.id) || [];
    const directors: ActorLookupDto[] | undefined = filters?.find(x => x.field === "directorString")?.value;
    const directorIds = directors?.map(x => x.id) || [];
    return { title, year, locationIds, actorIds, directorIds };
}
