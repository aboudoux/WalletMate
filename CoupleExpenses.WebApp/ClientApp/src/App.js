import React, { useReducer } from 'react';
import { SelectUser, SelectPassword } from './components/SelectUser';


const App = () => {

    const[connectedUser, dispatch] = useReducer(reducer, null);

    return (
        connectedUser == null
            ? <SelectUser dispatch={dispatch} />
            : <SelectPassword username={connectedUser} dispatch={dispatch} />
    );
}

const reducer = (state, user) => {    
    return user;
}

export default App;