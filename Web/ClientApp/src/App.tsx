import React from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { ActorsList } from './components/ActorsList';
import './custom.css'
import { MovieList } from './components/MovieList';
import { MovieEdit } from './components/MovieEdit';
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from 'react-toastify'
import { MovieCreate } from './components/MovieCreate';

export const App = () => {
    return (
        <Layout>
            <Route exact path="/">
                <MovieList />
            </Route>
            <Route exact path="/actors">
                <ActorsList />
            </Route>
            <Route exact path="/movies/edit/:id">
                <MovieEdit />
            </Route>
            <Route exact path="/movies/create">
                <MovieCreate />
            </Route>
            <ToastContainer />
        </Layout>
    );
}

