import React,{Component} from 'react';
import { Switch, Route, withRouter } from 'react-router';
import { Dimmer,Loader, Container } from 'semantic-ui-react'
// import dashboard from '../../pages/dashboard';
// import categories from '../../pages/categories';
// import certificates from '../../pages/certificates';



const FixedMenu = React.lazy(()=> import('./menu'));
// const Dashboard = React.lazy(()=> import('../../pages/dashboard'));
const Invoices = React.lazy(()=> import('../../pages/invoices/'));
// const Inventory = React.lazy(()=> import('../../pages/inventory'));
// const Categories = React.lazy(()=> import('../../pages/categories'));
// const Clients = React.lazy(()=> import('../../pages/clients'));
const Pos = React.lazy(()=> import('../../pages/pos/'));
const Dashboard = React.lazy(()=> import('../../pages/dashboard/'));
// const Certificates = React.lazy(()=> import('../../pages/certificates'));
// const General = React.lazy(()=> import('../../pages/account/settings/general'));
// const Collection = React.lazy(()=> import('../../pages/collection'));

const ErrorPage = React.lazy(()=> import('../../pages/error/ErrorPage'));
const loading = () =>  (<Dimmer active><Loader content='Cargando software' /></Dimmer>);


const New = React.lazy(()=> import('../../pages/invoices/edit'));

class Layout extends Component{

    render(){
        return(
            <div className='app'>
                <FixedMenu></FixedMenu>
                <Container style={{ marginTop: '7em' }}>
                <React.Suspense fallback={loading()}>
                    <Switch>
                        {/* <Route path='/invoices/new' component={New}></Route> */}
                        <Route path='/pos/' component={Pos}></Route>
                        <Route path='/invoices/' component={Invoices}></Route>
                        <Route path='/' exact component={Dashboard}></Route>
                        {/* 
                        <Route path='/inventory/' component={Inventory}></Route>
                        <Route path='/categories/' component={Categories}></Route>
                        <Route path='/clients/' component={Clients}></Route>
                        <Route path='/certificates/' component={Certificates}></Route>
                        <Route path='/collection/' component={Collection}></Route>
                        <Route path='/settings/general' component={General}></Route> */}
                        <Route component={ErrorPage}></Route>
                    </Switch>
                </React.Suspense>
                </Container>
            </div>
        );
    }
}

export default withRouter(Layout);