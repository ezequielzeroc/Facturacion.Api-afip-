
import React,{Component} from 'react';
import List from './list'
import EditForm from './edit'
import { Switch, Route, withRouter } from 'react-router';

class Invoices extends Component{

    render(){
        return(

                    <Switch>
                        <Route path='/invoices' exact component={List} />
                        <Route key='new' path='/invoices/new/' exact component={EditForm} />
                        <Route key='edit' path='/invoices/edit/:id?' exact component={EditForm} />

                    </Switch>    
            
        );
    }
}
export default withRouter(Invoices);