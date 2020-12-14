
import React,{Component} from 'react';
import Stats from './stats'
import { Switch, Route, withRouter } from 'react-router';

class Dashboard extends Component{

    render(){
        return(

                    <Switch>
                        <Route path='/' exact component={Stats} />
                        {/* <Route key='new' path='/invoices/new/' exact component={EditForm} />
                        <Route key='edit' path='/invoices/edit/:id?' exact component={EditForm} /> */}

                    </Switch>    
            
        );
    }
}
export default withRouter(Dashboard);