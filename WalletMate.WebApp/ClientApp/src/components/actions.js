import { Post } from './Call'


export function setPair(pair) {
    return { type: 'SET_PAIR', pair}
};

export function setConnectedUser(authInfo) {
    return { type: 'SET_CONNECTED_USER', authInfo}
}

export function selectUsername(selectedUsername) {
    return { type: 'SELECT_USERNAME', selectedUsername }
}

export function setPeriods(periods) {
    return { type: 'SET_PERIODS', periods}
}

export function openSpendingDialog(periodId) {
    return { type: 'OPEN_SPENDING_DIALOG', periodId}
}

export function openUpdateSpendingDialog(row) {
    return { type: 'OPEN_UPDATE_SPENDING_DIALOG', row }
}

export function closeSpendingDialog() {
    return { type: 'CLOSE_SPENDING_DIALOG' }
}

export function openRecipeDialog(periodId) {
    return { type: 'OPEN_RECIPE_DIALOG', periodId }
}

export function openUpdateRecipeDialog(row) {
    return { type: 'OPEN_UPDATE_RECIPE_DIALOG', row }
}

export function closeRecipeDialog() {
    return { type: 'CLOSE_RECIPE_DIALOG' }
}

export function closeDeleteOperationDialog() {
    return { type: 'CLOSE_DELETE_OPERATION_DIALOG'}
}

export function openDeleteOperationDialog(periodId, operationId) {
    return { type: 'OPEN_DELETE_OPERATION_DIALOG', periodId, operationId }
}

export function removeOperation(periodId, operationId, callBack) {
    Post('/api/Operation/Remove', { PeriodId: periodId, OperationId: operationId })
        .then(() => callBack())
        .catch((error) => {
            if (error.response.status === 401) {
                alert('error 401');
            }
        });
}