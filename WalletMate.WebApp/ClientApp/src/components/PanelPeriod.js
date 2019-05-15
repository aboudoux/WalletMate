import React from 'react';
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
import { openSpendingDialog, openRecipeDialog, showPeriodPanel, collapsePeriodPanel} from './actions';

function mapDispatchToProps(dispatch) {
    return {
        openSpendingDialog: periodId => dispatch(openSpendingDialog(periodId)),
        openRecipeDialog: periodId => dispatch(openRecipeDialog(periodId)),
        showPeriodPanel: periodId => dispatch(showPeriodPanel(periodId)),
        collapse: periodId => dispatch(collapsePeriodPanel(periodId))
    }
}

function mapStateToProps(state, ownProps) {
    return {        
        isExpanded: state.periodsData[ownProps.periodId] != undefined ? state.periodsData[ownProps.periodId].expanded : false,
        operations: state.periodsData[ownProps.periodId] != undefined ? state.periodsData[ownProps.periodId].operations : [],
        balance: state.periodsData[ownProps.periodId] != undefined ? state.periodsData[ownProps.periodId].balance : null
    };
}

const ConnectedPanelPeriod = ({ periodName, periodId, isExpanded, openSpendingDialog, openRecipeDialog, showPeriodPanel, operations, collapse, balance }) => {

    return (
        <div>            
            <ExpansionPanel expanded={isExpanded} >
                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} className="period-title" onClick={() => isExpanded ? collapse(periodId) : showPeriodPanel(periodId)}>
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
                    <TableOperations rows={operations} balance={balance} />
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

const PanelPeriod = connect(mapStateToProps, mapDispatchToProps)(ConnectedPanelPeriod);
export default PanelPeriod;