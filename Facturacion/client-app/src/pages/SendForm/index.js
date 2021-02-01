import React, { Component } from 'react'
import { connect } from 'react-redux';
import { Form, Header,Modal,Icon,Button } from 'semantic-ui-react'
import { hideSendEmail, SendInvoice } from '../../actions/invoices';


class SendForm extends Component {
    constructor(props){
        super(props);
        
        this.state = {
            showModal:false,
            toName:'',
            to:'',
            subject:'',
            priorityLevelOption: [
                { key: 'b', text: 'Baja', value: 'b' },
                { key: 'm', text: 'Media', value: 'm' },
                { key: 'a', text: 'Alta', value: 'a' },
              ],
            battachInvoice: 0,
            message:''
            
        }
    }

    handleChangeAttach = (e, { value }) => this.setState({ battachInvoice:value });
    handleChange = (e) => this.setState({[e.target.name]:e.target.value})

    handleSend = ()=>{
        let {to,subject,battachInvoice,message,toName} = this.state;
        let attachInvoice = (battachInvoice==='0'?true:false);
        let invoiceId = this.props.invoiceId;
        this.props.dispatch(
            SendInvoice({to,subject,attachInvoice,message,toName, invoiceId})
        )
        this.props.dispatch(
            hideSendEmail(),
        )
    }


    handleClose = (value)=>{
        this.props.dispatch(
            hideSendEmail(),
        )
    }

  render() {
    const { value, to, subject, priorityLevelOption, battachInvoice, message,toName} = this.state
    let {showModal } = this.props;
    return (


        <Modal
        size='small'
        closeIcon
        open={this.props.sendEmailShow}
        onClose={this.handleClose}
      >
        <Header icon='mail outline' content='Enviar factura' />
        <Modal.Content>
        <Form>
        <Form.Group>
        <Form.Input width='6' onChange={this.handleChange} fluid label='Nombre' name='toName' value={toName} />
          <Form.Input type='email'  width='6' onChange={this.handleChange} fluid label='Email:' name='to' value={to} />
          <Form.Select  width='4'
            fluid
            name='priorityLevel'
            label='Urgencia'
            options={priorityLevelOption}
            placeholder=''
            onChange={this.handleChange}
          />
        </Form.Group>
        <Form.Group widths='equal'>
          <Form.Input onChange={this.handleChange} fluid label='Asunto' name='subject' value={subject} />

        </Form.Group>

        <Form.TextArea onChange={this.handleChange} label='Mensaje' name='message' value={message} placeholder='Hola, te enviamos la factura emitida.' />

        <Form.Group inline>
          <label>Incluir adjunto</label>
          <Form.Radio
            label='Si.'
            value='0'
            checked={battachInvoice === '0'}
            onChange={this.handleChangeAttach}
            name='battachInvoice'
          />
          <Form.Radio
            label='No.'
            value='1'
            checked={battachInvoice === '1'}
            onChange={this.handleChangeAttach}
            name='battachInvoice'
          />
         
        </Form.Group>        
      </Form>
        </Modal.Content>
        <Modal.Actions>
          <Button color='gray' onClick={this.handleClose}>
            <Icon name='remove' /> Cerrar
          </Button>
          <Button color='green' onClick={this.handleSend}>
            <Icon name='checkmark' /> Enviar, cerrar
          </Button>
        </Modal.Actions>
      </Modal>

    )
  }
}

function mapStateToProps(state){
    return{
      // isFetching: state.Invoices.isFetching,
      sendEmailShow: state.invoices.sendEmailShow,
      invoiceId: state.invoices.invoiceId
    }
  }
  
  
  export default connect(mapStateToProps)(SendForm);


