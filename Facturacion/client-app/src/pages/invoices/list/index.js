import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Header, Icon, Label, Segment, Table } from 'semantic-ui-react'
import { CancelInvoice, deleteInvoice, downloadInvoice, fetchInvoices } from '../../../actions/invoices';
import { Status } from '../../../components/invoiceStatus/status'

class List extends Component{

  async download(id){
    this.props.dispatch(
      downloadInvoice(id),
    )
  }

  handleDelete =(id)=>{
    this.props.dispatch(
        deleteInvoice(id)
    ).then(()=>{
      this.props.dispatch(
        fetchInvoices()
      )
    })
  }

   cancelInvoice = ( id ) =>{
    this.props.dispatch(
      CancelInvoice({invoiceID:id})
    )
  }
  async componentDidMount(){
    this.props.dispatch(
        fetchInvoices()
    )
  }


    render(){
        return(
          <>
          <Header as='h2'>
                <Icon name='file alternate outline' />
                    <Header.Content>
                        Comprobantes emitidos
                    <Header.Subheader>Listado de comprobantes emitidos</Header.Subheader>
                </Header.Content>
            </Header>
            <Table>
              <Table.Header>
              <Table.Row>
                <Table.HeaderCell width='1'>Fecha</Table.HeaderCell>
                <Table.HeaderCell width='1'>Tipo</Table.HeaderCell>
                <Table.HeaderCell width='2'>Nro.</Table.HeaderCell>
                <Table.HeaderCell width='4'>Cliente</Table.HeaderCell>
                <Table.HeaderCell width='2'>Estado</Table.HeaderCell>
                <Table.HeaderCell width='2'>C.A.E.</Table.HeaderCell>
                <Table.HeaderCell width='2'textAlign='right'>Total</Table.HeaderCell>
                <Table.HeaderCell width='2'></Table.HeaderCell>
              </Table.Row>
            </Table.Header>
          <Table.Body>
            {
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
                <Table.Cell> {inv.cae} </Table.Cell>
                <Table.Cell textAlign='right'>{new Intl.NumberFormat("es-AR",{style: "currency", currency: "ARS"}).format(inv.total)} </Table.Cell>
                <Table.Cell textAlign='right'>
                  {
                    inv.status === 4 ?
                     (<Button size='tiny' circular title="Anular documento" onClick={()=>(this.cancelInvoice(parseInt(inv.invoiceID)))}  color='orange' icon='cancel'></Button>)
                    :
                    inv.status===1 || inv.status===2  ?
                    (<Button size='tiny' circular title="Eliminar docuento" onClick={()=>(this.handleDelete(inv.invoiceID))} color='red' icon='trash alternate'></Button>)
                    :
                    (<Button size='tiny' disabled circular title="Eliminar docuento" onClick={()=>(this.handleDelete(inv.invoiceID))} color='red' icon='trash alternate'></Button>)
                  }
                  <Button size='tiny'  circular title="Descargar" onClick={()=>(this.download(inv.invoiceID))} borderless  color='teal' icon='download'></Button>
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
  }
}



export default connect(mapStateToProps)(List);