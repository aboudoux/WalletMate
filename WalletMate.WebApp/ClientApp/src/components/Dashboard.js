import React, { useState } from 'react';
import { Get } from './Call';
import MainMenu from './MainMenu';
import PanelPeriod from './PanelPeriod';

const Dashboard = ({ dispatch }) => {

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
            {state.map((p) => <PanelPeriod periodName={p.periodName} periodId={p.periodId} isExpanded={false} dispatch={dispatch} />)}

        </div>);
}

export default Dashboard;