import React,{ Component, Fragment } from 'react';
import {Dimmer,Loader,Form,Header, Button, Icon, Image, Modal, Grid } from 'semantic-ui-react'
import { withRouter } from 'react-router';
import { connect } from 'react-redux';
import {  toast } from 'react-semantic-toasts';
import { updatePos, createPos, fetchPosById, handleChange, resetForm } from '../../../actions/pos';




class Edit extends Component{
    static defaultProps = {
        isNew:true,
        isFetching:false,
        name: '',
        code: '',
        description: '',
        posId: 0,
        address: '',
    }
    constructor(props){
        super(props);
        this.state = {
            isNew:true,
            title:'Nuevo punto de venta' 
        }
    }
        
    handleChange = (e) => {
        this.props.dispatch(
            handleChange(e.target)
        )
        console.log(this.props)
    }
    goBack = () =>{
        this.props.history.push("/pos/")
    }


    doCreatePos = (e) => {
        let {isNew} = this.state;

        console.log(this.props.posId);
        if(!isNew)
        {
            //es edicion
            this.props
                .dispatch(
                    updatePos({
                      posId:parseInt(this.props.posId),
                      name:this.props.name,
                      code:parseInt(this.props.code),
                      address:this.props.address,
                      description:this.props.description,
                    })
                ).then(x=>{
                    toast({
                        type: 'success',
                        icon: 'check',
                        title: 'Puntos de venta',
                        description: 'Punto de venta actualizado correctamente.',
                        time: 10000
                        }); 
    
                });
        }else{
            this.props
            .dispatch(
              createPos({
                name:this.props.name,
                code:parseInt(this.props.code),
                address:this.props.address,
                description:this.props.description,
              }),
            )
            .then(() =>{
                toast({
                    type: 'success',
                    icon: 'check',
                    title: 'Puntos de venta',
                    description: 'Punto de venta creado correctamente.',
                    time: 10000
                    }); 
              },
            );
        }
        
        this.props.dispatch(
            resetForm()
        );
    }

    async componentDidMount(){
        let{match: { params } } = this.props;
        let { isNew } = this.state;

        if(params?.id){
            console.log(params?.id);
            this.setState({'isNew':false, title:'Editar punto de venta'});
            this.props
                .dispatch(
                    fetchPosById(params.id)
                )
        }else{
            this.setState({'isNew':true, title:'Nuevo punto de venta'});
        }

        
    }
    render(){
        console.log(this.props);
        let {title} = this.state;
        return(

            <Fragment>

                <Header as='h2'>
                    <Icon name='pencil' />
                    <Header.Content>
                        {title}
                    </Header.Content>
                </Header>            
                  <Form>
                  <Dimmer active={this.props.isFetching?true:false} inverted>
                    <Loader content='Obteniendo datos' />
                </Dimmer> 
                <Form.Group>
                <Form.Input width='4' fluid label='Código' maxLength='4' name='code' value={this.props.code} onChange={this.handleChange} placeholder='00000' />
                <Form.Input width='4' fluid label='Nombre' name='name' value={this.props.name} onChange={this.handleChange} placeholder='Nombre' />
                <Form.Input width='8' fluid label='Dirección' name='address' value={this.props.address} onChange={this.handleChange} />
                </Form.Group>
       
            <Form.TextArea name='description' value={this.props.description} onChange={this.handleChange} label='Descripción'/>
            <Grid verticalAlign='middle'>
                    <Grid.Row>
                        <Grid.Column width='16'>
                        <Button onClick={this.doCreatePos} floated='right'>
                            {
                            this.props.isFetching?'Guardando...':'Guardar'
                            }
                        </Button>   
                        <Button onClick={this.goBack}  floated='right' basic ><Icon name='backward'></Icon>Volver</Button>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>
          </Form>
            </Fragment>
        )
    }
}


function mapStateToProps(state){
    return{
        isFetching: state.pos.isFetching,
        code: state.pos.posLoaded.code,
        address: state.pos.posLoaded.address,
        name: state.pos.posLoaded.name,
        description	: state.pos.posLoaded.description,
        posId :state.pos.posLoaded.posId,

    }
}
export default withRouter(connect(mapStateToProps)(Edit));