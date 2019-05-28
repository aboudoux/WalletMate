import React, { useState } from 'react';
import MenuItem from '@material-ui/core/MenuItem';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import { connect } from "react-redux";
import { closeCreatePeriodPopup, createPeriod } from './actions'
import './DialogCreatePeriod.css'

function mapDispatchToProps(dispatch) {
    return {
        closeCreatePeriodPopup: () => dispatch(closeCreatePeriodPopup()),
        createPeriod: (selectedMonth, selectedYear) => dispatch(createPeriod(selectedMonth, selectedYear))
    }
}

function mapStateToProps(state) {
    return { openState: state.createPeriodDialog.isOpen };
};

const ConnectedDialogCreatePeriod = ({ openState, closeCreatePeriodPopup, createPeriod }) => {

    const months = [
        "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre"
    ];

    let val = 1;
    const ranges = months.map(m => ({ value: val++, label: m }));
    const [selectedMonth, selectMonth] = useState(null);
    const [selectedYear, selectYear] = useState(null);

    const close = () => {
        selectMonth(null);
        selectYear(null);
        closeCreatePeriodPopup();
    }

    return (
        <Dialog
            open={openState}
            onClose={() => close() }
            aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">Créer une période</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Sélectionnez les informations de création pour la nouvelle periode.
                </DialogContentText>
                <TextField className="marge"
                    select
                    label="Mois"
                    margin="normal"
                    value={selectedMonth}
                    onChange={(event) => selectMonth(event.target.value)}
                    InputLabelProps={{ shrink: selectedMonth ? true : false }}
                >
                    {ranges.map(option => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                    ))}
                </TextField>

                <TextField className="marge"
                    label="Année"
                    margin="normal"
                    value={selectedYear}
                    onChange={(event) => selectYear(event.target.value)}
                >
                </TextField>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => close()} color="primary">
                    Annuler
                    </Button>
                <Button onClick={() => {
                    createPeriod(selectedMonth, selectedYear);
                    selectMonth(null);
                    selectYear(null); 
                }}
                    color="primary">
                    Valider
                    </Button>
            </DialogActions>
        </Dialog>
    );
}

const DialogCreatePeriod = connect(mapStateToProps, mapDispatchToProps)(ConnectedDialogCreatePeriod);
export default DialogCreatePeriod;