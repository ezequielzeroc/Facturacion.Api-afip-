import { act } from 'react-dom/test-utils';
import {

    
    FETCH_REQUEST,
    FETCH_SUCCESS,
    FETCH_FAILURE,
    MOVEMENT_IN,
    MOVEMENT_OUT,
    PENDING,
    MARK_PAID_REQUEST,
    MARK_PAID_SUCCESS,
    MARK_PAID_FAILURE,

  } from '../actions/FinancialMovements';


  export default function financialMovements(
    state = {
      isFetching: false,
      movements:[],
      In:0.00,
      Out:0.00,
      Pending:0.00,
      Balance:0.00,
      paidResult:false,
    },
    action,
  ) {
    
    switch (action.type) {
        case FETCH_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case FETCH_SUCCESS:
            if(action.movements !== undefined)
            {
              switch(action.movements.type){
                case MOVEMENT_IN:
                  return Object.assign({}, state, {
                    isFetching: false,
                    In: action.movements.amount,
                  });
                case MOVEMENT_OUT:
                  return Object.assign({}, state, {
                    isFetching: false,
                    Out: action.movements.amount,
                  });     
                case PENDING:
                  return Object.assign({}, state, {
                    isFetching: false,
                    Pending: action.movements.amount,
                  });               
              }
            }

          case FETCH_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message: 'Ha ocurrido un error, vuelva a intentar',
            });

            case MARK_PAID_REQUEST:
              return Object.assign({}, state, {
                isFetching: true,
              });
            case MARK_PAID_SUCCESS:
              return Object.assign({}, state, {
                isFetching: false,
                paidResult: action.result,
              });
            case MARK_PAID_FAILURE:
              return Object.assign({}, state, {
                isFetching: false,
                message: action.message,
              });           
          
        default:
        return state;
    }
}