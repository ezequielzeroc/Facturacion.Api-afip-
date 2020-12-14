import React from 'react';
import { Switch, Route, Redirect } from 'react-router';
import { HashRouter } from 'react-router-dom';
import 'semantic-ui-css/semantic.min.css'
import {Dimmer,Loader} from 'semantic-ui-react'
import jwt from 'jsonwebtoken';
import { connect } from 'react-redux';
import { SemanticToastContainer } from 'react-semantic-toasts';
import 'react-semantic-toasts/styles/react-semantic-alert.css';
import { logoutUser } from '../actions/account';
import { register } from '../serviceWorker';
import {unregister} from '../interceptor'
import Layout from './layout/'
const loading = () =>  (<Dimmer active><Loader content='Cargando software' /></Dimmer>);

const isAuthenticated = (token) => {
  // // We check if app runs with backend mode
  // if (!config.isBackend && token) return true;
  if (!token) return;
  const date = new Date().getTime() / 1000;
  const data = jwt.decode(token);
  return date < data.exp;
}

const PrivateRoute = ({dispatch, component, ...rest }) => {
  if (!isAuthenticated(localStorage.getItem('token'))) {
    console.log(localStorage.getItem('token'));
      dispatch(logoutUser());
      return (<Redirect to="/account/login"/>);
  } else {
      return ( // eslint-disable-line
          <Route {...rest} render={props => (React.createElement(component, props))}/>
      );
  }
};

const Login = React.lazy(()=>import('../pages/account/login'));
const Register = React.lazy(()=>import('../pages/account/register'));
class App extends React.PureComponent{
  
  render(){
      return (
    <div className="App">
      <SemanticToastContainer position='top-center'  animation='fly down' />
      <HashRouter>
        <React.Suspense fallback={loading()}>
          <Switch>
              <Route path='/account/login' exact component={Login}></Route>
              <Route path='/account/register' exact component={Register}></Route>
              <PrivateRoute path="/" dispatch={this.props.dispatch} component={Layout}/>
          </Switch>
        </React.Suspense>
      </HashRouter>
    </div>
          );
        }
    
    }

const mapStateToProps = state => ({
  isAuthenticated: state.auth.isAuthenticated,
});
export default connect(mapStateToProps)(App);
