import React, { useReducer, useState } from 'react';
import DialogAddSpending from './DialogAddSpending';
import DialogAddRecipe from './DialogAddRecipe';
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
import { Get } from './Call';

const PanelPeriod = ({ periodName, periodId, isExpanded, dispatch }) =>
{   
    const [expandState, onExpend] = useReducer(expandReducer, isExpanded);
    const [isSpendingDialogOpen, openSpendingDialog] = useState(false);
    const [isRecipeDialogOpen, openRecipeDialog] = useState(false);
    const [operations, setOperations] = useState([]);

    const refresh = () => expandReducer(false, { periodId: periodId, setOperations: setOperations });
    const doExpand = () => onExpend({ periodId: periodId, setOperations: setOperations });

    return (
        <div>
            <DialogAddSpending openState={isSpendingDialogOpen} closeDialog={() => { openSpendingDialog(false); refresh(); } } periodId={periodId} />
            <DialogAddRecipe openState={isRecipeDialogOpen} closeDialog={() => { openRecipeDialog(false); refresh(); }} periodId={periodId} />
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
                    <TableOperations rows={operations} />
                </ExpansionPanelDetails>

                <BottomNavigation
                    showLabels
                >
                    <BottomNavigationAction label="Ajouter une dépense" icon={<LocalAtmIcon color="secondary" />} onClick={() => { openSpendingDialog(true); }} />
                    <BottomNavigationAction label="Ajouter une recette" icon={<LocalAtmIcon color="primary" />} onClick={() => { openRecipeDialog(true); }} />
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
    }
    return newState;
}

export default PanelPeriod;