
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

export const CANCEL_REQUEST = 'CANCEL_REQUEST';
export const CANCEL_SUCCESS = 'CANCEL_SUCCESS';
export const CANCEL_FAILURE = 'CANCEL_FAILURE';

export const SHOW_SEND_EMAIL = 'SHOW_SEND_EMAIL';
export const HIDE_SEND_EMAIL = 'HIDE_SEND_EMAIL';

export const SET_INVOICE_ID = 'SET_INVOICE_ID';

export const SEND_REQUEST = 'SEND_REQUEST';
export const SEND_SUCCESS = 'SEND_SUCCESS';
export const SEND_FAILURE = 'SEND_FAILURE';



function requestCreate() {
  return {
    type: CREATE_REQUEST,
    isFetching: true,
  };
}

export function showSendEmail(){
  return{
    type:SHOW_SEND_EMAIL,
  }
}


export function setInvoiceID(invoiceId){
  return{
    type:SET_INVOICE_ID,
    invoiceId:invoiceId
  }
}
export function hideSendEmail(){
  return{
    type:HIDE_SEND_EMAIL,
  }
}


function createSuccess(result) {
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


function requestCancel() {
  return {
    type: CANCEL_REQUEST,
    isFetching: true,
  };
}

function cancelSuccess(result) {
  console.log(result)
  return {
    type: CANCEL_SUCCESS,
    isFetching: false,
    cae: result.cae,
    dueDateCae: result.dueDateCae,
    result: result.result,
    invoiceId: result.invoiceID,
    observations:result.observations,
    
  };
}
function cancelError(message) {
  return {
    type: CANCEL_FAILURE,
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



function requestSend() {
  return {
    type: SEND_REQUEST,
    isFetching: true,
  };
}

function sendSuccess(result) {
  return {
    type: SEND_SUCCESS,
    isFetching: false,
    
  };
}
function sendError(message) {
  return {
    type: SEND_FAILURE,
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


export function CancelInvoice(invoice) {
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
    dispatch(requestCancel(invoice));
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/invoice/cancelinvoice`, config)
      .then(response => response.json().then(invoice => ({ invoice, response })))
      .then(({ invoice, response }) => {
        if (!response.ok) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(cancelError(invoice.message));
          return Promise.reject(invoice);
        }
        // Dispatch the success action
        dispatch(cancelSuccess(invoice));
       
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

export function SendInvoice(data) {
  console.log(JSON.stringify(data))
  const config = {
    method: 'post',
    credentials: 'include',
    withCredentials:true,
    mode:'cors',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data),
  };

  return dispatch => {
    // We dispatch requestCreatePost to kickoff the call to the API
    dispatch(requestSend(data));
    // if(process.env.NODE_ENV === "development") {
    return fetch(`${SysConfig.base_url}/sender/addSend`, config)
      .then(response => response.json().then(data => ({ data, response })))
      .then(({ data, response }) => {
        if (!response.ok) {
          // If there was a problem, we want to
          // dispatch the error condition
          dispatch(sendError(data.message));
          return Promise.reject(data);
        }
        // Dispatch the success action
        dispatch(sendSuccess(data));
       
        return Promise.resolve(data);
      })
      .catch(err => console.error('Error: ', err));
    // } else {
    //   dispatch(createPostError(''));
    //   return Promise.reject();
    // }
  };
}