import rootReducer from './rootReducer'
import { setPair, setPeriods, openSpendingDialog, openRecipeDialog, setOperations, setBalance } from './actions'

var redux;

beforeEach(() => {
    redux = new Redux();
});


test("Reducer should set pair when calling SET_PAIR",
    () => {
        redux.dispatch(setPair({ firstPairName: "aurelien", secondPairName: "marie" }));
        expect(redux.state.firstPairName).toBe("aurelien");
        expect(redux.state.secondPairName).toBe("marie");
    });


test("Reducer should set periods when callin SET_PERIODS",
    () => {
        var periods = ["Janvier 2017", "Février 2017", "Mars 2017"];

        redux.dispatch(setPeriods(periods));

        expect(redux.state.periods).toHaveLength(3);
        expect(redux.state.periods).toContain("Janvier 2017");
        expect(redux.state.periods).toContain("Février 2017");
        expect(redux.state.periods).toContain("Mars 2017");
    });

test("Reducer should dispatch two elements",
    () => {
        const periodId = '2007-01';
        redux.dispatch(openSpendingDialog(periodId));
        redux.dispatch(openRecipeDialog(periodId));

        expect(redux.state.spendingDialog).toEqual({ isOpen: true, periodId:periodId, data:null});
        expect(redux.state.recipeDialog).toEqual({ isOpen: true, periodId: periodId, data: null });
        expect(redux.state.periods).toEqual([]);
    });

test("Set operation when calling SET_OPERATIONS",
    () => {
        const periodId = "2007-01";
        const operations = [{ test: "1234" }];

        redux.dispatch(setOperations(periodId, operations));

        expect(redux.state.periodsData[periodId].operations).toEqual(operations);
        expect(redux.state.periodsData[periodId].balance).toBeNull();
    });

test("Set balance when calling SET_BALANCE",
    () => {
        const periodId = "2007-01";
        const balance = { by:"Aurelien", amountDue:100}

        redux.dispatch(setBalance(periodId, balance));
        expect(redux.state.periodsData[periodId].balance).toEqual(balance);
        expect(redux.state.periodsData[periodId].operations).toEqual([]);
    });

test("Set operations and balance when calling each other",
    () => {
        const periodId = "2007-01";
        const operations = [{ test: "1234" }];
        const balance = { by: "Aurelien", amountDue: 100 }

        redux.dispatch(setOperations(periodId, operations));
        redux.dispatch(setBalance(periodId, balance));

        expect(redux.state.periodsData[periodId].operations).toEqual(operations);
        expect(redux.state.periodsData[periodId].balance).toEqual(balance);

    });

test("Set operations and balance when calling each other inverted",
    () => {
        const periodId = "2007-01";
        const operations = [{ test: "1234" }];
        const balance = { by: "Aurelien", amountDue: 100 }

        redux.dispatch(setBalance(periodId, balance));
        redux.dispatch(setOperations(periodId, operations));

        expect(redux.state.periodsData[periodId].operations).toEqual(operations);
        expect(redux.state.periodsData[periodId].balance).toEqual(balance);

    });


function Redux() {
    Redux.prototype.dispatch = (action) => this.state = rootReducer(this.state, action);    
}
