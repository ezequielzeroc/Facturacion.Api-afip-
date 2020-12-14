import { act } from 'react-dom/test-utils';
import {

    
    FETCH_REQUEST,
    FETCH_SUCCESS,
    FETCH_FAILURE,

  } from '../actions/documentType';


  export default function documentType(
    state = {
      isFetching: false,
      DocumentType:[],
    },
    action,
  ) {
    console.log(action.vatCondition)
    switch (action.type) {
        case FETCH_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case FETCH_SUCCESS:
            return Object.assign({}, state, {
              isFetching: false,
              DocumentType: action.DocumentType,
            });
          case FETCH_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message: 'Ha ocurrido un error, vuelva a intentar',
            });
          
        default:
        return state;
    }
}