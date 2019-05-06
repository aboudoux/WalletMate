import React, { useReducer } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import './dashboard.css';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import AccountCircle from '@material-ui/icons/AccountCircle';
import DialogCreatePeriod from './DialogCreatePeriod';
import { connect } from "react-redux";

import { openCreatePeriodPopup, closeCreatePeriodPopup } from './actions';

function mapDispatchToProps(dispatch) {
    return {
        openCreatePeriodPopup: () => dispatch(openCreatePeriodPopup()),
        closeCreatePeriodPopup: () => dispatch(closeCreatePeriodPopup())
    }
}

const ConnectedMainMenu = ({ openCreatePeriodPopup, closeCreatePeriodPopup }) =>
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

    const initialDashBoardState = { anchorLoginMenu: null, anchorActionMenu: null, createPeriodOpen: false };
    const [dashboardState, doAction] = useReducer(menuReducer, initialDashBoardState);


    const handleActionMenu = event => {
        doAction({ target: event.currentTarget, command: "actionMenu" });
    };

    const handleLoginMenu = event => {
        doAction({ target: event.currentTarget, command: "loginMenu" });
    };

    const handleClose = () => {
        doAction({ command: "resetAll" });
    };

    const handleDisconnect = () => {
        doAction({ command: "resetAll" });
        dispatch(null);
    };

    const handleCreatePeriod = () => {
        doAction({ command: "resetAll" });
        doAction({ command: "openCreatePeriodPopup" });
    }

    return (
        <div className="root">

            <DialogCreatePeriod />

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
                        <MenuItem onClick={openCreatePeriodPopup}>Créer une période</MenuItem>
                        <MenuItem onClick={closeCreatePeriodPopup}>Ajouter la prochaine période</MenuItem>
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

const MainMenu = connect(null, mapDispatchToProps)(ConnectedMainMenu);
export default MainMenu;