import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { toast } from 'react-semantic-toasts';
import {Message, Statistic, Card,Container, Radio, Form, Step, Segment, Menu, Grid, Header, Icon, Image, Select, Input, Divider, Table, Tab, Button, Confirm, TextArea, Checkbox } from 'semantic-ui-react';
class Stats extends Component{

    constructor(props){
        super(props)
        this.state = {
        }
    }

      

    render(){
        let { items } = this.state;
        return(
        <Segment placeholder>
            <Statistic.Group widths='four'>
                <Statistic>
                <Statistic.Value>-</Statistic.Value>
                <Statistic.Label>Facturas emitidas</Statistic.Label>
                </Statistic>
            
                <Statistic>
                <Statistic.Value>-</Statistic.Value>
                <Statistic.Label>IVA venta</Statistic.Label>
                </Statistic>
            
                <Statistic>
                <Statistic.Value>
                    <Icon name='plane' />5
                </Statistic.Value>
                <Statistic.Label>Flights</Statistic.Label>
                </Statistic>
            
                <Statistic>
                <Statistic.Value>
                    <Image src='/images/avatar/small/joe.jpg' className='circular inline' />
                    42
                </Statistic.Value>
                <Statistic.Label>Team Members</Statistic.Label>
                </Statistic>
            </Statistic.Group>
        </Segment>
           
        );
    }
}

export default Stats;