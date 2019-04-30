import React, { useState } from 'react';
import SelectUser from './components/SelectUser';
import SelectPassword from './components/SelectPassword';
import Dashboard from './components/Dashboard';
import { GetAnonymous } from './components/Call'
import { setPair } from './components/actions';
import { connect } from "react-redux";

function mapStateToProps(state) {
    return { connectedUser: state.connectedUser };
};

function mapDispatchToProp(dispatch) {
    return {
        setPair: pair => dispatch(setPair(pair))
    };
}

const ConnectedApp = ({ connectedUser, setPair}) => {

    const [initialized, setInitialized] = useState(false);

    if (!initialized) {
        GetAnonymous("/api/Pair/All").then(r => setPair(r.data));
        setInitialized(true);
    }

    return (
        (connectedUser)
            ? (connectedUser.authKey)
                ? <Dashboard />
                : <SelectPassword username={connectedUser.username} />
            : <SelectUser/>
    );
}

const reducer = (state, authInfo) => {    
    localStorage.setItem('connectedUser', JSON.stringify(authInfo));
    return authInfo;
}


const App = connect(mapStateToProps, mapDispatchToProp)(ConnectedApp);
export default App;