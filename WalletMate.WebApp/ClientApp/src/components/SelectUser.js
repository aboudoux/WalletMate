import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import {  Button } from 'reactstrap';
import ShadowBox from './ShadowBox'
import { selectUsername } from './actions';
import { connect } from "react-redux";


function mapDispatchToProps(dispatch) {
    return { selectUsername: username => dispatch(selectUsername(username)) }
}

function mapStateToProps(state) {
    return { firstPairName: state.firstPairName, secondPairName: state.secondPairName };
};

const ConnectedSelectUser = ({ firstPairName, secondPairName, selectUsername }) => {
  
    return (
        <ShadowBox message="Qui êtes vous ?">
            <Button outline color="primary" size="lg" block onClick={() => selectUsername(firstPairName)}>
                {firstPairName}
            </Button>
            <Button outline color="success" size="lg" block onClick={() => selectUsername(secondPairName)}>
                {secondPairName}
            </Button>
        </ShadowBox>
        );
}

const SelectUser = connect(mapStateToProps, mapDispatchToProps)(ConnectedSelectUser);
export default SelectUser;


