import {

    FETCH_POS_REQUEST,
    FETCH_POS_SUCCESS,
    FETCH_POS_FAILURE,
    FETCH_POSBYID_REQUEST,
    FETCH_POSBYID_SUCCESS,
    FETCH_POSBYID_FAILURE,
    DELETE_POS_REQUEST,
    DELETE_POS_SUCCESS,
    DELETE_POS_FAILURE,
    CREATE_POS_REQUEST,
    CREATE_POS_SUCCESS,
    CREATE_POS_FAILURE,
    UPDATE_POS_REQUEST,
    UPDATE_POS_SUCCESS,
    UPDATE_POS_FAILURE,
    HANDLE_CHANGE,
    RESET_FORM

  } from '../actions/pos';


  export default function pos(
    state = {
      isFetching: false,
      Pos:[],
      posLoaded:{
       posid:0, code:'',name:'',description:'',address:''
      }
    },
    action,
  ) {
    switch (action.type) {
        case FETCH_POS_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case FETCH_POS_SUCCESS:
            return Object.assign({}, state, {
              isFetching: false,
              Pos: action.Pos,
            });
          case FETCH_POS_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message: 'Ha ocurrido un error, vuelva a intentar',
            });
            case FETCH_POSBYID_REQUEST:
              return Object.assign({}, state, {
                isFetching: true,
              });
            case FETCH_POSBYID_SUCCESS:
              console.log(action)
              return Object.assign({}, state, {
                isFetching: false,
                posLoaded: action.posLoaded,
              });
            case FETCH_POSBYID_FAILURE:
              return Object.assign({}, state, {
                isFetching: false,
                message: 'Ha ocurrido un error, vuelva a intentar',
              });   
              case UPDATE_POS_REQUEST:
                return Object.assign({}, state, {
                  isFetching: true,
                });
              case UPDATE_POS_SUCCESS:
                return Object.assign({}, state, {
                  isFetching: false,
                  Pos: action.Pos,
                });
              case UPDATE_POS_FAILURE:
                return Object.assign({}, state, {
                  isFetching: false,
                  message: 'Ha ocurrido un error, vuelva a intentar',
                });    
                case CREATE_POS_REQUEST:
                  return Object.assign({}, state, {
                    isFetching: true,
                  });
                case CREATE_POS_SUCCESS:
                  return Object.assign({}, state, {
                    isFetching: false,
                    Pos: action.Pos,
                  });
                case CREATE_POS_FAILURE:
                  return Object.assign({}, state, {
                    isFetching: false,
                    message: 'Ha ocurrido un error, vuelva a intentar',
                  });          
                  case DELETE_POS_REQUEST:
                    return Object.assign({}, state, {
                      isFetching: true,
                    });
                  case DELETE_POS_SUCCESS:
                    return Object.assign({}, state, {
                      isFetching: false,
                      Pos: action.Pos,
                    });
                  case DELETE_POS_FAILURE:
                    return Object.assign({}, state, {
                      isFetching: false,
                      message: 'Ha ocurrido un error, vuelva a intentar',
                    });       
                    case HANDLE_CHANGE:
                      let {name, value }= action.target;
                      return Object.assign({}, state, {
                        posLoaded: Object.assign({}, state.posLoaded, {
                          [name]:value,
                        })
                   });     
                   case RESET_FORM:
                    return Object.assign({}, state, {
                      posLoaded: Object.assign({}, state.posLoaded, {
                        code:'',
                        name:'',
                        description:'',
                        address:'',
                      })
                 });                              
                
        default:
        return state;
    }
}