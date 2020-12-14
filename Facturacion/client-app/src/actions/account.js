
import SysConfig from '../config';
import {  toast } from 'react-semantic-toasts';
export const LOGIN_REQUEST = 'LOGIN_REQUEST';
export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';
export const LOGIN_FAILURE = 'LOGIN_FAILURE';
export const LOGOUT_REQUEST = 'LOGOUT_REQUEST';
export const LOGOUT_SUCCESS = 'LOGOUT_SUCCESS';
export const LOGOUT_FAILURE = 'LOGOUT_FAILURE';
export const CHANGE_PASSWORD_REQUEST = 'CHANGE_PASSWORD_REQUEST';
export const CHANGE_PASSWORD_SUCCESS = 'CHANGE_PASSWORD_SUCCESS';
export const CHANGE_PASSWORD_FAILURE = 'CHANGE_PASSWORD_FAILURE';

export const CREATE_USER_REQUEST = 'CREATE_USER_REQUEST';
export const CREATE_USER_SUCCESS = 'CREATE_USER_SUCCESS';
export const CREATE_USER_FAILURE = 'CREATE_USER_FAILURE';
export const HANDLE_CHANGE = 'HANDLE_CHANGE';

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


function requestCreateUser(userInfo) {
  return {
    type: CREATE_USER_REQUEST,
    isFetching: true,
    created:false,
    message:'',
    userInfo,
  };
}

export function createUserSuccess(message) {
  return {
    type: CREATE_USER_SUCCESS,
    isFetching: false,
    message:message,
    created:true
  };
}

function createUserError(message) {
  return {
    type: CREATE_USER_FAILURE,
    isFetching: false,
    created:false,
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


export function handleChange(target){
  const {name,value} = target;
  console.log("desde acctiones",name,value)
  return{
    type:HANDLE_CHANGE,
    target
  }
}




// Logs the user out
export function logoutUser() {
  return dispatch => {
    dispatch(requestLogout());
    localStorage.removeItem('token');
    document.cookie = 'token=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    dispatch(receiveLogout());
  };
}

export function loginUser(creds) {
  let username = creds.login;
  let password = creds.password;

  
  const config = {
    method: 'POST',
    credentials: 'include',
    withCredentials:true,
    mode:'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password })
  };
  
  console.log(creds);
  return dispatch => {
    // We dispatch requestLogin to kickoff the call to the API
    dispatch(requestLogin(creds));
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/account/login`, config)
      .then(response => response.json().then(user => ({ user, response })))
      .then(({ user, response }) => {
        if (!response.ok) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(loginError(user.message));
          return Promise.reject(user);
        }

        toast(
          {
              title: 'Inicio de sesión',
              description: "Inicio de sesión exitoso"
          });
        // in posts create new action and check http status, if malign logout
        // If login was successful, set the token in local storage
        localStorage.setItem('token', user.token);
        localStorage.setItem('name',user.name);

        setTimeout(()=>{
         dispatch(logoutUser())    
         toast(
          {
              title: 'Sessión vencida',
              description: "Se ha vencido la sesión"
          });

        },600000)
        
        // Dispatch the success action
        dispatch(receiveLogin(user));
        return Promise.resolve(user);
      })
      .catch(err => {
        if(err.status ==='Ok'){
          toast(
            {
                title: 'Error',
                description: "Error de credenciales",
                type:'error',
                icon:'key'
            });
        }else{
          toast(
            {
                title: 'Error',
                description: "Error de comunicación con el servidor.",
                type:'error'
            });
        }

      });
  
      // } else {
    //   localStorage.setItem('id_token', appConfig.id_token);
    //   dispatch(receiveLogin({id_token: appConfig.id_token}))
    // }
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
  };
  
  return dispatch => {
    // We dispatch requestLogin to kickoff the call to the API
    dispatch(requestChangePassword(creds));
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/session/changepassword`, config)
      .then(response => response.json().then(user => ({ user, response })))
      .then(({ user, response }) => {
        if (!response.ok) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(changePasswordError(user.message));
          return Promise.reject(user);
        }
        // in posts create new action and check http status, if malign logout
        // If login was successful, set the token in local storage
        // Dispatch the success action
        dispatch(changePasswordSuccess(user));
        return Promise.resolve(user);
      })
      .catch(err => console.error('Error: ', err));
  
      // } else {
    //   localStorage.setItem('id_token', appConfig.id_token);
    //   dispatch(receiveLogin({id_token: appConfig.id_token}))
    // }
  };
}



export function createUser(userInfo) {
  console.log(userInfo)
  const config = {
    method: 'post',
    credentials: 'include',
    withCredentials:true,
    mode:'cors',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(userInfo),
  };
  
  return dispatch => {
    // We dispatch requestLogin to kickoff the call to the API
    dispatch(requestCreateUser(userInfo));
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/account/create`, config)
      .then(response => response.json().then(message => ({ message, response })))
      .then(({ message, response }) => {
        if (!response.ok) {
          dispatch(createUserError(message));
          return Promise.reject(message);
        }
        
        dispatch(createUserSuccess(message));
        return Promise.resolve(message);
      })
      .catch(err => console.error('Error: ', err));
  
      // } else {
    //   localStorage.setItem('id_token', appConfig.id_token);
    //   dispatch(receiveLogin({id_token: appConfig.id_token}))
    // }
  };
}

