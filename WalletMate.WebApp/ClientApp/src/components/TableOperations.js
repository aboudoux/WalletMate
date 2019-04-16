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

const TableOperations = ({ rows }) =>
{
    return (
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
                                    <IconButton>
                                        <DeleteIcon />
                                    </IconButton>
                                </Tooltip>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Paper>
    );
};

export default TableOperations;