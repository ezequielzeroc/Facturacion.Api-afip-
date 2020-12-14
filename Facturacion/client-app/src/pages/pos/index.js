
import React,{Component} from 'react';
import List from './list'
import Edit from './edit'
import { Switch, Route, withRouter } from 'react-router';

class Pos extends Component{

    render(){
        return(

                    <Switch>
                        <Route path='/pos' exact component={List} />
                        <Route key='new' path='/pos/new/' exact component={Edit} />
                        <Route key='edit' path='/pos/edit/:id?' exact component={Edit} />

                    </Switch>    
            
        );
    }
}
export default withRouter(Pos);