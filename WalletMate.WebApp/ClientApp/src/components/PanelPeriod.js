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
import { showPeriodPanel, collapsePeriodPanel } from './actions';
import Waiter from './Waiter';

function mapDispatchToProps(dispatch) {
    return {
        showPeriodPanel: periodId => dispatch(showPeriodPanel(periodId)),
        collapse: periodId => dispatch(collapsePeriodPanel(periodId))
    }
}

function mapStateToProps(state, ownProps) {
    return {        
        isExpanded: state.periodsData[ownProps.periodId] !== undefined ? state.periodsData[ownProps.periodId].expanded : false,
        operations: state.periodsData[ownProps.periodId] !== undefined ? state.periodsData[ownProps.periodId].operations : [],
        balance: state.periodsData[ownProps.periodId] !== undefined ? state.periodsData[ownProps.periodId].balance : null,
        panelLoading: state.periodsData[ownProps.periodId] !== undefined ? state.periodsData[ownProps.periodId].isLoading : false
    };
}

const ConnectedPanelPeriod = ({ periodName, periodId, isExpanded, showPeriodPanel, operations, collapse, balance, panelLoading }) => {

    return (
        <div>            
            <ExpansionPanel expanded={isExpanded} >
                <ExpansionPanelSummary expandIcon=
                    {
                    panelLoading
                        ? <Waiter id="panelWaiter" />
                        : <ExpandMoreIcon />
                    } className="period-title" onClick={() => isExpanded ? collapse(periodId) : showPeriodPanel(periodId)}>
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
                    <TableOperations periodId={periodId} rows={operations} balance={balance} />
                </ExpansionPanelDetails>
            </ExpansionPanel>
        </div>
    );       
}

const PanelPeriod = connect(mapStateToProps, mapDispatchToProps)(ConnectedPanelPeriod);
export default PanelPeriod;