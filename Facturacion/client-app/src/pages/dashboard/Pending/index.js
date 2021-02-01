import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Header, Icon, Label, Dimmer,Loader, Grid, Card } from 'semantic-ui-react'
import { fetchMovements } from '../../../actions/FinancialMovements';
import { CancelInvoice, deleteInvoice, downloadInvoice } from '../../../actions/invoices';
import { Status } from '../../../components/invoiceStatus/status'
class Pending extends Component{

    constructor(props){
        super(props)
        this.state = {
            amount:0.00,
            fetching:true
        }
    }   



  async componentDidMount(){
      this.props.dispatch(
          fetchMovements(3)
      ).then(res=>{
          this.setState({amount:res,fetching:false})
      })
  }


    render(){
        let {amount,fetching} = this.state;
        return(
            
            <Grid.Column>             
            <Card fluid color='orange'>
                <Dimmer active={fetching} inverted >
                    <Loader content='Cargando...' />
                </Dimmer>   
                <Card.Content>
                    <Card.Header content='Pendiente de cobro' />
                    <Card.Description  textAlign='right' content={"$ "+ this.props.amount}  />
                </Card.Content>
            </Card>
            </Grid.Column>
            
        )
    }
    

}
function mapStateToProps(state){
  return{
    amount: state.financialMovements.Pending,
  }
}



export default connect(mapStateToProps)(Pending);