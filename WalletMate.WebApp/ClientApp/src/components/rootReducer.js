
const initialState = {
    firstPairName: '',
    secondPairName: '',
    connectedUser: JSON.parse(localStorage.getItem('connectedUser')),
    periods: [],
    periodsData: {},
    spendingDialog: { isOpen: false, periodId: '', data:null },
    recipeDialog: { isOpen: false, periodId: '', data:null },
    deleteDialog: { isOpen: false, periodId: '', operationId: 0 },
    createPeriodDialog: { isOpen: false },
    mainMenu: { anchorLoginMenu: null, anchorActionMenu: null },
    searchResult: [],
    searchLoading: false
};

const defaultData = { operations: [], expanded: false, balance: null, isLoading:false }

function rootReducer(state = initialState, action) {

    var newState = { ...state }

    var hideAllMainMenu = () => {
        newState.mainMenu.anchorLoginMenu = null;
        newState.mainMenu.anchorActionMenu = null;
    }

    switch (action.type) {
        case 'SHOW_ACTION_MENU':
            newState.mainMenu.anchorActionMenu = action.element;
            break;
        case 'SHOW_LOGIN_MENU':
            newState.mainMenu.anchorLoginMenu = action.element;
            break;
        case 'HIDE_ALL_MENU':
            hideAllMainMenu();
            break;
        case 'SET_PAIR':
            newState.firstPairName = action.pair.firstPairName;
            newState.secondPairName = action.pair.secondPairName;
            break;
        case 'SET_CONNECTED_USER':
            localStorage.setItem('connectedUser', JSON.stringify(action.authInfo));
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
            newState.spendingDialog.data = null;
            break;
        case 'OPEN_UPDATE_SPENDING_DIALOG':
            newState.spendingDialog.isOpen = true;
            newState.spendingDialog.data = action.row;
            break;
        case 'CLOSE_SPENDING_DIALOG':
            newState.spendingDialog.isOpen = false;
            newState.spendingDialog.data = null;
            break;
        case 'OPEN_RECIPE_DIALOG':
            newState.recipeDialog.isOpen = true;
            newState.recipeDialog.periodId = action.periodId;
            newState.recipeDialog.data = null;
            break;
        case 'OPEN_UPDATE_RECIPE_DIALOG':
            newState.recipeDialog.isOpen = true;
            newState.recipeDialog.data = action.row;
            break;
        case 'CLOSE_RECIPE_DIALOG':
            newState.recipeDialog.isOpen = false;
            newState.recipeDialog.data = null;
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
            hideAllMainMenu();
            break;
        case 'CLOSE_CREATE_PERIOD_POPUP':
            newState.createPeriodDialog.isOpen = false;
            hideAllMainMenu();
            break;
        case 'SET_OPERATIONS':
            newState.periodsData = {
                ...state.periodsData,
                [action.periodId]: { ...defaultData, ...state.periodsData[action.periodId], expanded: true, operations: action.operations, isLoading: false } 
            }
            break;
        case 'SET_BALANCE':
            newState.periodsData = {
                ...state.periodsData,
                [action.periodId]: { ...defaultData, ...state.periodsData[action.periodId], balance: action.balance }
            }
            break;
        case 'PANEL_LOADING':
            newState.periodsData = {
                ...state.periodsData,
                [action.periodId]: { ...defaultData, ...state.periodsData[action.periodId], isLoading: true }
            }
            break;
        case 'COLLAPSE_PERIOD_PANEL':
            newState.periodsData = { ...state.periodsData, [action.periodId]: defaultData }
            break;
        case 'SET_SEARCH_RESULT':
            newState.searchResult = action.searchResult;
            newState.searchLoading = false;
            break;
        case 'SEARCH_LOADING':
            newState.searchLoading = true;
            break;
    default:
        return state;
    }

    return newState;
};
export default rootReducer;