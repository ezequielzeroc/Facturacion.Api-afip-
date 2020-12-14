import {
    CHANGE_PASSWORD_REQUEST,
    CHANGE_PASSWORD_SUCCESS,
    CHANGE_PASSWORD_FAILURE,
    LOGIN_REQUEST, 
    LOGIN_SUCCESS, 
    LOGIN_FAILURE, 
    LOGOUT_SUCCESS,
    CREATE_USER_SUCCESS,
    CREATE_USER_REQUEST,
    CREATE_USER_FAILURE,
    HANDLE_CHANGE
    }
    from '../actions/account.js'
    
    
    const token = localStorage.getItem('token');
    export default function account(
        state = {
          isFetching: false,
          changed:false,
          isAuthenticated: !!token,
          created:false,
          message:'',
          loginForm:''
        },
        action,
      ) {
        switch (action.type) {
          case CHANGE_PASSWORD_REQUEST:
            return Object.assign({}, state, {
              isFetching: true,
            });
          case CHANGE_PASSWORD_SUCCESS:
            return Object.assign({}, state, {
              isFetching: false,
              message: '',
              changed: true
            });
          case CHANGE_PASSWORD_FAILURE:
            return Object.assign({}, state, {
              isFetching: false,
              message:
                'Ocurrió un error al intentar cambiar la contraseña.',
            });
            case CREATE_USER_REQUEST:
              return Object.assign({}, state, {
                isFetching: true,
                created:false,
                message:'',
              });
            case CREATE_USER_SUCCESS:

              return Object.assign({}, state, {
                isFetching: false,
                message: action.message,
                created: true
              });
            case CREATE_USER_FAILURE:
              return Object.assign({}, state, {
                isFetching: false,
                message: action.message,
                created:false,
                  
              });            
            case LOGIN_REQUEST:
                return Object.assign({}, state, {
                    isFetching: true,
                    isAuthenticated: false,
                });
            case LOGIN_SUCCESS:
                return Object.assign({}, state, {
                    isFetching: false,
                    isAuthenticated: true,
                    errorMessage: '',
                });
            case LOGIN_FAILURE:
                return Object.assign({}, state, {
                    isFetching: false,
                    isAuthenticated: false,
                    errorMessage: action.payload,
                });
            case LOGOUT_SUCCESS:
                return Object.assign({}, state, {
                    isAuthenticated: false,
                });          
                case HANDLE_CHANGE:
                  let {name, value }= action.target;
                  return Object.assign({}, state, {
                    loginForm: Object.assign({}, state.loginForm, {
                      [name]:value,
                    })
               });      
          default:
            return state;
        }
      }