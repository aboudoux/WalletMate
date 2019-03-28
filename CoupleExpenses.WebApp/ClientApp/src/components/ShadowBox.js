import React from 'react';
import './style.css'


const ShadowBox = ({ message, children }) => {
    return (
        <div className="login-box">
            <h2>{message}</h2>
            {children}
        </div>
    );
}

export default ShadowBox;
