
const initialState = {
    firstPairName: '',
    secondPairName: '',
    connectedUser: null,
    periods: []
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

    default:
        return state;
    }

    return newState;
};
export default rootReducer;