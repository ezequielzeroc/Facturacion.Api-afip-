import SysConfig from '../config';

export const FETCH_IDENTITYDOCUMENT_REQUEST = 'FETCH_IDENTITYDOCUMENT_REQUEST';
export const FETCH_IDENTITYDOCUMENT_SUCCESS = 'FETCH_IDENTITYDOCUMENT_SUCCESS';
export const FETCH_IDENTITYDOCUMENT_FAILURE = 'FETCH_IDENTITYDOCUMENT_FAILURE';


function requestFetchIdentityDocumentType() {
    return {
      type: FETCH_IDENTITYDOCUMENT_REQUEST,
      isFetching: true,
    };
  }

  function fetchIdentityDocumentTypeSuccess(IdentityDocumentType) {
    return {
      type: FETCH_IDENTITYDOCUMENT_SUCCESS,
      isFetching: false,
      IdentityDocumentType:IdentityDocumentType,
    };
  }
  function fetchIdentityDocumentTypeError(message) {
    return {
      type: FETCH_IDENTITYDOCUMENT_FAILURE,
      isFetching: false,
      message,
    };
  }


  export function fetchIdentityDocumentType() {
  
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
      dispatch(requestFetchIdentityDocumentType());
  
      return fetch(`${SysConfig.base_url}/IdentityDocumentType/`, config)
        .then(response =>
          response.json().then(responseJson => ({
            IdentityDocumentType: responseJson
          })),
        )
        .then(({ IdentityDocumentType, responseJson }) => {
          if (!IdentityDocumentType) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(fetchIdentityDocumentTypeError("Error al intentar recuperar listado puntos de venta"));
            return Promise.reject(IdentityDocumentType);
          }
          // Dispatch the success action
          dispatch(fetchIdentityDocumentTypeSuccess(IdentityDocumentType));
          return Promise.resolve(IdentityDocumentType);
        })
        .catch(err => console.error('Error: ', err));
    };
  }