import SysConfig from '../config';

export const FETCH_VATCONDITION_REQUEST = 'FETCH_VATCONDITION_REQUEST';
export const FETCH_VATCONDITION_SUCCESS = 'FETCH_VATCONDITION_SUCCESS';
export const FETCH_VATCONDITION_FAILURE = 'FETCH_VATCONDITION_FAILURE';


function requestFetchVatCondition() {
    return {
      type: FETCH_VATCONDITION_REQUEST,
      isFetching: true,
    };
  }

  function fetchVatConditionSuccess(VatCondition) {
    return {
      type: FETCH_VATCONDITION_SUCCESS,
      isFetching: false,
      VatCondition:VatCondition,
    };
  }
  function fetchVatConditionError(message) {
    return {
      type: FETCH_VATCONDITION_FAILURE,
      isFetching: false,
      message,
    };
  }


  export function fetchVatCondition() {
  
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
      dispatch(requestFetchVatCondition());
  
      return fetch(`${SysConfig.base_url}/VatCondition/`, config)
        .then(response =>
          response.json().then(responseJson => ({
            VatCondition: responseJson
          })),
        )
        .then(({ VatCondition, responseJson }) => {
          if (!VatCondition) {
            // If there was a problem, we want to
            // dispatch the error condition
            dispatch(fetchVatConditionError("Error al intentar recuperar listado puntos de venta"));
            return Promise.reject(VatCondition);
          }
          // Dispatch the success action
          dispatch(fetchVatConditionSuccess(VatCondition));
          return Promise.resolve(VatCondition);
        })
        .catch(err => console.error('Error: ', err));
    };
  }