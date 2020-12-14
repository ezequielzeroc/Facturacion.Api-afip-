import { act } from 'react-dom/test-utils';
import {

    
    FETCH_VATCONDITION_REQUEST,
    FETCH_VATCONDITION_SUCCESS,
    FETCH_VATCONDITION_FAILURE,

  } from '../actions/vatCondition';


  export default function vatCondition(
    state = {
      isFetching: false,
      vatCondition:[],
    },
    action,
  ) {
    console.log(action.vatCondition)
    switch (action.type) {
        case FETCH_VATCONDITION_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case FETCH_VATCONDITION_SUCCESS:
            return Object.assign({}, state, {
              isFetching: false,
              vatCondition: action.VatCondition,
            });
          case FETCH_VATCONDITION_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message: 'Ha ocurrido un error, vuelva a intentar',
            });
          
        default:
        return state;
    }
}