import SysConfig from '../config';

export const FETCH_REQUEST = 'FETCH_REQUEST';
export const FETCH_SUCCESS = 'FETCH_SUCCESS';
export const FETCH_FAILURE = 'FETCH_FAILURE';


function requestFetch() {
    return {
      type: FETCH_REQUEST,
      isFetching: true,
    };
  }

  function fetchSuccess(DocumentType) {
    return {
      type: FETCH_SUCCESS,
      isFetching: false,
      DocumentType:DocumentType,
    };
  }
  function fetchError(message) {
    return {
      type: FETCH_FAILURE,
      isFetching: false,
      message,
    };
  }


  export function fetchDocumentType() {
  
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
  
      return fetch(`${SysConfig.base_url}/documentType/`, config)
        .then(response =>
          response.json().then(responseJson => ({
            DocumentType: responseJson
          })),
        )
        .then(({ DocumentType, responseJson }) => {
          if (!DocumentType) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(fetchError("Error al intentar recuperar listado."));
            return Promise.reject(DocumentType);
          }
          // Dispatch the success action
          dispatch(fetchSuccess(DocumentType));
          return Promise.resolve(DocumentType);
        })
        .catch(err => console.error('Error: ', err));
    };
  }