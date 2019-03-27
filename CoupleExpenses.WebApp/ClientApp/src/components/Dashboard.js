import React, { useState, useReducer } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
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
import { Get, Post } from './Call';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import AccountCircle from '@material-ui/icons/AccountCircle';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import BottomNavigation from '@material-ui/core/BottomNavigation';
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction';
import LocalAtmIcon from '@material-ui/icons/LocalAtm';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import InputAdornment from '@material-ui/core/InputAdornment';
import FormControl from '@material-ui/core/FormControl';

export const Dashboard = ({ dispatch }) => {

    const [initialized, setInitialized] = useState(false);
    const [state, refreshPeriod] = useState([]);  

    if (!initialized) {
        Get("/api/Period/All")        
            .then(response => refreshPeriod(response.data))
            .catch((error) => {
                if (error.response.status === 401) {
                    dispatch(null);
                }
            });
        setInitialized(true);
    }
    
    return (
        <div>
            <MainMenu dispatch={dispatch} refreshDashboard={setInitialized} />
            {state.map((p) => <PeriodPanel periodName={p} isExpanded={false} dispatch={dispatch} />)}
           
        </div>);
}

const PeriodPanel = ({ periodName, isExpanded, dispatch }) =>
{
    const [expandState, onExpend] = useReducer((state) => !state, isExpanded);
    const [isSpendingDialogOpen, openSpendingDialog] = useState(false);

    return (
        <div>
            <AddSpendingDialog openState={isSpendingDialogOpen} setOpenState={openSpendingDialog} />
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
                <PeriodOperations expended={expandState} dispatch={dispatch} />
            </ExpansionPanelDetails>

            <BottomNavigation                                
                showLabels                
            >
                    <BottomNavigationAction label="Ajouter une dépense" icon={<LocalAtmIcon color="secondary" />} onClick={() => { openSpendingDialog(true);}} />
                <BottomNavigationAction label="Ajouter une recette" icon={<LocalAtmIcon color="primary" />} />
            </BottomNavigation>
            </ExpansionPanel>
            </div>
        );
}

const MainMenu = ({ dispatch, refreshDashboard }) =>
{
    const menuReducer = (state, action) => {
        var newState = { ...state };

        switch (action.command) {
            case "actionMenu":
                newState.anchorActionMenu = action.target;
                break;
            case "loginMenu":
                newState.anchorLoginMenu = action.target;
                break;
            case "resetAll":            
                newState.anchorActionMenu = null;
                newState.anchorLoginMenu = null;
                break;
            case "openCreatePeriodPopup":
                newState.createPeriodOpen = true;
                break;
            case "closeCreatePeriodPopup":
                newState.createPeriodOpen = false;
                break;
        }
        return newState;
    };

    const initialDashBoardState = { anchorLoginMenu: null, anchorActionMenu: null, createPeriodOpen:false };
    const [dashboardState, doAction] = useReducer(menuReducer, initialDashBoardState);
    

    const handleActionMenu = event => {
        doAction({ target: event.currentTarget, command:"actionMenu" });
    };

    const handleLoginMenu = event => {
        doAction({ target: event.currentTarget, command:"loginMenu" });
    };

    const handleClose = () => {
        doAction({ command: "resetAll" });
    };

    const handleDisconnect = () => {
        doAction({ command: "resetAll" });
        dispatch(null);
    };

    const handleCreatePeriod = () =>        
    {
        doAction({ command: "resetAll" });
        doAction({ command: "openCreatePeriodPopup" });
    }

    return (
        <div className="root">

            <CreatePeriodDialog openState={dashboardState.createPeriodOpen} doAction={doAction} refreshDashboard={refreshDashboard} />
           
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        aria-owns={Boolean(dashboardState.anchorActionMenu) ? 'menu-action' : undefined}
                        className="menu-button"
                        color="inherit"
                        aria-label="Menu"
                        aria-haspopup="true"
                        onClick={handleActionMenu}>
                        <MenuIcon />                        
                    </IconButton>
                    <Menu
                        id="menu-action"
                        anchorEl={dashboardState.anchorActionMenu}                        
                        open={Boolean(dashboardState.anchorActionMenu)}
                        onClose={handleClose}>
                        <MenuItem onClick={handleCreatePeriod}>Créer une période</MenuItem>
                        <MenuItem onClick={handleClose}>Ajouter la prochaine période</MenuItem>
                    </Menu>
                    

                    <Typography variant="h6" color="inherit" className="grow">
                        Gestion des dépenses
                    </Typography>
                    <div>
                        <IconButton
                            aria-owns={Boolean(dashboardState.anchorLoginMenu) ? 'menu-appbar' : undefined}
                            aria-haspopup="true"
                            onClick={handleLoginMenu}
                            color="inherit">                            
                            <AccountCircle />
                        </IconButton>                        
                        <Menu
                            id="menu-appbar"
                            anchorEl={dashboardState.anchorLoginMenu}                            
                            open={Boolean(dashboardState.anchorLoginMenu)}
                            onClose={handleClose}
                        >
                            <MenuItem onClick={handleDisconnect}>Déconnecter</MenuItem>
                        </Menu>
                    </div>
                </Toolbar>
            </AppBar>
        </div>
    );
};

const PeriodOperations = ({ expended, dispatch}) =>
{
    const [rows, updateRows] = useState([]);
    const [initialised, setInitialized] = useState(false);

    if (!expended && initialised) {
        setInitialized(false);
    } else if (expended && !initialised) {
        Get("/api/Period/Operations")
            .then(response => updateRows(response.data))
            .catch((error) => {
                if (error.response.status === 401) {
                    dispatch(null);
                }
            });
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

const CreatePeriodDialog = ({ openState, doAction, refreshDashboard }) =>
{
    const months = [
        "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre","Décembre"
    ];

    let val = 1;
    const ranges = months.map(m => ({ value: val++, label: m }));
    const [selectedMonth, selectMonth] = useState(null);
    const [selectedYear, selectYear] = useState(null);

    return (
        <Dialog
            open={openState}
            onClose={() => doAction({ command: 'closeCreatePeriodPopup' })}
            aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">Créer une période</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Sélectionnez les informations de création pour la nouvelle periode.
                    </DialogContentText>
                <TextField
                    select
                    margin="dense"
                    label="Mois"
                    value={selectedMonth}
                    onChange={(event) => selectMonth(event.target.value)}
                >
                    {ranges.map(option => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                    ))}                
                </TextField>

                <TextField
                    label="Année"
                    margin="dense"
                    value={selectedYear}
                    onChange={(event) => selectYear(event.target.value)}
                >
                </TextField>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => doAction({ command: 'closeCreatePeriodPopup' })} color="primary">
                    Annuler
                    </Button>
                <Button onClick={() => Post("/api/Period/Create", { Month: selectedMonth, Year: selectedYear })
                    .then(() => {
                        doAction({ command: 'closeCreatePeriodPopup' });
                        refreshDashboard(false);
                    })
                    .catch((error) => {
                        if (error.response.status === 401) {
                            alert('error 401');
                        }
                    })}

                    color="primary">
                    Valider
                    </Button>
            </DialogActions>
        </Dialog>
        );
}

const AddSpendingDialog = ({openState, setOpenState}) => {
    return (
        <Dialog
            open={openState}
            aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">Ajouter un dépense</DialogTitle>
            <DialogContent>
                <FormControl fullWidth>
                    <InputLabel htmlFor="adornment-amount">Montant</InputLabel>
                    <Input
                        id="adornment-amount"
                        startAdornment={<InputAdornment position="start">€</InputAdornment>}
                    />
                </FormControl>
                <FormControl fullWidth>
                    <InputLabel htmlFor="adornment-libelle">Libelle</InputLabel>
                    <Input
                        id="adornment-libelle"
                    />
                </FormControl>
              
            </DialogContent>
            <DialogActions>
                <Button color="primary" onClick={() => setOpenState(false)}>
                    Annuler
                    </Button>
                <Button
                    color="primary">
                    Valider
                    </Button>
            </DialogActions>
        </Dialog>);
}