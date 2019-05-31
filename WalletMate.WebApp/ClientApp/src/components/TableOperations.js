import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import DeleteIcon from '@material-ui/icons/Delete';
import RecipeIcon from '@material-ui/icons/ArrowBack';
import SpendingIcon from '@material-ui/icons/ArrowForward';
import EditIcon from '@material-ui/icons/Edit';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import IconAddOperation from '@material-ui/icons/AttachMoney';
import { connect } from 'react-redux';
import './TableOperations.css'
import { openDeleteOperationDialog, openUpdateSpendingDialog, openUpdateRecipeDialog, openSpendingDialog, openRecipeDialog} from './actions';

function mapDispatchToProps(dispatch) {
    return {
        openRecipeDialog: periodId => dispatch(openRecipeDialog(periodId)),
        openSpendingDialog: periodId => dispatch(openSpendingDialog(periodId)),
        openDeleteOperationDialog: (periodId, operationId) => dispatch(openDeleteOperationDialog(periodId, operationId)),
        openUpdateSpendingDialog: (row) => dispatch(openUpdateSpendingDialog(row)),
        openUpdateRecipeDialog: (row) => dispatch(openUpdateRecipeDialog(row))
    }
}

const ConnectedTableOperations = ({ periodId, rows, balance, openDeleteOperationDialog, openUpdateSpendingDialog, openUpdateRecipeDialog, openSpendingDialog, openRecipeDialog }) => {

    return (
        <div>

        <Paper >            
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Type</TableCell>
                        <TableCell>Binôme</TableCell>                        
                        <TableCell>Libelle</TableCell>
                        <TableCell>Montant</TableCell>
                        <TableCell>Opération</TableCell>
                        <TableCell align="center">Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                        {rows.map(row => (
                            <TableRow key={row.operationId} className="row-height" hover="true">
                                <TableCell component="th" scope="row">
                                    {
                                        row.type === "Recette"
                                            ? <SpendingIcon style={{ color: 'green' }} />
                                            : <RecipeIcon style={{ color: 'red' }} />
                                    }
                                    {row.type}
                            </TableCell>
                                <TableCell>{row.pair}</TableCell>
                                <TableCell>{row.label}</TableCell>
                                <TableCell>{row.amount} €</TableCell>
                                <TableCell>{row.category}</TableCell>
                                <TableCell>
                                <Tooltip title="Editer cette opération">
                                        <IconButton className="icon" onClick={() => {
                                            row.type === "Dépense"
                                                ? openUpdateSpendingDialog(row)
                                                : openUpdateRecipeDialog(row);
                                        }}>
                                            
                                        <EditIcon />
                                        </IconButton>
                                </Tooltip>
                                <Tooltip title="Supprimer cette opération">
                                        <IconButton className="icon" onClick={() => openDeleteOperationDialog(periodId, row.operationId)}>
                                        <DeleteIcon />
                                    </IconButton>
                                </Tooltip>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
                </Table>
                <div className="wrapper">
                    <div>
                        <IconButton classes={{ root: 'operation-button' }} style={{ color: 'red' }} onClick={() => { openSpendingDialog(periodId); }}>
                            <IconAddOperation /> Ajouter une dépense
                        </IconButton>
                        <IconButton classes={{ root: 'operation-button' }} style={{ color: 'green' }} onClick={() => { openRecipeDialog(periodId); }} >
                            <IconAddOperation /> Ajouter une recette
                        </IconButton>
                    </div>
                <div className="balance">                    
                    {
                    (balance) 
                        ? <p><span id="by">{balance.by}</span> doit la somme de <span id="amount-due">{balance.amountDue}</span> €</p>
                        : <p>No balance found</p>
                    }
                    </div>
                </div>
            </Paper>
         </div>       
    );
};

const TableOperations = connect(null, mapDispatchToProps)(ConnectedTableOperations);
export default TableOperations;