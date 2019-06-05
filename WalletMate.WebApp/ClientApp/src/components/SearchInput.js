import React, { useState } from 'react';
import Search from '@material-ui/icons/Search';
import './SearchInput.css'
import { searchOperations } from './actions';
import { connect } from 'react-redux';


function mapDispatchToProps(dispatch) {
    return {
        searchOperations: filter => dispatch(searchOperations(filter))
    }
}

const ConnectedSearchInput = ({ id, searchOperations }) => {

    var nameInput;
    const [inputText, setInputText] = useState("");

    const updateInput = text => {
        setInputText(text);
        searchOperations(text);
        nameInput.focus();
    };

    return (
            <div id={id} className="search-grid">
            <input
                ref={input => nameInput = input}
                className="search-input"
                value={inputText}
                placeholder="Rechercher une opération..."
                type="text"
                onChange={(e) => updateInput(e.target.value)} />
            <Search className="search-icon" onClick={() => updateInput("")} />
            </div>
    );
}

export const SearchInput = connect(null, mapDispatchToProps)(ConnectedSearchInput);

function mapStateToProps(state) {
    return {
        searchResult: state.searchResult
    };
};

export const ConnectedSearchResult = ({ searchResult }) => {
    return (
        <div className="search-result">
            <table>
                <thead>
                    <td>Période</td>
                    <td>Type</td>
                    <td>Binôme</td>
                    <td>Libelle</td>
                    <td>Montant</td>
                    <td>Opération</td>
                </thead>

                {searchResult.map(row => (
                    <tbody>
                        <td>{row.periodId}</td>
                        <td>{row.type}</td>
                        <td>{row.pair}</td>
                        <td>{row.label}</td>
                        <td>{row.amount} €</td>
                        <td>{row.category}</td>
                    </tbody>
                    ))}
            </table>
        </div>
     )
}

export const SearchResult = connect(mapStateToProps, null)(ConnectedSearchResult);