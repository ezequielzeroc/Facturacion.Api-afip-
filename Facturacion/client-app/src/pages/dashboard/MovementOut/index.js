import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Header, Icon, Label, Loader, Dimmer, Grid, Card } from 'semantic-ui-react'
import { fetchMovements } from '../../../actions/FinancialMovements';
import { Status } from '../../../components/invoiceStatus/status'
class MovementOut extends Component{

    constructor(props){
        super(props)
        this.state = {
            amount:0.00,
            fetching:true
        }
    }   



  async componentDidMount(){

      this.props.dispatch(
          fetchMovements(2)
      ).then(res=>{
          this.setState({amount:res,fetching:false})
      })
  }


    render(){
        let {amount,fetching} = this.state;
        return(
            
            <Grid.Column>
            <Card fluid color='red'>
                <Dimmer active={fetching} inverted >
                    <Loader content='Cargando...' />
                </Dimmer>

                <Card.Content>
                    <Card.Header content='Egresos' />
                    <Card.Description  textAlign='right' content={"$ "+ this.props.amount}  />
                </Card.Content>
            </Card>
            </Grid.Column>
            
        )
    }
    

}
function mapStateToProps(state){
  return{
    amount: state.financialMovements.Out,
  }
}



export default connect(mapStateToProps)(MovementOut);