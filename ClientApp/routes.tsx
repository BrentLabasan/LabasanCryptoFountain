import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';

import Fountain from './components/Fountain';
import About from './components/About';



import FetchData from './components/FetchData';
import Counter from './components/Counter';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />

    <Route exact path='/fountain' component={ Fountain } />
    <Route exact path='/about' component={ About } />

    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata/:startDateIndex?' component={ FetchData } />
</Layout>;
