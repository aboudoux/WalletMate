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
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
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
        <div className="table-operations">

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
                            <TableRow key={row.operationId} id="row-height" hover="true">
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
                                        <IconButton id="icon" onClick={() => {
                                            row.type === "Dépense"
                                                ? openUpdateSpendingDialog(row)
                                                : openUpdateRecipeDialog(row);
                                        }}>
                                            
                                        <EditIcon />
                                        </IconButton>
                                </Tooltip>
                                <Tooltip title="Supprimer cette opération">
                                        <IconButton id="icon" onClick={() => openDeleteOperationDialog(periodId, row.operationId)}>
                                        <DeleteIcon />
                                    </IconButton>
                                </Tooltip>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
                </Table>
                <div className="wrapper">
                    <div></div>
                    <div className="balance">
                        {
                            (balance)
                                ? balance.amountDue === 0
                                    ? <p></p>
                                    : <p><span id="by">{balance.by}</span> doit la somme de <span id="amount-due">{balance.amountDue}</span> €</p>
                                : <p>No balance found</p>
                        }
                    </div>
                    <div>
                        <Tooltip title="Ajouter un recette">
                            <Fab size="small" id="add-recipe-button" aria-label="Add" onClick={() => { openRecipeDialog(periodId); }}>
                                <AddIcon />
                            </Fab>
                        </ Tooltip>
                        <Tooltip title="Ajouter un dépense">
                            <Fab size="small" id="add-spending-button" aria-label="Add" onClick={() => { openSpendingDialog(periodId); }}>
                                <AddIcon />
                            </Fab>
                        </ Tooltip>
                    </div>
                </div>
            </Paper>
         </div>       
    );
};

const TableOperations = connect(null, mapDispatchToProps)(ConnectedTableOperations);
export default TableOperations;