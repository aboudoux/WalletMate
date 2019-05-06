
const initialState = {
    firstPairName: '',
    secondPairName: '',
    connectedUser: null,
    periods: [],
    spendingDialog: { isOpen: false, periodId: '', data:null },
    recipeDialog: { isOpen: false, periodId: '', data:null },
    deleteDialog: { isOpen: false, periodId: '', operationId: 0 },
    createPeriodDialog: { isOpen: false}
};

function rootReducer(state = initialState, action) {

    var newState = { ...state }

    switch (action.type) {
        case 'SET_PAIR':
            newState.firstPairName = action.pair.firstPairName;
            newState.secondPairName = action.pair.secondPairName;
            break;
        case 'SET_CONNECTED_USER':
            newState.connectedUser = action.authInfo;
            break;
        case 'SELECT_USERNAME':
            newState.connectedUser = {username:action.selectedUsername}
            break;
        case 'SET_PERIODS':
            newState.periods = action.periods;
            break;
        case 'OPEN_SPENDING_DIALOG':
            newState.spendingDialog.isOpen = true;
            newState.spendingDialog.periodId = action.periodId;
            break;
        case 'OPEN_UPDATE_SPENDING_DIALOG':
            newState.spendingDialog.isOpen = true;
            newState.spendingDialog.data = action.row;
            break;
        case 'CLOSE_SPENDING_DIALOG':
            newState.spendingDialog.isOpen = false;
            break;
        case 'OPEN_RECIPE_DIALOG':
            newState.recipeDialog.isOpen = true;
            newState.recipeDialog.periodId = action.periodId;
            break;
        case 'OPEN_UPDATE_RECIPE_DIALOG':
            newState.recipeDialog.isOpen = true;
            newState.recipeDialog.data = action.row;
            break;
        case 'CLOSE_RECIPE_DIALOG':
            newState.recipeDialog.isOpen = false;
            break;
        case 'OPEN_DELETE_OPERATION_DIALOG':            
            newState.deleteDialog.isOpen = true;
            newState.deleteDialog.periodId = action.periodId;
            newState.deleteDialog.operationId = action.operationId;
            break;
        case 'CLOSE_DELETE_OPERATION_DIALOG':
            newState.deleteDialog.isOpen = false;
            break;        
        case 'OPEN_CREATE_PERIOD_POPUP':
            newState.createPeriodDialog.isOpen = true;
            break;
        case 'CLOSE_CREATE_PERIOD_POPUP':
            newState.createPeriodDialog.isOpen = false;
            break;
    default:
        return state;
    }

    return newState;
};
export default rootReducer;