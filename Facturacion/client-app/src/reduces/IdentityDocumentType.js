import { act } from 'react-dom/test-utils';
import {

    
    FETCH_IDENTITYDOCUMENT_REQUEST,
    FETCH_IDENTITYDOCUMENT_SUCCESS,
    FETCH_IDENTITYDOCUMENT_FAILURE,

  } from '../actions/identityDocumentType';


  export default function identityDocumentType(
    state = {
      isFetching: false,
      identityDocumentTypes:[],
    },
    action,
  ) {
    switch (action.type) {
        case FETCH_IDENTITYDOCUMENT_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case FETCH_IDENTITYDOCUMENT_SUCCESS:
            return Object.assign({}, state, {
              isFetching: false,
              identityDocumentTypes: action.IdentityDocumentType,
            });
          case FETCH_IDENTITYDOCUMENT_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message: 'Ha ocurrido un error, vuelva a intentar',
            });
          
        default:
        return state;
    }
}