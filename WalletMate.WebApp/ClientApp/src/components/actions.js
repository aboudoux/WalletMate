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

export function openCreatePeriodPopup() {
    return { type: 'OPEN_CREATE_PERIOD_POPUP' }
}

export function closeCreatePeriodPopup() {
    return { type: 'CLOSE_CREATE_PERIOD_POPUP' }
}

export function removeOperation(periodId, operationId, then) {
    Post('/api/Operation/Remove', { PeriodId: periodId, OperationId: operationId })
        .then(() => then())
        .catch((error) => handleError(error));
};

export function createPeriod(month, year, then) {
    Post("/api/Period/Create", { Month: month, Year: year })
        .then(() => then())
        .catch((error) => handleError(error));
}

function handleError(error) {
    if (error.response.status === 401) {
        alert('error 401');
    }
}