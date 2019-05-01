import React, { useState } from 'react';
import { Get } from './Call';
import MainMenu from './MainMenu';
import PanelPeriod from './PanelPeriod';
import { setPeriods } from './actions';
import { connect } from "react-redux";
import DialogAddSpending from './DialogAddSpending';
import DialogAddRecipe from './DialogAddRecipe';
import DialogDeleteOperation from './DialogDeleteOperation'

function mapDispatchToProps(dispatch) {
    return {
        setPeriods: periods => dispatch(setPeriods(periods))
     }
}

function mapStateToProps(state) {
    return { allPeriods: state.periods };
};

const ConnectedDashboard = ({ dispatch, setPeriods, allPeriods }) => {

    const [initialized, setInitialized] = useState(false);

    if (!initialized) {
        Get("/api/Period/All")
            .then(response => setPeriods(response.data))
            .catch((error) => {
                if (error.response.status === 401) {
                    dispatch(null);
                }
            });
        setInitialized(true);
    }

    return (
        <div>            
            <DialogAddSpending />
            <DialogAddRecipe />
            <DialogDeleteOperation />
            <MainMenu dispatch={dispatch} />
            {allPeriods.map((p) => <PanelPeriod key={p.periodName} periodName={p.periodName} periodId={p.periodId} isExpanded={false} dispatch={dispatch} />)}
        </div>);
}

const Dashboard = connect(mapStateToProps, mapDispatchToProps)(ConnectedDashboard);
export default Dashboard;