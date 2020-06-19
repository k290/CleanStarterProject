import React, { useEffect, useState } from 'react';
import { Location, ActorLookupDto, DirectorLookupDto, MovieDisplayActorDto, MovieDisplayDirectorDto, UpdateMovieCommand, MovieVm } from '../client';
import { EnumHelpers, EnumNameAndValue } from './enumhelpers';
import { Input, NumericTextBox, NumericTextBoxChangeEvent } from '@progress/kendo-react-inputs';
import { DropDownList, DropDownListChangeEvent, MultiSelect, MultiSelectChangeEvent } from '@progress/kendo-react-dropdowns';
import { Button } from '@progress/kendo-react-buttons';


export type MovieFormProps = {
    vm: MovieVm;
    handleSubmit: (e: React.FormEvent, js: unknown) => {};
}
export const MovieForm = (props: MovieFormProps) => {
    const [title, setTitle] = useState<string>();
    const [year, setYear] = useState<number>();
    const [location, setLocation] = useState<EnumNameAndValue<Location>>();
    const [actors, setActors] = useState<MovieDisplayActorDto[]>();
    const [director, setDirector] = useState<MovieDisplayDirectorDto>();
    const [directorLookups, setDirectorLookups] = useState<DirectorLookupDto[]>();
    const [actorLookups, setActorLookups] = useState<ActorLookupDto[]>();
    const [locationLookups, setLocationLookups] = useState<EnumNameAndValue<Location>[]>();

    const map = async () => {
        setTitle(props.vm.movie?.title);
        setYear(props.vm.movie?.year);
        setActors(props.vm.movie?.actors);
        setDirector(props.vm.movie?.director);
        setDirectorLookups(props.vm?.directorLookups);
        setActorLookups(props.vm?.actorLookups);
        let locationLookups = EnumHelpers.getNamesAndValues(Location);
        setLocationLookups(locationLookups);
        setLocation(locationLookups.find(x => x.value == props.vm.movie?.location));
    }


    useEffect(() => {
        map();
    }, [props.vm]);

    const onTitleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTitle(event.target.value);
    }

    const onYearChange = (event: NumericTextBoxChangeEvent) => {
        setYear(event.value || undefined);
    }

    const onDirectorChange = (event: DropDownListChangeEvent) => {
        setDirector(event.value);
    }

    const onLocationChange = (event: DropDownListChangeEvent) => {
        setLocation(event.value);
    }

    const onActorsChange = (event: MultiSelectChangeEvent) => {
        setActors(event.value);
    }


    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (year && director && actors && location) {
            const js = { id: props.vm.movie?.id, title: title, year: year, directorId: director.id, actorIds: actors.map(x => x.id), location: location.value };
            props.handleSubmit(event, js);
        }
    }



    return (
        <>

          <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <Input name="title" label="Title" value={title} required onChange={onTitleChange} />
                </div>
                <div className="form-group">
                    <NumericTextBox name="year" label="Year" value={year} required onChange={onYearChange} />
                </div>
                <div className="form-group">
                    <DropDownList name="location" label="Location" value={location} required onChange={onLocationChange} data={locationLookups} dataItemKey={"value"} textField={"name"} />
                </div>
                <div className="form-group">
                    <DropDownList name="director" label="Director" value={director} required onChange={onDirectorChange} data={directorLookups} dataItemKey={"id"} textField={"fullName"} />
                </div>
                <div className="form-group">
                    <MultiSelect name="actors" label="Actors" value={actors} required onChange={onActorsChange} data={actorLookups} dataItemKey={"id"} textField={"fullName"} />
                </div>


                <div className="k-form-buttons">
                    <Button
                        type={'submit'}
                        primary
                    >
                        Submit
                        </Button>
                </div>
            </form>
        </>
    );
}
