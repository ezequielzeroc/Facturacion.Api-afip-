
import SysConfig from '../config';

export const LOGIN_REQUEST = 'LOGIN_REQUEST';
export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';
export const LOGIN_FAILURE = 'LOGIN_FAILURE';
export const LOGOUT_REQUEST = 'LOGOUT_REQUEST';
export const LOGOUT_SUCCESS = 'LOGOUT_SUCCESS';
export const LOGOUT_FAILURE = 'LOGOUT_FAILURE';
export const CHANGE_PASSWORD_REQUEST = 'CHANGE_PASSWORD_REQUEST';
export const CHANGE_PASSWORD_SUCCESS = 'CHANGE_PASSWORD_SUCCESS';
export const CHANGE_PASSWORD_FAILURE = 'CHANGE_PASSWORD_FAILURE';

function requestChangePassword(creds) {
  return {
    type: CHANGE_PASSWORD_REQUEST,
    isFetching: true,
    creds,
  };
}

export function changePasswordSuccess(user) {
  return {
    type: CHANGE_PASSWORD_SUCCESS,
    isFetching: false,
    changed:true
  };
}

function changePasswordError(message) {
  return {
    type: CHANGE_PASSWORD_FAILURE,
    isFetching: false,
    message,
  };
}

function requestLogin(creds) {
  return {
    type: LOGIN_REQUEST,
    isFetching: true,
    isAuthenticated: false,
    creds,
  };
}

export function receiveLogin(user) {
  return {
    type: LOGIN_SUCCESS,
    isFetching: false,
    isAuthenticated: true,
    id_token: user.id_token,
  };
}

function loginError(message) {
  return {
    type: LOGIN_FAILURE,
    isFetching: false,
    isAuthenticated: false,
    message,
  };
}

function requestLogout() {
  return {
    type: LOGOUT_REQUEST,
    isFetching: true,
    isAuthenticated: true,
  };
}

export function receiveLogout() {
  return {
    type: LOGOUT_SUCCESS,
    isFetching: false,
    isAuthenticated: false,
  };
}







// Logs the user out
export function logoutUser() {
  return dispatch => {
    dispatch(requestLogout());
    localStorage.removeItem('id_token');
    document.cookie = 'id_token=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    dispatch(receiveLogout());
  };
}

export function loginUser(creds) {
  let email = creds.login;
  let password = creds.password;

  
  const config = {
    method: 'POST',
    credentials: 'include',
    withCredentials:true,
    mode:'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password })
  };
  
  return dispatch => {
    dispatch(requestLogin(creds));
    return fetch(`${SysConfig.base_url}/session/login`, config)
      .then(response => response.json().then(user => ({ user, response })))
      .then(({ user, response }) => {
        if (!response.ok) {
          dispatch(loginError(user.message));
          return Promise.reject(user);
        }
        localStorage.setItem('id_token', user.token);
        localStorage.setItem('user_id',user.id);
        localStorage.setItem('user_name',user.name);
        dispatch(receiveLogin(user));
        return Promise.resolve(user);

      })
      .catch(err=>{dispatch(loginError("El usuario y la contraseña no coinciden."))});
  
  };
}

export function changeUserPassword(creds) {
  const config = {
    method: 'post',
    credentials: 'include',
    withCredentials:true,
    mode:'cors',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(creds),
    credentials: 'include',
  };
  
  return dispatch => {
    dispatch(requestChangePassword(creds));
    return fetch(`${SysConfig.base_url}/session/changepassword`, config)
      .then(response => response.json().then(user => ({ user, response })))
      .then(({ user, response }) => {
        if (!response.ok) {
          dispatch(changePasswordError(user.message));
          return Promise.reject(user);
        }
        dispatch(changePasswordSuccess(user));
        return Promise.resolve(user);
      })
      .catch(err => console.error('Error: ', err));
  
  };
}

