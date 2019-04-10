import React, { useState } from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import { Get } from './Call';

const PeriodOperations = ({ expended, dispatch }) => {
    const [rows, updateRows] = useState([]);
    const [initialised, setInitialized] = useState(false);

    if (!expended && initialised) {
        setInitialized(false);
    } else if (expended && !initialised) {
        Get("/api/Operation/All?periodId=2019-05")
            .then(response => updateRows(response.data))
            .catch((error) => {
                if (error.response.status === 401) {
                    dispatch(null);
                }
            });
        setInitialized(true);
    }

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
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Paper>
    );
};

export default PeriodOperations;