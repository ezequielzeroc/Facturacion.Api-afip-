import React, { Component } from 'react'
import { Button, Form, Grid, Header, Image, Message, Segment } from 'semantic-ui-react'
// import logo from '../../../logo.png'
import { connect } from 'react-redux';
import { withRouter, Redirect } from 'react-router-dom';
import { createUser } from '../../../actions/account'
import {  toast } from 'react-semantic-toasts';
import PropTypes from 'prop-types';

class RegisterForm extends Component{
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
          password: '',
          lastName:'',
          name:'',
          userName:'',
          email:''
        };
      }
    
      changeLogin = (event) => {
        this.setState({login: event.target.value});
      }
    
      changePassword = (event) => {
        this.setState({password: event.target.value});
      }
      change = (event) => {
        let {name,value} = event.target;

      this.setState({
          [name]:value
      });
    }
    
      doCreate = (e) => {
        this.props.dispatch(
          createUser({
           name:this.state.name,
           lastName:this.state.lastName,
           userName:this.state.userName,
           email:this.state.email,
           password:this.state.password

          }),
        );
        e.preventDefault();
      }

    render(){
      let {email,password,lastName,userName,name} = this.state;
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
            <Form onSubmit={this.doCreate} size='large'>
                <Segment stacked>
                <Form.Input onChange={this.change} value={name} name='name' fluid icon='user' iconPosition='left' placeholder='Nombre'  />
                <Form.Input onChange={this.change} value={lastName} name='lastName' fluid icon='user' iconPosition='left' placeholder='Apellido'  />
                <Form.Input onChange={this.change} value={userName} name='userName' fluid icon='user' iconPosition='left' placeholder='Usuario'  />
                <Form.Input onChange={this.change} value={email} name='email' fluid icon='envelope' iconPosition='left' placeholder='E-Mail'  />
                <Form.Input onChange={this.change} value={password} name='password'
                    fluid
                    icon='lock'
                    iconPosition='left'
                    placeholder='Contraseña'
                    type='password'
                />
                <Form.Input onChange={this.change} value={password} name='password'
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
                Ya tengo una cuenta registrada, <a href='#/account/login'>iniciar sesión</a>
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
export default withRouter(connect(mapStateToProps)(RegisterForm));