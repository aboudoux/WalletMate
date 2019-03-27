import axios from 'axios';
import { Buffer } from 'buffer'


export const Get = (url) =>
{
    return axios.get(url,
        {
            headers: {
                "Authorization": "Guid " + getAuthorizationTokenFromLocalStorage()
            }
        });
}

export const Post = (url, param) => {
    return axios.post(url, param,
        {
            headers: {
                "Authorization": "Guid " + getAuthorizationTokenFromLocalStorage()
            }
        });
}

const getAuthorizationTokenFromLocalStorage = () => {
    var authInfo = JSON.parse(localStorage.getItem('connectedUser'));
    return Buffer.from(`${authInfo.username}:${authInfo.authKey}`).toString('base64');
}