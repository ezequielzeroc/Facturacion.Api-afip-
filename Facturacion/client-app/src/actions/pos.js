import SysConfig from '../config';

export const FETCH_POS_REQUEST = 'FETCH_POS_REQUEST';
export const FETCH_POS_SUCCESS = 'FETCH_POS_SUCCESS';
export const FETCH_POS_FAILURE = 'FETCH_POS_FAILURE';
//TO FECTCH ONLY ONE
export const FETCH_POSBYID_REQUEST = 'FETCH_POSBYID_REQUEST';
export const FETCH_POSBYID_SUCCESS = 'FETCH_POSBYID_SUCCESS';
export const FETCH_POSBYID_FAILURE = 'FETCH_POSBYID_FAILURE';
//TO CREATE
export const CREATE_POS_REQUEST = 'FETCH_POS_REQUEST';
export const CREATE_POS_SUCCESS = 'FETCH_POS_SUCCESS';
export const CREATE_POS_FAILURE = 'FETCH_POS_FAILURE';

//TO UPDATE
export const UPDATE_POS_REQUEST = 'UPDATE_POS_REQUEST';
export const UPDATE_POS_SUCCESS = 'UPDATE_POS_SUCCESS';
export const UPDATE_POS_FAILURE = 'UPDATE_POS_FAILURE';
//TO DELETE
export const DELETE_POS_REQUEST = 'DELETE_POS_REQUEST';
export const DELETE_POS_SUCCESS = 'DELETE_POS_SUCCESS';
export const DELETE_POS_FAILURE = 'DELETE_POS_FAILURE';

export const HANDLE_CHANGE = 'HANDLE_CHANGE';

export const RESET_FORM = 'RESET_FORM';

function requestUpdatePos() {
  return {
    type: UPDATE_POS_REQUEST,
    isFetching: true,
  };
}

function updatePosSuccess() {
  return {
    type: UPDATE_POS_SUCCESS,
    isFetching: false,
  };
}
function updatePosError(message) {
  return {
    type: UPDATE_POS_FAILURE,
    isFetching: false,
    message,
  };
}

function requestDeletePos() {
  return {
    type: DELETE_POS_REQUEST,
    isFetching: true,
  };
}

function deletePosSuccess() {
  return {
    type: DELETE_POS_SUCCESS,
    isFetching: false,
  };
}
function deletePosError(message) {
  return {
    type: DELETE_POS_FAILURE,
    isFetching: false,
    message,
  };
}

function requestCreatePos() {
  return {
    type: CREATE_POS_REQUEST,
    isFetching: true,
  };
}

function createPosSuccess() {
  return {
    type: CREATE_POS_SUCCESS,
    isFetching: false,
  };
}
function createPosError(message) {
  return {
    type: CREATE_POS_FAILURE,
    isFetching: false,
    message,
  };
}

function requestFetchPos() {
    return {
      type: FETCH_POS_REQUEST,
      isFetching: true,
    };
  }

  function fetchPosSuccess(Pos) {
    return {
      type: FETCH_POS_SUCCESS,
      isFetching: false,
      Pos:Pos,
    };
  }
  function fetchPosError(message) {
    return {
      type: FETCH_POS_FAILURE,
      isFetching: false,
      message,
    };
  }

  function requestFetchPosById() {
    return {
      type: FETCH_POSBYID_REQUEST,
      isFetching: true,
    };
  }

  function fetchPosByIdSuccess(Pos) {
    return {
      type: FETCH_POSBYID_SUCCESS,
      isFetching: false,
      posLoaded:Pos,
    };
  }

  function fetchPosByIdError(message) {
    return {
      type: FETCH_POSBYID_FAILURE,
      isFetching: false,
      message,
    };
  }

  export function handleChange(target){
    return{
      type:HANDLE_CHANGE,
      target
    }
  }

  export function resetForm(){
    return{
      type:RESET_FORM,
    }
  }

  export function updatePos(pos) {
    const config = {
      method: 'PATCH',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(pos),
      credentials: 'include',
    };
  
    return dispatch => {
      // We dispatch requestCreatePost to kickoff the call to the API
      dispatch(requestUpdatePos(pos));
      // if(process.env.NODE_ENV === "development") {
      return fetch(`${SysConfig.base_url}/pos/`, config)
        .then(response => response.json().then(item => ({ pos, response })))
        .then(({ pos, response }) => {
          if (!response.ok) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(updatePosError(pos.message));
            return Promise.reject(pos);
          }
          // Dispatch the success action
          dispatch(updatePosSuccess());
   
          return Promise.resolve(pos);
        })
        .catch(err => console.error('Error: ', err));
      // } else {
      //   dispatch(createPostError(''));
      //   return Promise.reject();
      // }
    };
  }


  export function deletePos(id) {
    const config = {
      method: 'DELETE',
      credentials: 'include',
      withCredentials:true,
      mode:'cors',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      },
      credentials: 'include',
    };
  
    return dispatch => {
      dispatch(requestDeletePos());
  
      return fetch(`${SysConfig.base_url}/pos/${id}`, config)
        .then(response =>
          response.json().then(responseJson => ({
            pos: responseJson
          })),
        )
        .then(({ pos, responseJson }) => {
          if (!pos) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(deletePosError("Error al intentar eliminar el item seleccionado"));
            return Promise.reject(pos);
          }
          // Dispatch the success action
          dispatch(deletePosSuccess());
          return Promise.resolve(pos);
        })
        .catch(err => console.error('Error: ', err));
    };
  }


  export function createPos(pos) {
    const config = {
      method: 'post',
      credentials: 'include',
      withCredentials:true,
      mode:'cors',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(pos),
    };
  
    return dispatch => {
      // We dispatch requestCreatePost to kickoff the call to the API
      dispatch(requestCreatePos(pos));
      // if(process.env.NODE_ENV === "development") {
      return fetch(`${SysConfig.base_url}/pos/create`, config)
        .then(response => response.json().then(pos => ({ pos, response })))
        .then(({ pos, response }) => {
          if (!response.ok) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(createPosError(pos.message));
            return Promise.reject(pos);
          }
          // Dispatch the success action
          dispatch(createPosSuccess(pos));
         
          return Promise.resolve(pos);
        })
        .catch(err => console.error('Error: ', err));
      // } else {
      //   dispatch(createPostError(''));
      //   return Promise.reject();
      // }
    };
  }
  
  export function fetchPosById(id) {
  
    const config = {
      method: 'GET',
      credentials: 'include',
      withCredentials:true,
      mode:'cors',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      }
    };
    console.log("ID",id);
  
    return dispatch => {
  
      return fetch(`${SysConfig.base_url}/pos/${id}`, config)
        .then(response =>
          response.json().then(responseJson => ({
            Pos: responseJson
          })),
        )
        .then(({ Pos, responseJson }) => {
          if (!Pos) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(fetchPosByIdError("Error al intentar recuperar listado puntos de venta"));
            return Promise.reject(Pos);
          }
          // Dispatch the success action
          dispatch(fetchPosByIdSuccess(Pos));
          return Promise.resolve(Pos);
        })
        .catch(err => console.error('Error: ', err));
    };
  }

  export function fetchPos() {
  
    const config = {
      method: 'GET',
      credentials: 'include',
      withCredentials:true,
      mode:'cors',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      },
    };
  
    return dispatch => {
      dispatch(requestFetchPos());
  
      return fetch(`${SysConfig.base_url}/pos/list`, config)
        .then(response =>
          response.json().then(responseJson => ({
            Pos: responseJson
          })),
        )
        .then(({ Pos, responseJson }) => {
          if (!Pos) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(fetchPosError("Error al intentar recuperar listado puntos de venta"));
            return Promise.reject(Pos);
          }
          // Dispatch the success action
          dispatch(fetchPosSuccess(Pos));
          return Promise.resolve(Pos);
        })
        .catch(err => console.error('Error: ', err));
    };
  }