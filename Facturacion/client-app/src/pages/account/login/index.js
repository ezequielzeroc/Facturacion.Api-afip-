import React, { Component } from 'react'
import { Button, Form, Grid, Header, Image, Message, Segment } from 'semantic-ui-react'
// import logo from '../../../logo.png'
import { connect } from 'react-redux';
import { withRouter, Redirect } from 'react-router-dom';
import { loginUser } from '../../../actions/account'
import {  toast } from 'react-semantic-toasts';
import PropTypes from 'prop-types';

class LoginForm extends Component{
    // static propTypes = {
    //     dispatch: PropTypes.func.isRequired,
    //     isAuthenticated: PropTypes.bool,
    //     isFetching: PropTypes.bool,
    //     location: PropTypes.any, // eslint-disable-line
    //     errorMessage: PropTypes.string,
    //   };
    
    
    //   static defaultProps = {
    //     isAuthenticated: false,
    //     isFetching: false,
    //     location: {},
    //     errorMessage: null,
    //   };
    
      constructor(props) {
        super(props);
    
        this.state = {
          login: '',
          password: '',
        };
      }
    
      changeLogin = (event) => {
        this.setState({login: event.target.value});
      }
    
      changePassword = (event) => {
        this.setState({password: event.target.value});
      }
    
      doLogin = (e) => {
        this.props.dispatch(
          loginUser({
            login: this.state.login,
            password: this.state.password,
          }),
        );
        e.preventDefault();
      }

    render(){
        let {login,password} = this.state;
        const {from} = this.props.location.state || {
            from: {pathname: '/'},
          };
      
          if (this.props.isAuthenticated) {
            // cant access login page while logged in
            return <Redirect to={from} />;
          }
        return(

            <Grid textAlign='center' style={{ height: '100vh' }} verticalAlign='middle'>
                <Grid.Column style={{ maxWidth: 450 }}>
                {/* <Image centered size='small' src={logo} />  */}
                <br></br>
            <Form onSubmit={this.doLogin} size='large'>
                <Segment stacked>
                <Form.Input onChange={this.changeLogin} value={login} name='email' fluid icon='user' iconPosition='left' placeholder='Usuario'  />
                <Form.Input onChange={this.changePassword} value={password} name='password'
                    fluid
                    icon='lock'
                    iconPosition='left'
                    placeholder='Contraseña'
                    type='password'
                />

                <Button disabled={this.props.isFetching} color='teal' fluid size='large'>
                    {this.props.isFetching? 'Accediendo':'Acceder'}
                </Button>
                </Segment>
            </Form>
            <Message>
                ¿Aún no tienes cuenta? <a href='#/account/register'>registrar</a>
            </Message>
            </Grid.Column>
            </Grid>

        )
    }
  
}

function mapStateToProps(state) {
    return {
        isFetching: state.account.isFetching,
        isAuthenticated: state.account.isAuthenticated,
        errorMessage: state.account.errorMessage,
    };
}
export default withRouter(connect(mapStateToProps)(LoginForm));