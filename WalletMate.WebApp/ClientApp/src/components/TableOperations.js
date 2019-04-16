import React, { useState } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Slide from '@material-ui/core/Slide';
import Button from '@material-ui/core/Button';
import { Post } from './Call'

const TableOperations = ({ rows, refresh }) => {

    const [deleteDialogState, openDeleteDialog] = useState({ isOpen: false, periodId: '', operationId: 0, reload:refresh});

    return (
        <div>

        <Paper >            
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Type</TableCell>
                        <TableCell>Binôme</TableCell>
                        <TableCell>Montant</TableCell>
                        <TableCell>Libelle</TableCell>
                        <TableCell>Opération</TableCell>
                        <TableCell align="center">Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map(row => (
                        <TableRow key={row.operationId}>
                            <TableCell component="th" scope="row">
                                {row.type}
                            </TableCell>
                            <TableCell>{row.pair}</TableCell>
                            <TableCell>{row.amount} €</TableCell>
                            <TableCell>{row.label}</TableCell>
                            <TableCell>{row.category}</TableCell>
                            <TableCell>
                                <Tooltip title="Editer cette opération">
                                    <IconButton>
                                        <EditIcon />
                                    </IconButton>
                                </Tooltip>
                                <Tooltip title="Supprimer cette opération">
                                        <IconButton onClick={() => openDeleteDialog({ isOpen: true, periodId:row.periodId, operationId:row.operationId, reload:refresh })}>
                                        <DeleteIcon />
                                    </IconButton>
                                </Tooltip>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
                </Table>

            <Dialog
                open={deleteDialogState.isOpen}
                TransitionComponent={transition}
                keepMounted
                onClose={() => openDeleteDialog({isOpen:false})}
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
                        <Button onClick={() => openDeleteDialog({ isOpen: false })} color="primary">
                        NON
                    </Button>
                        <Button onClick={() => {removeOperation(deleteDialogState.periodId, deleteDialogState.operationId, deleteDialogState.reload); openDeleteDialog(false); }} color="primary">
                        Oui
                    </Button>
                </DialogActions>
                </Dialog>
            </Paper>
         </div>       
    );
};

function removeOperation(periodId, operationId, reload) {
    Post('/api/Operation/Remove', { PeriodId: periodId, OperationId: operationId })
        .then(() => reload())
        .catch((error) => {
            if (error.response.status === 401) {
                alert('error 401');
            }
        });
}

function transition(props) {
    return <Slide direction="up" {...props} />;
}

export default TableOperations;