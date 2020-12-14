import React, { Component, } from 'react';
import { connect } from 'react-redux';
import { Form,Select } from 'semantic-ui-react';
import { fetchPos } from '../../actions/pos';


class SelectPos extends Component {
    static defaultProps = {
        isNew:true,
        isFetching:false,
        name: '',
        code: '',
        description: '',
        posid: 0,
        address: '',
    }
    constructor(props){
        super(props)
        this.state = {
            posOptions:[]
        }
    }

     componentDidMount(){
        this.props
        .dispatch(
            fetchPos()
        );
    }

    render(){
        let {posOptions} = this.state;
        return(
            <Form.Field width={this.props?.size}>
            <label>{this.props.label}</label>
            <Select name='PosCode' onChange={this.props.handleChange} options={this.props.list?.map((i,ix)=>({key:i.code,value:i.code,text:i.name}))}></Select>
            </Form.Field>
        )
    }
}
const mapStateToProps=(state)=>{
    return {
        list: state.pos.Pos
    }
        
    
}

export default connect(mapStateToProps)(SelectPos);