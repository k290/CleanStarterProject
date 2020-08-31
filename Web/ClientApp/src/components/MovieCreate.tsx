import React, { useEffect, useState } from 'react';
import { MoviesClient, MovieModel, ActorsClient, MovieDto, CreateMovieCommand, DirectorsClient } from '../client';
import { useParams, useHistory } from 'react-router';
import { toast } from 'react-toastify'
import { MovieForm } from './MovieForm';
import { Loading } from './Loading';


export const MovieCreate = () => {
    const { id } = useParams();
    const actorsClient = new ActorsClient();
    const directorsClient = new DirectorsClient();
    const moviesClient = new MoviesClient();
    const [vm, setVm] = useState<MovieModel>();
    const history = useHistory();
    const [loading, setLoading] = useState<boolean>(true);

    const fetch = async () => {
        const actorsLookupsVm = actorsClient.getLookups();
        const directorsLookupsVm = directorsClient.getLookups();
        const movieDto = MovieDto.fromJS({ id: undefined, title: "", year: undefined, location: undefined, actors: undefined, director: undefined })
        const initialVm = MovieModel.fromJS({
            actorLookups: (await actorsLookupsVm).actorLookups,
            directorLookups: (await directorsLookupsVm).directorLookups,
            movie: movieDto,

        });
        setVm(initialVm);
        setLoading(false);
    }


    useEffect(() => {
        fetch();
    }, []);

    const handleSubmit = async (event: React.FormEvent, js: unknown) => {
        event.preventDefault();
        setLoading(true);
        const command = CreateMovieCommand.fromJS(js);
        try {
            const id = await moviesClient.create(command);
            setLoading(false);
            toast.success("Saved successfully");
            history.push(`/movies/edit/${id}`);
        } catch (e) {
            toast.error("Something went wrong");
        }
    }



    return (
        <>
            <h1>Create</h1>
            {loading && <Loading />}
            {vm && <MovieForm vm={vm} handleSubmit={handleSubmit} />}
        </>
    );
}
