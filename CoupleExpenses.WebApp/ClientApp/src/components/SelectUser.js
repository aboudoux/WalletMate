import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import {  Button } from 'reactstrap';
import ShadowBox from './ShadowBox'


export const SelectUser = ({onSelect}) => {
    return (
        <ShadowBox message="Qui êtes vous ?">
            <Button outline color="primary" size="lg" block onClick={() => onSelect({ username: 'Marie' })}>
                Marie
            </Button>
            <Button outline color="success" size="lg" block onClick={() => onSelect({ username: 'Aurelien' })}>
                Aurélien
            </Button>
        </ShadowBox>
        );
}

export default SelectUser;


