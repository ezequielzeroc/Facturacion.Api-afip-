import PropTypes from 'prop-types';
import React, { Component, PureComponent } from 'react';
import { connect } from 'react-redux';
import { Button, Header, Icon, Label, Image, Table, Grid, Card } from 'semantic-ui-react'
import { fetchMovements } from '../../../actions/FinancialMovements';
import { CancelInvoice, deleteInvoice, downloadInvoice, fetchInvoices } from '../../../actions/invoices';
import { Status } from '../../../components/invoiceStatus/status'
import Balance from '../Balance';
import MovementIN from '../MovementIN';
import MovementOut from '../MovementOut';
import Pending from '../Pending';
import List from './List.js';
class lastInvoices extends PureComponent{

    constructor(props){
        super(props)
        this.state = {
            description: "test"
        }
    }   
  async download(id){
    this.props.dispatch(
      downloadInvoice(id),
    )
  }



   cancelInvoice = ( id ) =>{
    this.props.dispatch(
      CancelInvoice({invoiceID:id})
    )
  }
  componentDidMount(){


  }


    render(){
        let { description } = this.state;
        console.log("Componente renderizado",this.props)
        return(
            
          <>
          {/* <Header as='h2'>
                <Icon name='file alternate outline' />
                    <Header.Content>
                        Comprobantes emitidos
                    <Header.Subheader>Listado de comprobantes emitidos</Header.Subheader>
                </Header.Content>
            </Header> */}
            {/* <Header> */}
            <Grid columns='four' divided>
                <Grid.Row>
                <MovementIN></MovementIN>
                <Pending></Pending>            
                <MovementOut></MovementOut>
                <Balance></Balance>
                </Grid.Row>
            </Grid>
            <List></List>
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


export default connect(mapStateToProps)(lastInvoices);