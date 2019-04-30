
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