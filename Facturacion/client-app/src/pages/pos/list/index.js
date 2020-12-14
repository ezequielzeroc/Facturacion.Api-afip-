import React,{ Component, Fragment }  from 'react'
import { connect } from 'react-redux';
import {Header,Icon,Table,Dropdown,Checkbox, Container, Button, Grid } from 'semantic-ui-react'
import Edit from '../edit'
import { deletePos, fetchPos } from '../../../actions/pos';
class List extends Component{
    // constructor(props){
    //     super(props);
    // }

    goToCreate = ()=>{
        this.props.history.push('/pos/new')
    }
    edit = (id)=>{
        this.props.history.push(`/pos/edit/${id}`);
    }
    deletePos(id){
        let confirm = window.confirm("¿Realmente deseas eliminar el punto de venta seleccionado?");
        if(confirm)
        {
          this.props.dispatch(
            deletePos(id),
          ).then(x=>{
            this.props.dispatch(fetchPos()); 
          });
        }
        
      }
    async componentDidMount(){
        this.props
            .dispatch(
                fetchPos()
            )
    }
    render(){

        return(
            <Fragment>
                <Header as='h2'>
                <Icon name='tags' />
                <Header.Content>
                    Puntos de venta
                <Header.Subheader>Agregue, elimine o modifique.</Header.Subheader>
                </Header.Content>
                </Header>
                <Grid verticalAlign='middle'>
                    <Grid.Row>
                        <Grid.Column width='16'>
                            <Button onClick={this.goToCreate} floated='right' color='teal'>Nuevo</Button>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>

                <Table selectable>
                    <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell  ><Checkbox></Checkbox></Table.HeaderCell>
                        <Table.HeaderCell width='2'>Código</Table.HeaderCell>
                        <Table.HeaderCell width='4'>Nombre</Table.HeaderCell>
                        <Table.HeaderCell width='4'>Dirección</Table.HeaderCell>
                        <Table.HeaderCell width='6'>Descripcion</Table.HeaderCell>
                        <Table.HeaderCell textAlign='right' ></Table.HeaderCell>
                        
                    </Table.Row>
                    </Table.Header>

                    <Table.Body>
                        {
                            this.props.Pos &&
                            this.props.Pos.map((i,e)=>(
                                <Table.Row key={i.posid}>
                                    <Table.Cell><Checkbox name='itemsSelected' id={i.posId} key={i.posId} value={i.posId}></Checkbox></Table.Cell>
                                    <Table.Cell>{i.code}</Table.Cell>
                                    <Table.Cell>{i.name}</Table.Cell>
                                    <Table.Cell> {i.address} </Table.Cell>
                                    <Table.Cell> {i.description} </Table.Cell>
                                    <Table.Cell textAlign='right'>
                                    <Dropdown >
                                        <Dropdown.Menu>
                                        <Dropdown.Item onClick={()=>(this.deletePos(i.posId))} icon='trash' text='Eliminar' />
                                        <Dropdown.Item onClick={()=>(this.edit(i.posId))}  icon='pencil' text='Editar' />
                                        </Dropdown.Menu>
                                    </Dropdown>
                                    </Table.Cell>
                                </Table.Row>
                            ))
                        }
                    </Table.Body>
                </Table>      
            </Fragment>
            

        );
    }
}
function mapStateToProps(state){
    return{
        isFetching: state.pos.isFetching,
        Pos: state.pos.Pos,
        // categoryID: state.categories.categoryID,
    }
}
export default connect(mapStateToProps)(List);