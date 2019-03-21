import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { Col, Button, Form, FormGroup,  Input,  } from 'reactstrap';
import './style.css'



export const SelectUser = ({dispatch}) => {
    return (
        <ShadowBox message="Qui êtes vous ?">
            <Button outline color="primary" size="lg" block onClick={() => dispatch("Marie")}>
                Marie
            </Button>
            <Button outline color="success" size="lg" block onClick={() => dispatch("Aurelien")}>
                Aurélien
            </Button>
        </ShadowBox>
        );
}

export const SelectPassword = ({ username }) => {

    function handleSubmit(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        alert('fetch !');

        fetch('/api/form-submit-url', {
            method: 'POST',
            body: data,
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
                    <Button color="danger">Annuler</Button>
                    <Button color="success">Valider</Button>
                
            </FormGroup>
        </Form>
        </ShadowBox>
    );
};

class ShadowBox extends React.Component {
    render() {
        return (
            <div className="login-box">
                <h2>{this.props.message}</h2>
                {this.props.children}
            </div>
        );
    }
}


