import React, { Component } from 'react';
import AppRouter from './AppRouter/AppRouter'

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
    }
    render() {      

        return (
            <div>
               <AppRouter/>
            </div>
        );
    }
}
