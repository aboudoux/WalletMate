import React, { useReducer, useState } from 'react';
import { SelectUser, SelectPassword } from './components/SelectUser';
import { Dashboard } from './components/Dashboard';


const App = () => {

    const [connectedUser, connectUser] = useReducer(reducer, JSON.parse(localStorage.getItem('connectedUser')));

    return (
        (connectedUser)
            ? (connectedUser.authKey)
                ? <Dashboard dispatch={connectUser} />
                : <SelectPassword username={connectedUser.username} onConnected={connectUser} />
            : <SelectUser onSelect={connectUser} />
    );
}

const reducer = (state, authInfo) => {    
    localStorage.setItem('connectedUser', JSON.stringify(authInfo));
    return authInfo;
}

export default App;