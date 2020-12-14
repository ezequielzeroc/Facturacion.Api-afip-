import React,{Component} from 'react'
import {Menu,Dimmer,Loader,Dropdown, Button, Form, Grid, Header, Image,Container, Icon, Divider, Label } from 'semantic-ui-react'
// import logo from '../../../logo.png';
import { Link, Redirect, withRouter } from 'react-router-dom';
import { logoutUser } from '../../../actions/account';
import { connect } from 'react-redux';
class FixedMenu extends React.PureComponent{
    onChange = (e)=>{
        this.props.history.push(e);
      }
      logOut=()=>{
          this.props
            .dispatch(
                logoutUser()
            )
            this.props.history.push('/account/login');
      }
      getUserName=()=>{
          return `Hola ${localStorage.getItem("name")}!`
      }
      
      btnInvoice = (<span><Icon name='file alternate outline'></Icon>Facturación</span>)
      
    render(){
        return(
        <Menu fixed='top' inverted compact>
                <Container>
                    <Menu.Item onClick={()=>(this.onChange('/'))} as='a' header>
                    {/* <Image size='mini' src={logo} style={{ marginRight: '1.5em' }} /> */}
                    EasyStock
                    </Menu.Item>
                    <Dropdown trigger={this.btnInvoice}  item >
                    <Dropdown.Menu>
                        <Dropdown.Item onClick={()=>(this.onChange('/invoices/new/'))} ><Icon name='add'></Icon>Nueva factura</Dropdown.Item>
                        <Dropdown.Item onClick={()=>(this.onChange('/invoices/'))}> <Icon name='folder outline'></Icon>Listado de facturas</Dropdown.Item>
                        <Dropdown.Divider />
                        <Dropdown.Item onClick={()=>(this.onChange('/pos/'))}> <Icon name='home'></Icon>Puntos de venta</Dropdown.Item>
                    </Dropdown.Menu>
                    </Dropdown>
{/* 
                    <Dropdown item text={()=>(<><Icon name='boxes'></Icon>Inventario</>)}>
                    <Dropdown.Menu>
                        <Dropdown.Item onClick={()=>(this.onChange('/inventory/new/'))}><Icon name='add' /> Nuevo Producto/Servicio</Dropdown.Item>
                        <Dropdown.Item onClick={()=>(this.onChange('/inventory/'))}><Icon name='list alternate outline' /> Listado completo</Dropdown.Item>
                        <Divider></Divider>
                        <Dropdown.Item disabled onClick={()=>(this.onChange('/inventory/update-price'))}>Actualización de precios</Dropdown.Item>
                        <Dropdown.Item disabled onClick={()=>(this.onChange('/inventory/stock'))}>Ajuste de stock</Dropdown.Item>
                        <Dropdown.Item disabled onClick={()=>(this.onChange('/inventory/import'))}>Importación masiva</Dropdown.Item>
                        <Divider></Divider>
                        <Dropdown.Item  onClick={()=>(this.onChange('/categories/'))}><Icon name='tags'></Icon> Categorías</Dropdown.Item>
                        
                    </Dropdown.Menu>
                    </Dropdown> */}
{/*                     
                    <Dropdown item text={()=>(<span><Icon name='users'></Icon>Clientes</span>)} >
                    <Dropdown.Menu>
                        <Dropdown.Item onClick={()=>(this.onChange('/clients/new'))}><Icon name='user plus'></Icon> Nuevo cliente</Dropdown.Item>
                        <Dropdown.Item onClick={()=>(this.onChange('/clients/'))}><Icon name='list alternate outline'></Icon> Listado de clientes</Dropdown.Item>
                        <Dropdown.Divider />
                        <Dropdown.Item disabled>Importación masiva</Dropdown.Item>
                    </Dropdown.Menu>
                    </Dropdown>    
                    <Dropdown item text={()=>(<span><Icon name='briefcase'></Icon>Gestión</span>)} >
                    <Dropdown.Menu>
                        <Dropdown.Header>Cobranzas</Dropdown.Header>
                        <Dropdown.Item  onClick={()=>(this.onChange('/collection/'))}><Icon name='file text'></Icon> Comprobantes a cobrar</Dropdown.Item>
                    </Dropdown.Menu>
                    </Dropdown>      
                    <Dropdown item text={()=>(<span><Icon name='file archive outline'></Icon>Reportería</span>)} >
                    <Dropdown.Menu>
                        <Dropdown.Header>Cobranzas</Dropdown.Header>
                        <Dropdown.Item disabled onClick={()=>(this.onChange('/clients/new'))}><Icon name='file text'></Icon> Comprobantes emitidos</Dropdown.Item>
                    </Dropdown.Menu>
                    </Dropdown>      */}
                </Container>
                <Menu.Item position='right'>
                    {this.getUserName()}
                </Menu.Item>
                <Menu.Item>
                    <Dropdown item icon='cog' direction='left'>
                        <Dropdown.Menu>
                        <Dropdown.Item><Icon name='user outline'></Icon> Mi perfil</Dropdown.Item>
                        <Dropdown.Divider />
                        <Dropdown.Item onClick={()=>(this.onChange('/pos/'))}><Icon name='home'></Icon> Puntos de venta</Dropdown.Item>
                        <Dropdown.Divider />
                        <Dropdown.Item onClick={()=>(this.onChange('/certificates/'))}><Icon name='certificate'></Icon> Certificados</Dropdown.Item>
                        <Dropdown.Item disabled onClick={()=>(this.onChange('/settings/general'))}><Icon name='settings'></Icon> Configuración</Dropdown.Item>
                        <Dropdown.Divider>
                        </Dropdown.Divider>
                        <Dropdown.Item onClick={this.logOut.bind(this)}><Icon name='log out'></Icon> Cerrar sesión</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>        

                </Menu.Item>
                </Menu>
        )
    }
}
function mapStateToProps(state){
    return{

    }
}
export default withRouter(connect(mapStateToProps)(FixedMenu));