import {
    CREATE_REQUEST, CREATE_SUCCESS, CREATE_FAILURE,
    DELETE_REQUEST, DELETE_SUCCESS, DELETE_FAILURE,
    FETCH_REQUEST, FETCH_SUCCESS, FETCH_FAILURE,
    CANCEL_REQUEST, CANCEL_SUCCESS, CANCEL_FAILURE,
    SHOW_SEND_EMAIL,HIDE_SEND_EMAIL, 
    SEND_REQUEST, SEND_FAILURE,SEND_SUCCESS, SET_INVOICE_ID
  } from '../actions/invoices';
  
  export default function invoices(state = {
    isFetching: false,
    Invoices:[],
    cae:'',
    result:'',
    invoiceId:0,
    errorMessage:'',
    observations:[],
    sendEmailShow:false
  }, action) {
    switch (action.type) {
        case CREATE_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case CREATE_SUCCESS:
            return Object.assign({}, state, {
              isFetching: false,
              cae: action.cae,
              dueDateCae: action.dueDateCae,
              result: action.result,
              invoiceId: action.invoiceId,
              observations: action.observations,
            });
          case CREATE_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message: 'Ha ocurrido un error, vuelva a intentar',
            });
            case CANCEL_REQUEST:
              return Object.assign({}, state, {
                isFetching: true,
              });
            case CANCEL_SUCCESS:
              return Object.assign({}, state, {
                isFetching: false,
                cae: action.cae,
                dueDateCae: action.dueDateCae,
                result: action.result,
                invoiceId: action.invoiceID,
                observations: action.observations,
              });
            case CANCEL_FAILURE:
              return Object.assign({}, state, {
                isFetching: false,
                message: 'Ha ocurrido un error, vuelva a intentar',
              });            
            case DELETE_REQUEST:
              return Object.assign({}, state, {
                isFetching: true,
              });
            case DELETE_SUCCESS:
              return Object.assign({}, state, {
                isFetching: false,
              });
            case DELETE_FAILURE:
              return Object.assign({}, state, {
                isFetching: false,
                message: 'Ha ocurrido un error, vuelva a intentar',
              });
          
            case FETCH_REQUEST:
              return Object.assign({}, state, {
                isFetching: true,
              });
            case FETCH_SUCCESS:
              return Object.assign({}, state, {
                isFetching: false,
                Invoices: action.Invoices

              });
            case FETCH_FAILURE:
              return Object.assign({}, state, {
                isFetching: false,
                message: 'Ha ocurrido un error, vuelva a intentar',
              });     
              case SEND_REQUEST:
                return Object.assign({}, state, {
                  isFetching: true,
                });
              case SEND_SUCCESS:
                return Object.assign({}, state, {
                  isFetching: false
  
                });
              case SEND_FAILURE:
                return Object.assign({}, state, {
                  isFetching: false,
                  message: 'Ha ocurrido un error, vuelva a intentar',
                });             
              
              case SHOW_SEND_EMAIL:
                return Object.assign({}, state, {
                  sendEmailShow:true
                })
                case HIDE_SEND_EMAIL:
                  return Object.assign({}, state, {
                    sendEmailShow:false
                  })
              case SET_INVOICE_ID:
                return Object.assign({}, state, {
                  invoiceId:action.invoiceId
                })
                              
        default:
            return state;
    }
  }
  