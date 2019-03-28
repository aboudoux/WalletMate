import React from 'react'
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Input from '@material-ui/core/Input';
import InputAdornment from '@material-ui/core/InputAdornment';


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