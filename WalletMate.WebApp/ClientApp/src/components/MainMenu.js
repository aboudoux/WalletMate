import React from 'react';
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
import { openCreatePeriodPopup, closeCreatePeriodPopup, showActionMenu, showLoginMenu, hideAllMenu, disconnectUser } from './actions';

function mapDispatchToProps(dispatch) {
    return {
        openCreatePeriodPopup: () => dispatch(openCreatePeriodPopup()),
        closeCreatePeriodPopup: () => dispatch(closeCreatePeriodPopup()),
        showActionMenu: element => dispatch(showActionMenu(element)),
        showLoginMenu: element => dispatch(showLoginMenu(element)),
        hideAllMenu: () => dispatch(hideAllMenu()),
        disconnectUser: () => dispatch(disconnectUser())
    }
}

function mapStateToProps(state) {
    return { anchorLoginMenu: state.mainMenu.anchorLoginMenu, anchorActionMenu: state.mainMenu.anchorActionMenu};
};

const ConnectedMainMenu = ({ openCreatePeriodPopup, closeCreatePeriodPopup, anchorLoginMenu, anchorActionMenu, showActionMenu, showLoginMenu, hideAllMenu, disconnectUser }) =>
{
    return (
        <div className="root">

            <DialogCreatePeriod />

            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        aria-owns={Boolean(anchorActionMenu) ? 'menu-action' : undefined}
                        className="menu-button"
                        color="inherit"
                        aria-label="Menu"
                        aria-haspopup="true"
                        onClick={event => showActionMenu(event.currentTarget)}>
                        <MenuIcon />
                    </IconButton>
                    <Menu
                        id="menu-action"
                        anchorEl={anchorActionMenu}
                        open={Boolean(anchorActionMenu)}
                        onClose={hideAllMenu}>
                        <MenuItem onClick={openCreatePeriodPopup}>Créer une période</MenuItem>
                        <MenuItem onClick={closeCreatePeriodPopup}>Ajouter la prochaine période</MenuItem>
                    </Menu>

                    <Typography variant="h6" color="inherit" className="grow">
                        Gestion des dépenses
                    </Typography>
                    <div>
                        <IconButton
                            aria-owns={Boolean(anchorLoginMenu) ? 'menu-appbar' : undefined}
                            aria-haspopup="true"
                            onClick={event => showLoginMenu(event.currentTarget)}
                            color="inherit">
                            <AccountCircle />
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorLoginMenu}
                            open={Boolean(anchorLoginMenu)}
                            onClose={hideAllMenu}
                        >
                            <MenuItem onClick={() => {
                                disconnectUser();
                                hideAllMenu();
                            }}>Déconnecter</MenuItem>
                        </Menu>
                    </div>
                </Toolbar>
            </AppBar>
        </div>
    );
};

const MainMenu = connect(mapStateToProps, mapDispatchToProps)(ConnectedMainMenu);
export default MainMenu;