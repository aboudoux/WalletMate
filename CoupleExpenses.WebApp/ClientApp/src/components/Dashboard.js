import React, { useEffect, useState, useReducer } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import './dashboard.css';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import AlarmOn from '@material-ui/icons/AlarmOn';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import axios from 'axios';
import { Buffer } from 'buffer'


export const Dashboard = ({ dispatch }) => {

    const [initialized, setInitialized] = useState(false);
    const [state, refreshPeriod] = useState([]);  

    if (!initialized) {
        axios.get("/api/Period/All", {
                headers: {
                    "Authorization": "Guid " + getAuthorizationTokenFromLocalStorage()
                }
            })
            .then(response => refreshPeriod(response.data));
        setInitialized(true);
    }
    
    return (
        <div>
            <MainMenu dispatch={dispatch} />
            {state.map((p) => <PeriodPanel periodName={p} isExpanded={false} />)}

        </div>);
}

const getAuthorizationTokenFromLocalStorage = () => {
    var authInfo = JSON.parse(localStorage.getItem('connectedUser'));
    return Buffer.from(`${authInfo.Username}:${authInfo.authKey}`).toString('base64');
}

const PeriodPanel = ({ periodName, isExpanded }) => {

    const [expandState, onExpend] = useReducer((state) => !state, isExpanded);

    return (
        <ExpansionPanel expanded={expandState} >
            <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} className="period-title" onClick={onExpend} >
                <Grid container direction="row" spacing={16}>
                    <Grid item>
                        <AlarmOn />
                    </Grid>
                    <Grid item>
                        <div>{periodName}</div>
                     </Grid>
                </Grid>
            </ExpansionPanelSummary>
            <ExpansionPanelDetails>
                <PeriodOperations expended={expandState} />
            </ExpansionPanelDetails>
        </ExpansionPanel>
        );
}

const MainMenu = ({dispatch}) => {
    return (
        <div className="root">
            <AppBar position="static">
                <Toolbar>
                    <IconButton className="menu-button" color="inherit" aria-label="Menu">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" color="inherit" className="grow">
                        Gestion des dépenses
                    </Typography>
                    <Button color="inherit" onClick={()=>dispatch(null)}>Login</Button>
                </Toolbar>
            </AppBar>
        </div>
    );
};

const PeriodOperations = ({ expended } ) => {

    const [rows, updateRows] = useState([]);
    const [initialised, setInitialized] = useState(false)

    if (!expended && initialised) {
        setInitialized(false);
    } else if (expended && !initialised) {
        axios.get("/api/Period/Operations")
            .then(response => updateRows(response.data));
        setInitialized(true);
    }
    
    return (
        <Paper >
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Type</TableCell>
                        <TableCell align="right">Binôme</TableCell>
                        <TableCell align="right">Montant</TableCell>
                        <TableCell>Libelle</TableCell>                        
                        <TableCell align="right">Opération</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map(row => (
                        <TableRow key={row.id}>
                            <TableCell component="th" scope="row">
                                {row.type}
                            </TableCell>
                            <TableCell align="right">{row.pair}</TableCell>
                            <TableCell align="right">{row.amount} €</TableCell>
                            <TableCell align="right">{row.label}</TableCell>
                            <TableCell align="right">{row.operationType}</TableCell>                            
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Paper>
    );
};

