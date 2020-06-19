import React, { useEffect, useState } from 'react';
import { MoviesClient, UpdateMovieCommand, MovieVm } from '../client';
import { useParams } from 'react-router';
import { toast } from 'react-toastify'
import { MovieForm } from './MovieForm';
import { Loading } from './Loading';


export const MovieEdit = () => {
    const { id } = useParams();
    const client = new MoviesClient();
    const [vm, setVm] = useState<MovieVm>();
    const [loading, setLoading] = useState<boolean>(true);

    const fetch = async () => {
        const vm = await client.get(id);
        setVm(vm)
        setLoading(false)
    }


    useEffect(() => {
        fetch();
    }, []);

    const handleSubmit = async (event: React.FormEvent, js: unknown) => {
        event.preventDefault();
        setLoading(true);
        const command = UpdateMovieCommand.fromJS(js);
        try {
            await client.update(id, command);
            setLoading(false);
            toast.success("Saved successfully");
        } catch (e) {
            toast.error("Something went wrong");
        }
    }


    return (
        <>
            <h1>Edit</h1>
            {loading && <Loading />}
            {vm && <MovieForm vm={vm} handleSubmit={handleSubmit} />}
        </>
    );
}
