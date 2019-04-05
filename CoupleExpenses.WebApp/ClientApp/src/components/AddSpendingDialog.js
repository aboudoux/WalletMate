import React, { useState } from 'react'
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Input from '@material-ui/core/Input';
import InputAdornment from '@material-ui/core/InputAdornment';
import Radio from '@material-ui/core/Radio';
import RadioGroup from '@material-ui/core/RadioGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';


const AddSpendingDialog = ({ openState, setOpenState }) => {

    
    return (
        <Dialog
            open={openState}
            aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">Ajouter un dépense</DialogTitle>
            <DialogContent>
                <FormControl fullWidth>
                    <InputLabel htmlFor="adornment-amount">Montant</InputLabel>
                    <Input
                        id="adornment-amount"
                        startAdornment={<InputAdornment position="start">€</InputAdornment>}
                    />
                </FormControl>
                <FormControl fullWidth>
                    <InputLabel htmlFor="adornment-libelle">Libelle</InputLabel>
                    <Input
                        id="adornment-libelle"
                    />
                    <RadioGroup
                        row
                        aria-label="Par"
                        name="pair">
                            <FormControlLabel value="1" control={<Radio />} label="Aurélien" />
                            <FormControlLabel value="2" control={<Radio />} label="Marie" />
                    </RadioGroup>
                    <RadioGroup
                        row
                        aria-label="Destination"
                        name="destination">
                        <FormControlLabel value="1" control={<Radio />} label="Commun" />
                        <FormControlLabel value="2" control={<Radio />} label="Avance" />
                    </RadioGroup>
                </FormControl>

            </DialogContent>
            <DialogActions>
                <Button color="primary" onClick={() => setOpenState(false)}>
                    Annuler
                    </Button>
                <Button
                    color="primary">
                    Valider
                    </Button>
            </DialogActions>
        </Dialog>);
}

export default AddSpendingDialog;