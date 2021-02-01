import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Header, Icon, Label, Image, Loader,Dimmer, Grid, Card } from 'semantic-ui-react'
import { fetchMovements } from '../../../actions/FinancialMovements';
import { CancelInvoice, deleteInvoice, downloadInvoice, fetchInvoices } from '../../../actions/invoices';
import { Status } from '../../../components/invoiceStatus/status'
class MovementIn extends Component{

    toDecimal = ( value ) => Number.parseFloat(value).toFixed(2);
    constructor(props){
        super(props)
        this.state = {
            amount:0.00,
            fetching:true
        }
    }   



  async componentDidMount(){
    //   this.props.dispatch(
    //       fetchMovements(1)
    //   ).then(res=>{
    //       this.setState({amount:res,fetching:false})
    //   })
  }


    render(){
        let {amount,fetching} = this.state;
        return(
            
                <Grid.Column>
                    <Card fluid color='blue'>
                        <Card.Content>
                            <Card.Header content='Balance' />
                            <Card.Description textAlign='right' content={+ this.toDecimal(((this.props.In)-(this.props.Out)).toString())}  />
                        </Card.Content>
                    </Card>
                </Grid.Column>
            
        )
    }
    

}
function mapStateToProps(state){
  return{
     In: state.financialMovements.In,
     Out: state.financialMovements.Out,
  }
}



export default connect(mapStateToProps)(MovementIn);