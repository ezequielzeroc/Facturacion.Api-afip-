
import SysConfig from '../config';

export const CREATE_REQUEST = 'CREATE_REQUEST';
export const CREATE_SUCCESS = 'CREATE_SUCCESS';
export const CREATE_FAILURE = 'CREATE_FAILURE';

export const DELETE_REQUEST = 'DELETE_REQUEST';
export const DELETE_SUCCESS = 'DELETE_SUCCESS';
export const DELETE_FAILURE = 'DELETE_FAILURE';

export const FETCH_REQUEST = 'FETCH_REQUEST';
export const FETCH_SUCCESS = 'FETCH_SUCCESS';
export const FETCH_FAILURE = 'FETCH_FAILURE';

function requestCreate() {
  return {
    type: CREATE_REQUEST,
    isFetching: true,
  };
}

function createSuccess(result) {
  console.log(result)
  return {
    type: CREATE_SUCCESS,
    isFetching: false,
    cae: result.cae,
    dueDateCae: result.dueDateCae,
    result: result.result,
    invoiceId: result.invoiceID,
    observations:result.observations,
    
  };
}
function createError(message) {
  return {
    type: CREATE_FAILURE,
    isFetching: false,
    message,
  };
}



function requestDelete() {
  return {
    type: DELETE_REQUEST,
    isFetching: true,
  };
}

function deleteSuccess(result) {
  return {
    type: DELETE_SUCCESS,
    isFetching: false,
    
  };
}
function deleteError(message) {
  return {
    type: DELETE_FAILURE,
    isFetching: false,
    message,
  };
}


function requestFetch() {
  return {
    type: FETCH_REQUEST,
    isFetching: true,
  };
}

function FetchSuccess(invoices) {
  return {
    type: FETCH_SUCCESS,
    isFetching: false,
    Invoices: invoices
  };
}
function FetchError(message) {
  return {
    type: FETCH_FAILURE,
    isFetching: false,
    message,
  };
}






export function CreateInvoice(invoice) {
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
    dispatch(requestCreate(invoice));
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/invoice/create`, config)
      .then(response => response.json().then(invoice => ({ invoice, response })))
      .then(({ invoice, response }) => {
        if (!response.ok) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(createError(invoice.message));
          return Promise.reject(invoice);
        }
        // Dispatch the success action
        dispatch(createSuccess(invoice));
       
        return Promise.resolve(invoice);
      })
      .catch(err => console.error('Error: ', err));
    // } else {
    //   dispatch(createPostError(''));
    //   return Promise.reject();
    // }
  };
}


export function deleteInvoice(id) {
  const config = {
    method: 'DELETE',
    credentials: 'include',
    withCredentials:true,
    mode:'cors',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
  };

  return dispatch => {
    // We dispatch requestCreatePost to kickoff the call to the API
    dispatch(requestDelete());
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/invoice/delete/${id}`, config)
      .then(response => response.json().then(invoice => ({ invoice, response })))
      .then(({ invoice, response }) => {
        if (!response.ok) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(deleteError(invoice.message));
          return Promise.reject(invoice);
        }
        // Dispatch the success action
        dispatch(deleteSuccess(true));
       
        return Promise.resolve(invoice);
      })
      .catch(err => console.error('Error: ', err));
    // } else {
    //   dispatch(createPostError(''));
    //   return Promise.reject();
    // }
  };
}



export function fetchInvoices() {
  
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

    return fetch(`${SysConfig.base_url}/invoice/list`, config)
      .then(response =>
        response.json().then(responseJson => ({
          Invoices: responseJson
        })),
      )
      .then(({ Invoices, responseJson }) => {
        if (!Invoices) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(FetchError("Error al intentar recuperar listado puntos de venta"));
          return Promise.reject(Invoices);
        }
        // Dispatch the success action
        dispatch(FetchSuccess(Invoices));
        return Promise.resolve(Invoices);
      })
      .catch(err => console.error('Error: ', err));
  };
}

export function downloadInvoice(id) {
  const config = {
    method: 'get',
    withCredentials:true,
    mode:'cors',
    responseType: 'blob',
    headers: {
     responseType: 'blob'
    },
    credentials: 'include',

  };

  const link = document.createElement('a');
    const url = `${SysConfig.base_url}/invoice/download/${id}`;
      link.href = url;
      link.setAttribute('download', 'file.pdf');
      document.body.appendChild(link);
      link.click();
      link.remove();

  // return dispatch => {
  //   dispatch(requestDownloadInvoice());

  //   return fetch(`${SysConfig.base_url}/invoices/download/${id}`, config)
  //   .then(function(response) {
  //     const disposition = response.headers['content-disposition'];
  //     const url = window.URL.createObjectURL(new Blob([response.data]));
  //     const link = document.createElement('a');
  //     link.href = url;
  //     link.setAttribute('download', 'file.pdf');
  //     document.body.appendChild(link);
  //     link.click();
  //     return response.blob();
  //   })
  //     .catch(err => console.error('Error: ', err));
  // };
}