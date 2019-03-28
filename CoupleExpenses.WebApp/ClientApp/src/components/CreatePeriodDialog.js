import React, { useState } from 'react';
import { Post } from './Call';
import MenuItem from '@material-ui/core/MenuItem';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';

const CreatePeriodDialog = ({ openState, doAction, refreshDashboard }) => {
    const months = [
        "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre"
    ];

    let val = 1;
    const ranges = months.map(m => ({ value: val++, label: m }));
    const [selectedMonth, selectMonth] = useState(null);
    const [selectedYear, selectYear] = useState(null);

    return (
        <Dialog
            open={openState}
            onClose={() => doAction({ command: 'closeCreatePeriodPopup' })}
            aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">Créer une période</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Sélectionnez les informations de création pour la nouvelle periode.
                    </DialogContentText>
                <TextField
                    select
                    margin="dense"
                    label="Mois"
                    value={selectedMonth}
                    onChange={(event) => selectMonth(event.target.value)}
                >
                    {ranges.map(option => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                    ))}
                </TextField>

                <TextField
                    label="Année"
                    margin="dense"
                    value={selectedYear}
                    onChange={(event) => selectYear(event.target.value)}
                >
                </TextField>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => doAction({ command: 'closeCreatePeriodPopup' })} color="primary">
                    Annuler
                    </Button>
                <Button onClick={() => Post("/api/Period/Create", { Month: selectedMonth, Year: selectedYear })
                    .then(() => {
                        doAction({ command: 'closeCreatePeriodPopup' });
                        refreshDashboard(false);
                    })
                    .catch((error) => {
                        if (error.response.status === 401) {
                            alert('error 401');
                        }
                    })}

                    color="primary">
                    Valider
                    </Button>
            </DialogActions>
        </Dialog>
    );
}

export default CreatePeriodDialog;