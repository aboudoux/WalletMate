import React from 'react';
import ShadowBox from './ShadowBox'
import { Form, FormGroup, Col, Input, Button } from 'reactstrap';
import { authenticate, resetConnectedUser } from './actions'
import { connect } from "react-redux";

function mapDispatchToProps(dispatch) {
    return {
        authenticate: (login, password) => dispatch(authenticate(login, password)),
        resetConnectedUser: () => dispatch(resetConnectedUser())
    }
}

const ConnectedSelectPassword = ({ username, authenticate, resetConnectedUser }) => {

    function handleSubmit(event)
    {
        event.preventDefault();
        const data = new FormData(event.target);
        authenticate(username, data.get('password'));
    }

    return (
        <ShadowBox message={'Bonjour ' + username}>
            <Form onSubmit={handleSubmit}>
                <FormGroup row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        <Input type="password" name="password" placeholder="Entrez votre mot de passe" style={{ textAlign: 'center', marginTop: '20px' }} />
                    </Col>
                </FormGroup>
                <FormGroup row className="cancel-validate">
                    <Button color="danger" onClick={() => resetConnectedUser()}>Annuler</Button>
                    <Button type="submit" color="success">Valider</Button>
                </FormGroup>
            </Form>
        </ShadowBox>
    );
};

const SelectPassword = connect(null, mapDispatchToProps)(ConnectedSelectPassword);
export default SelectPassword;