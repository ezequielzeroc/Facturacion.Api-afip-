import SysConfig from '../config';

export const FETCH_REQUEST = 'FETCH_REQUEST';
export const FETCH_SUCCESS = 'FETCH_SUCCESS';
export const FETCH_FAILURE = 'FETCH_FAILURE';

export const MARK_PAID_REQUEST = "MARK_PAID_REQUEST"
export const MARK_PAID_SUCCESS = "MARK_PAID_SUCCESS"
export const MARK_PAID_FAILURE = "MARK_PAID_FAILURE"


export const MOVEMENT_IN = 1;
export const MOVEMENT_OUT = 2;
export const PENDING = 3;

function requestFetch() {
    return {
      type: FETCH_REQUEST,
      isFetching: true,
    };
  }

  function fetchSuccess(movements) {
    return {
      type: FETCH_SUCCESS,
      isFetching: false,
      movements,
    };
  }
  function fetchError(message) {
    return {
      type: FETCH_FAILURE,
      isFetching: false,
      message,
    };
  }

  function requestMarkPaid() {
    return {
      type: MARK_PAID_REQUEST,
      isFetching: true,
    };
  }

  function markPaidSuccess(result) {
    return {
      type: MARK_PAID_SUCCESS,
      isFetching: false,
      result,
    };
  }
  function markPaidError(message) {
    return {
      type: MARK_PAID_FAILURE,
      isFetching: false,
      message,
    };
  }




  export function fetchMovements(type) {
  
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
      dispatch(requestFetch());
  
      return fetch(`${SysConfig.base_url}/financialMovements/get/${type}`, config)
        .then(response =>
          response.json().then(responseJson => ({
            Movements: responseJson
          })),
        )
        .then(({ Movements, responseJson }) => {
          if (!Movements) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(fetchError("Error al intentar recuperar listado."));
            return Promise.reject(Movements);
          }
          // Dispatch the success action
          dispatch(fetchSuccess({type:type,amount:Movements}));
          return Promise.resolve(Movements);
        })
        .catch(err => console.error('Error: ', err));
    };
  }


  export function markAsPaid(invoice) {
    console.log(invoice);
    const config = {
      method: 'post',
      credentials: 'include',
      withCredentials:true,
      mode:'cors',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(invoice),
    };
  
    return dispatch => {
      // We dispatch requestCreatePost to kickoff the call to the API
      dispatch(requestMarkPaid(invoice));
      // if(process.env.NODE_ENV === "development") {
      return fetch(`${SysConfig.base_url}/financialmovements/markAsPaid`, config)
        .then(response => response.json().then(invoice => ({ invoice, response })))
        .then(({ invoice, response }) => {
          if (!response.ok) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(markPaidError("Error al intentar guardar estado"));
            return Promise.reject(invoice);
          }
          // Dispatch the success action
          dispatch(markPaidSuccess(invoice));
         
          return Promise.resolve(invoice);
        })
        .catch(err => console.error('Error: ', err));
      // } else {
      //   dispatch(createPostError(''));
      //   return Promise.reject();
      // }
    };
  }
  


  export function finalAction(actions) {
    console.log(actions);
    const config = {
      method: 'post',
      credentials: 'include',
      withCredentials:true,
      mode:'cors',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(actions),
    };
  
    return dispatch => {
      // We dispatch requestCreatePost to kickoff the call to the API
      dispatch(requestMarkPaid(actions));
      // if(process.env.NODE_ENV === "development") {
      return fetch(`${SysConfig.base_url}/invoices/finalaction`, config)
        .then(response => response.json().then(invoice => ({ invoice, response })))
        .then(({ invoice, response }) => {
          if (!response.ok) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(markPaidError("Error al intentar guardar estado"));
            return Promise.reject(invoice);
          }
          // Dispatch the success action
          dispatch(markPaidSuccess(invoice));
         
          return Promise.resolve(invoice);
        })
        .catch(err => console.error('Error: ', err));
      // } else {
      //   dispatch(createPostError(''));
      //   return Promise.reject();
      // }
    };
  }