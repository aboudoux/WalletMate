import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { Col, Button, Form, FormGroup,  Input,  } from 'reactstrap';
import './style.css'
import axios from 'axios';
import crypto from 'crypto'



export const SelectUser = ({onSelect}) => {
    return (
        <ShadowBox message="Qui êtes vous ?">
            <Button outline color="primary" size="lg" block onClick={() => onSelect({ Username: 'Marie' })}>
                Marie
            </Button>
            <Button outline color="success" size="lg" block onClick={() => onSelect({ Username: 'Aurelien' })}>
                Aurélien
            </Button>
        </ShadowBox>
        );
}

export const SelectPassword = ({ username, onConnected }) => {

    function handleSubmit(event) {
        event.preventDefault();

        const data = new FormData(event.target);
        const authenticationInfos = {
            Username: username,
            Password: crypto.createHash('sha1').update(data.get('password')).digest("hex")
        };

        axios.post("/api/Authentication/authenticate", authenticationInfos)
            .then(res => {
                onConnected(res.data);
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
                    <Button color="danger" onClick={() => onConnected(null)}>Annuler</Button>
                    <Button type="submit" color="success">Valider</Button>
                
            </FormGroup>
        </Form>
        </ShadowBox>
    );
};

const ShadowBox = ({ message, children }) => {
        return (
            <div className="login-box">
                <h2>{message}</h2>
                {children}
            </div>
        );

}


