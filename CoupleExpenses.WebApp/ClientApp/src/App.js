import React, { useReducer, useState } from 'react';
import { SelectUser, SelectPassword } from './components/SelectUser';
import { Dashboard } from './components/Dashboard';
import axios from 'axios';
import { Buffer } from 'buffer'

const App = () => {

    const [connectedUser, connectUser] = useReducer(reducer, JSON.parse(localStorage.getItem('connectedUser')));

    return (
        (connectedUser)            
            ? (connectedUser.authKey)
                ? <Dashboard dispatch={connectUser} />
                : <SelectPassword username={connectedUser.Username} onConnected={connectUser} />
            : <SelectUser onSelect={connectUser} />
    );
}

const reducer = (state, authInfo) => {    
    localStorage.setItem('connectedUser', JSON.stringify(authInfo));
    
    //    axios.defaults.headers.common['Authorization'] = Buffer.from(`key:${authInfo.authKey}`).toString('base64');
    return authInfo;
}

export default App;