import React, { useState } from 'react';
import MainMenu from './MainMenu';
import PanelPeriod from './PanelPeriod';
import { connect } from "react-redux";
import DialogAddSpending from './DialogAddSpending';
import DialogAddRecipe from './DialogAddRecipe';
import DialogDeleteOperation from './DialogDeleteOperation'
import { getAllPeriod } from './actions'

function mapDispatchToProps(dispatch) {
    return {
        getAllPeriod: () => dispatch(getAllPeriod())
     }
}

function mapStateToProps(state) {
    return { allPeriods: state.periods };
};

const ConnectedDashboard = ({ allPeriods, getAllPeriod}) => {

    const [initialized, setInitialized] = useState(false);

    if (!initialized) {
        getAllPeriod();
        setInitialized(true);
    }

    return (
        <div>            
            <DialogAddSpending />
            <DialogAddRecipe />
            <DialogDeleteOperation />
            <MainMenu/>
            {allPeriods.map((p) => <PanelPeriod key={p.periodName} periodName={p.periodName} periodId={p.periodId} />)}
        </div>);
}

const Dashboard = connect(mapStateToProps, mapDispatchToProps)(ConnectedDashboard);
export default Dashboard;