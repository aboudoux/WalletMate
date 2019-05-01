import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import Slide from '@material-ui/core/Slide';
import { connect } from 'react-redux';
import { closeDeleteOperationDialog, removeOperation} from './actions'

function mapStateToProps(state) {
    return {
        isOpen: state.deleteDialog.isOpen,
        periodId: state.deleteDialog.periodId,
        operationId: state.deleteDialog.operationId
    };
}

function mapDispatchToProps(dispatch) {
    return {
        closeDialog: () => dispatch(closeDeleteOperationDialog())
    }
}

const ConnectedDialogDeleteOperation = ({isOpen, closeDialog, periodId, operationId}) => {

    return (
        <Dialog
            open={isOpen}
            TransitionComponent={transition}
            keepMounted
            onClose={() => closeDialog()}
            aria-labelledby="alert-dialog-slide-title"
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle id="alert-dialog-slide-title">
                Supprimer l'opération ?
                </DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-slide-description">
                    Vous êtes sur le point de supprimer une opération.
                    Etes vous sûr de vouloir continuer ?
                    </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => closeDialog() } color="primary">
                    NON
                    </Button>
                <Button onClick={() => { removeOperation(periodId, operationId, () => closeDialog()); }} color="primary">
                    Oui
                    </Button>
            </DialogActions>
        </Dialog>
        );
}

function transition(props) {
    return <Slide direction="up" {...props} />;
}

const DialogDeleteOperation = connect(mapStateToProps, mapDispatchToProps)(ConnectedDialogDeleteOperation);
export default DialogDeleteOperation;