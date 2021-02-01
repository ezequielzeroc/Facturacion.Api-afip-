import PropTypes from 'prop-types';
import React, { Component, PureComponent } from 'react';
import { connect } from 'react-redux';
import { Button, Header, Icon, Label, Image, Table, Grid, Card } from 'semantic-ui-react'
import { fetchMovements } from '../../../actions/FinancialMovements';
import { CancelInvoice, deleteInvoice, downloadInvoice, fetchInvoices, setInvoiceID, showSendEmail } from '../../../actions/invoices';
import { Status } from '../../../components/invoiceStatus/status'
import SendForm from '../../SendForm';
import Balance from '../Balance';
import MovementIN from '../MovementIN';
import MovementOut from '../MovementOut';
import Pending from '../Pending';

class List extends Component{

    constructor(props){
        super(props)
        this.state = {
          showModal:false,
          invoiceId:0,
        }
    }   

    async download(id){
        this.props.dispatch(
          downloadInvoice(id),
        )
      }

  componentDidMount(){

    this.props.dispatch(
        fetchInvoices()
    ).then(console.log("Componente montado",this.props))
  }

  handleSelect=(invoiceId)=>{
      this.props.dispatch(
        setInvoiceID(invoiceId)
      );
      this.handleOpen()

      

  }
   handleOpen(){
    this.props.dispatch(
      showSendEmail()
    )
  }


    render(){
        let { description,showModal,invoiceId } = this.state;
        return(
            
          <>
          <SendForm showModal={showModal} invoiceID={invoiceId}></SendForm>
            <Header></Header>
            <Table size='small'>
              <Table.Header>
              <Table.Row>
                <Table.HeaderCell width='1'>Fecha</Table.HeaderCell>
                <Table.HeaderCell width='1'>Tipo</Table.HeaderCell>
                <Table.HeaderCell width='2'>Nro.</Table.HeaderCell>
                <Table.HeaderCell width='4'>Cliente</Table.HeaderCell>
                <Table.HeaderCell width='1'>Estado</Table.HeaderCell>
                <Table.HeaderCell width='1'>Pago</Table.HeaderCell>
                <Table.HeaderCell width='2'>C.A.E.</Table.HeaderCell>
                <Table.HeaderCell width='2'textAlign='right'>Total</Table.HeaderCell>
                <Table.HeaderCell width='2'></Table.HeaderCell>
              </Table.Row>
            </Table.Header>
          <Table.Body>
            {
              this.props.Invoices &&
              this.props.Invoices?.map((inv,indx)=>(
                <Table.Row key={inv.invoiceID}>
                  <Table.Cell>
                    {
                      new Intl.DateTimeFormat("es-AR", {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit"
                        }).format(new Date(inv.created))
                    }
                  </Table.Cell>
                <Table.Cell>{inv.documentTypeShortCode}</Table.Cell>
                <Table.Cell>{inv.invoiceNumber}</Table.Cell>
                <Table.Cell><Icon name='user' />{inv.clientName}</Table.Cell>
                <Table.Cell><Label color='grey'>{Status.Invoice(inv.status).Name}</Label></Table.Cell>
                <Table.Cell textAlign='center'>
                  {
                    inv.paid?
                      <Icon title='Pago realizado' color='green' size='large' name='check circle'></Icon>
                      :
                     <Icon title='Pago no realizado' color='red' size='large' name='times circle'></Icon>
                  }
                  </Table.Cell>
                <Table.Cell> {inv.cae} </Table.Cell>
                <Table.Cell textAlign='right'>{new Intl.NumberFormat("es-AR",{style: "currency", currency: "ARS"}).format(inv.total)} </Table.Cell>
                <Table.Cell textAlign='right'>
                <Button size='tiny'  circular title="Enviar" onClick={()=>(this.handleSelect(inv.invoiceID))}   color='teal' icon='send'></Button>
                  {
                    inv.status === 4 ?
                     (<Button size='tiny' circular title="Anular documento" onClick={()=>(this.cancelInvoice(parseInt(inv.invoiceID)))}  color='orange' icon='cancel'></Button>)
                    :
                    inv.status===2 ?
                    (<Button size='tiny' circular title="Eliminar docuento" onClick={()=>(this.handleDelete(inv.invoiceID))} color='red' icon='trash alternate'></Button>)
                    :
                    (<Button size='tiny' disabled circular title="Eliminar docuento" onClick={()=>(this.handleDelete(inv.invoiceID))} color='red' icon='trash alternate'></Button>)
                  }
                  <Button size='tiny'  circular title="Descargar" onClick={()=>(this.download(inv.invoiceID))}   color='teal' icon='download'></Button>
                </Table.Cell>
              </Table.Row>
              ))
            }
          </Table.Body>
        </Table>
          </>
          
        )
    }
    

}
function mapStateToProps(state){
  return{
    // isFetching: state.Invoices.isFetching,
    Invoices: state.invoices.Invoices,
    sendEmailShow: state.invoices.sendEmailShow
  }
}


export default connect(mapStateToProps)(List);