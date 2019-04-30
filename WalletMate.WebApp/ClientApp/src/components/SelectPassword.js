import React from 'react';
import ShadowBox from './ShadowBox'
import { Form, FormGroup, Col, Input, Button } from 'reactstrap';
import axios from 'axios';
import crypto from 'crypto'
import { setConnectedUser } from './actions'
import { connect } from "react-redux";

function mapDispatchToProps(dispatch) {
    return { setConnectedUser: authInfo => dispatch(setConnectedUser(authInfo)) }
}

const ConnectedSelectPassword = ({ username, setConnectedUser }) => {

    function handleSubmit(event) {
        event.preventDefault();

        const data = new FormData(event.target);
        const authenticationInfos = {
            Username: username,
            Password: crypto.createHash('sha1').update(data.get('password')).digest("hex")
        };

        axios.post("/api/Authentication/authenticate", authenticationInfos)
            .then(res => {
                setConnectedUser(res.data);
            }).catch((error) => {
                alert(error.message);
            });
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
                    <Button color="danger" onClick={() => setConnectedUser(null)}>Annuler</Button>
                    <Button type="submit" color="success">Valider</Button>
                </FormGroup>
            </Form>
        </ShadowBox>
    );
};

const SelectPassword = connect(null, mapDispatchToProps)(ConnectedSelectPassword);
export default SelectPassword;