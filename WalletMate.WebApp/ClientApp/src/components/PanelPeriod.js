import React, { useReducer, useState } from 'react';

import TableOperations from './TableOperations';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import AlarmOn from '@material-ui/icons/AlarmOn';
import Grid from '@material-ui/core/Grid';
import BottomNavigation from '@material-ui/core/BottomNavigation';
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction';
import LocalAtmIcon from '@material-ui/icons/LocalAtm';
import { connect } from "react-redux";
import { Get } from './Call';
import { openSpendingDialog, openRecipeDialog} from './actions';



function mapDispatchToProps(dispatch) {
    return {
        openSpendingDialog: periodId => dispatch(openSpendingDialog(periodId)),
        openRecipeDialog: periodId => dispatch(openRecipeDialog(periodId)),        
    }
}

const ConnectedPanelPeriod = ({ periodName, periodId, isExpanded,  openSpendingDialog, openRecipeDialog}) =>
{   
    const [expandState, onExpend] = useReducer(expandReducer, isExpanded);
    const [operations, setOperations] = useState([]);
    const [balance, setBalance] = useState({ amountDue:0, by:''});

    const refresh = () => expandReducer(false, { periodId: periodId, setOperations: setOperations, setBalance: setBalance });
    const doExpand = () => onExpend({ periodId: periodId, setOperations: setOperations, setBalance: setBalance });

    return (
        <div>            
            <ExpansionPanel expanded={expandState} >
                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} className="period-title" onClick={() => doExpand()}>
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
                    <TableOperations rows={operations} balance={balance} refresh={refresh} />
                </ExpansionPanelDetails>

                <BottomNavigation
                    showLabels
                >
                    <BottomNavigationAction label="Ajouter une dépense" icon={<LocalAtmIcon color="secondary" />} onClick={() => { openSpendingDialog(periodId); }} />
                    <BottomNavigationAction label="Ajouter une recette" icon={<LocalAtmIcon color="primary" />} onClick={() => { openRecipeDialog(periodId); }} />
                </BottomNavigation>
            </ExpansionPanel>
        </div>
    );       
}


const expandReducer = (state, action) =>
{
    const newState = !state;
    if (newState === true) {
        Get("/api/Operation/All?periodId=" + action.periodId)
            .then(response => action.setOperations(response.data));
        Get("/api/Period/Balance?periodId=" + action.periodId)
            .then(response => action.setBalance(response.data));
    }
    return newState;
}

const PanelPeriod = connect(null, mapDispatchToProps)(ConnectedPanelPeriod);
export default PanelPeriod;