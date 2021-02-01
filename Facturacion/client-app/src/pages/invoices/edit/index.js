import React, { Component } from 'react';
import { connect } from 'react-redux';
import { toast } from 'react-semantic-toasts';
import { Container, Dimmer, Radio, Form, Modal, Segment, Message, Grid, Header, Icon, Select, Input, Divider, Table, Tab, Button, Confirm, TextArea, Checkbox, Loader } from 'semantic-ui-react';
import { fetchDocumentType } from '../../../actions/documentType';
import { markAsPaid } from '../../../actions/FinancialMovements';
import { fetchIdentityDocumentType } from '../../../actions/identityDocumentType';
import { CreateInvoice, deleteInvoice } from '../../../actions/invoices';
import { fetchVatCondition } from '../../../actions/vatCondition';
import SelectPos from '../../../components/pos/SelectPos';
import { Tax } from '../../../components/Tax/Tax'



class EditForm extends Component{
   
    constructor(props){
        super(props);
        this.state = {
            showMessage:false,
            showSaveBtn:false,
            invoiceTypeOptions:[
                {key:26,text:'Factura A',value:'FCA'},
                {key:19,text:'Nota de débito A',value:'NDA'},
                {key:19,text:'Nota de crédito A',value:'NCA'},
                {key:19,text:'Factura B',value:'FCB'},
                {key:21,text:'Nota de débito B',value:'NDB'},
                {key:23,text:'Nota de crédito B',value:'NCB'},
                {key:25,text:'Recibo B',value:'RCB'},

            ],
            taxes:[
                {key:1,value:1,text:'No gravado'},
                {key:2,value:2,text:'Exento'},
                {key:3,value:3,text:'IVA 0%'},
                {key:4,value:4,text:'IVA 10,50%'},
                {key:5,value:5,text:'IVA 21,00%'},
                {key:6,value:6,text:'IVA 27,00%'},
                {key:8,value:8,text:'IVA 5,00%'},
                {key:9,value:9,text:'IVA 2.50%'},
            ],
            conceptsOptions:[
                {key:1,text:'Productos',value:'1'},
                {key:2,text:'Servicios',value:'2'},
                {key:3,text:'Mixto',value:'3'},
              ],
            DocumentTypeOptions:[],
            PaymentMethod:0,
            Lines:[],
            codeBar:'',
            Description:'',
            Qtty:1,
            UnityOfMeasure:0,
            taxId:0,
            UOM:[{key:1,value:1,text:'Unidades'},{key:2,value:2,text:'Metros'}],
            unitOfMeasurementID:1,
            discount:0,
            subtotal:0,
            subTotalAmount:this.ToDecimal(0),
            price:0,
            total:0,
            taxCalculated:0,
            TotalTaxes:[],
            TotalDiscount:0,
            calculatedDiscount:0,
            PosCode:0,
            DocumentTypeID:0,
            InvoiceDate:'',
            ConceptCode:0,
            VatConditionCode:0,
            ClientName:'',
            IdentityDocumentTypeCode:0,
            IdentityDocumentNumber:'',
            ClientAddress:'',
            ClientEmail:'',
            Payed:false,

        }
        
    }

    ToDecimal = (value)=> Number.parseFloat(value).toFixed(2);
    closeMessage = () =>this.setState({showMessage:false});
    handlePaymentMethod = (e,{name,value}) => {
        this.setState({[name]:value})
    } 


      groupBy = (xs, key) => {
        return xs.reduce(function(rv, x) {
          (rv[x[key]] = rv[x[key]] || []).push(x);
          return rv;
        }, {});
      };
    addLine=()=>{
        
        
        let {Lines, Description,Qtty,taxId,unitOfMeasurementID,discount,subtotal,price,total,TotalTaxes,taxCalculated,subTotalAmount,calculatedDiscount,TotalDiscount} = this.state;
        

        if(Qtty===0)
        {
            toast({
                title:'Error',
                description:'Ingrese cantidad > 0',
                icon:'warning',
                type:'warning'
                
            })
            return;
        }

        
        
        let tax = Tax.taxById(parseInt(taxId));
        let taxValue = tax.Value;


        Lines.push({id:parseInt(Lines[Lines.length-1]?Lines[Lines.length-1].id+1:1),name:'remove',Description:Description,Qtty:Qtty, taxId:taxId, unitOfMeasurementID:unitOfMeasurementID,TotalDiscount:parseFloat(calculatedDiscount),discount:parseFloat(discount),subtotal:parseFloat(subtotal),price:parseFloat(price), taxCalculated:parseFloat(taxCalculated)    });
        let _totalTaxes = this.state.TotalTaxes;
        let _subTotalAmount =  parseFloat(this.ToDecimal(parseFloat(subTotalAmount) + parseInt(Qtty)*parseFloat(price)));
        let _newTotalDiscount = parseFloat(this.ToDecimal(calculatedDiscount)) + parseFloat(TotalDiscount); 
        const index = _totalTaxes.findIndex(item => item.id === taxId);
        console.log(taxId);
        if(index>-1){
            _totalTaxes[index] = {..._totalTaxes[index], total:this.ToDecimal(parseFloat(_totalTaxes[index].total)+ taxCalculated)}
        }else{
            _totalTaxes.push({id:parseInt(taxId),name:Tax.taxById(parseInt(taxId)).Name, total:this.ToDecimal(taxCalculated)}); 
        }
        this.setState({TotalTaxes:_totalTaxes,subTotalAmount:_subTotalAmount, TotalDiscount:_newTotalDiscount})



        this.setState({
            lines:Lines,
            Description:'',
            Qtty:0,
            price:this.ToDecimal(0),
            discount:0,
            taxId:0,
            subtotal:this.ToDecimal(0),
            total:parseFloat(total)+parseFloat(subtotal)
        });
    };

    removeLine = (id)=>{
        let {Lines ,total,TotalTaxes,subTotalAmount,TotalDiscount} = this.state;
        let auxLines = [];
        let _totalTaxes = TotalTaxes;
        let auxLineToDelete = Lines.filter(x=>x.id === id);
        let newTotal = parseFloat(total)-parseFloat(auxLineToDelete[0]?.subtotal);
        let newSubTotalAmount = this.ToDecimal(parseFloat(subTotalAmount) - (parseFloat(auxLineToDelete[0]?.price)*parseInt(auxLineToDelete[0]?.Qtty)));
        let newTotalDiscount = TotalDiscount - parseFloat(auxLineToDelete[0]?.TotalDiscount)
        auxLines = Lines.filter(x=>x.id!==id)


        console.log("impuesto a bscar",auxLineToDelete[0].taxId);
        const index = _totalTaxes.findIndex(item => item.id === auxLineToDelete[0].taxId);
        if(index>-1){
            _totalTaxes[index] = {..._totalTaxes[index], total:this.ToDecimal(parseFloat(_totalTaxes[index].total)- auxLineToDelete[0].taxCalculated)}
            if(_totalTaxes[index].total===this.ToDecimal(0)){
                _totalTaxes.splice(index,index+1);
            }
        }

        this.setState({Lines:auxLines,total: newTotal, TotalTaxes:_totalTaxes,subTotalAmount:newSubTotalAmount, TotalDiscount:newTotalDiscount});
    }

    emptyTrash=()=>{
        let confirmx = window.confirm("Realmente deseas limpiar todas las lineas agregadas?");
        if(confirmx){
            this.setState({Lines:[]})
        }
    }
    handleChange=(e,{name,value})=>{
        this.setState({[name]:value});
        console.log(name,value)
    }

    handleChangeTax =(e,{name,value})=>{
        this.setState({[name]:value},()=>(this.calculateTotal()));
        
    }
    handleUpdateLine=(id,e)=>{
        let {Lines} = this.state;
        let lAux = [...this.state.Lines]
        let {name,value} = e.target;
        // let result = Lines.filter(x=>x.id!==id);
         let ToUpdate = Lines.filter(x=>x.id===id);
         const index = Lines.findIndex(item => item.id === id);
         lAux[index] = {...lAux[index], [name]:value}
            this.setState({Lines:lAux});
    }

    handleChangeToPayed = (e,{name,checked})=>{
        this.setState({
            [name]: checked,
            showSaveBtn:checked
        });
    }

    handleMarkAsPaid = (invoiceId) =>{
        console.log("propiedades",this.props)
        this.props.dispatch(
            markAsPaid({invoiceId:invoiceId})
        )
    }
    handleChangeQtty = (e,{name,value})=>{
        let {price,taxId} = this.state;
    
        // let _total = this.ToDecimal(0)
        // let tax = Tax.taxById(parseInt(taxId));
        // let taxValue = tax.Value;
        // let TaxCalculated = (e.target.value*price*taxValue)/100;
        // _total = this.ToDecimal(parseFloat(value*price) + parseFloat(TaxCalculated));
    
        // this.setState({ subtotal:_total });
        
    
        this.setState({[name]: parseInt(value)},()=>(this.calculateTotal()));
      }

      handleChangeDiscount=(e)=>{
        let {name,value} = e.target;
        
        let {Qtty,price,taxId} = this.state;
        let calculatedDiscount = 0;
        let percent = value/100;
        let _subTotal = 0;
        let _total = this.ToDecimal(0);
  
        calculatedDiscount = (price*Qtty)*percent;
        
        _subTotal =this.ToDecimal((price*Qtty)-calculatedDiscount);
  
        
        let tax = Tax.taxById(parseInt(taxId));
        let taxValue = tax.Value;
        let TaxCalculated = (_subTotal * taxValue)/100;
        _total = this.ToDecimal(parseFloat(_subTotal) + parseFloat(TaxCalculated));
  
        this.setState({
            [name]:value,
            subtotal:parseFloat(_total),
            taxCalculated:parseFloat(TaxCalculated)
        });
    }

    handleCreateInvoice = () => {
        let { TotalDiscount, Lines,ClientName,ClientAddress,ClientEmail,IdentityDocumentTypeCode,DocumentTypeID,PosCode,ConceptCode,InvoiceDate,VatConditionCode,IdentityDocumentNumber,total,subTotalAmount,TotalTaxes } = this.state;
        let Taxes;
        let canCreate = true;

        if(Lines.length===0){
            canCreate = false;
            toast({
                title:'Nueva factura',
                description:'No hay productos/servicios a facturar',
                type:'error'
            });
        }
        if(InvoiceDate===''){
            toast({
                title:'Nueva factura',
                description:'Ingrese la fecha de emisión para la factura.',
                type:'error'
            });
        }

        if(VatConditionCode===0){
            canCreate = false;
            toast({
                title:'Nueva factura',
                description:'Seleccione condición de IVA.',
                type:'error'
            });
        }



        if(IdentityDocumentNumber===''){
            canCreate = false;
            toast({
                title:'Nueva factura',
                description:'Seleccione condición de IVA.',
                type:'error'
            });
        }

        if(canCreate)
        {
            this.props.dispatch(
                CreateInvoice(
                    {
                        ClientName:ClientName,
                        ClientAddress:ClientAddress,
                        ClientEmail:ClientEmail,
                        PosCode:PosCode,
                        IdentityDocumentTypeCode:IdentityDocumentTypeCode,
                        IdentityDocumentNumber:IdentityDocumentNumber,
                        DocumentTypeID:DocumentTypeID,
                        ConceptCode:ConceptCode,
                        InvoiceDate:InvoiceDate,
                        VatConditionCode:VatConditionCode,
                        Total: total,
                        Subtotal:subTotalAmount,
                        Items: Lines,
                        TotalDiscount:TotalDiscount,
                    }
                )
            ).then(x=>{this.setState({showMessage:true})});
        }
        
    }
    calculateTotal =()=>{

        // subtotal = ((precio * qtty)+-descuento) + iva
        
        let {price, Qtty, taxId,discount,subtotal} = this.state;
        
        let calculatedDiscount = 0;
        let discountPercent = discount/100;
        let _subTotal = 0;
        let _total = this.ToDecimal(0);
  
        calculatedDiscount = (price*Qtty)*discountPercent;
        
        _subTotal =this.ToDecimal((price*Qtty)-calculatedDiscount);
  
        
        let tax = Tax.taxById(parseInt(taxId));
        let taxValue = tax.Value;
        let TaxCalculated = (_subTotal * taxValue)/100;
        _total = this.ToDecimal(parseFloat(_subTotal) + parseFloat(TaxCalculated));
  
        this.setState({
            subtotal:_total,
            taxCalculated:TaxCalculated,
            calculatedDiscount:calculatedDiscount
        });



    }

    handleChangeUOM = ({name,value})=>{

    }

    handleChangePrice=(e,{name,value})=>{
        // let _Total = this.handleCalculateTotal();
        this.setState({[name]:value},()=>(this.calculateTotal()));
    }
    
    // handleChangeTax = ()=>{}

 

     handleCalculateTotal=()=>{
    let {price, Qtty, taxId, discount} = this.state;
    let subtotal = 0;
        subtotal = parseFloat(price)*Qtty;
    return subtotal;
    }
    
    async componentDidMount(){
        this.props.dispatch(
            fetchIdentityDocumentType()
        );
        this.props.dispatch(
            fetchVatCondition()
        );

        this.props.dispatch(
            fetchDocumentType(),
        );
    }
    render(){
        let {invoiceTypeOptions,conceptsOptions,PaymentMethod, Lines,Description,Qtty, taxes, UOM,discount,subtotal,price,unitOfMeasurementID,taxId,total, TotalTaxes,subTotalAmount, PosId,
            DocumentTypeID,
            InvoiceDate,
            ConceptCode,
            VatConditionCode,
            ClientName,
            DocumentTypeCode,
            DocumentNumber,
            ClientAddress,
            ClientEmail,
            showMessage,
            TotalDiscount,
            Payed} = this.state;
        return(
            <>
            <Button onClick={()=>(this.setState({showMessage:true}))}></Button>
             <Modal
                className='success'
                open={showMessage} 
                size='mini'
                >
                <Modal.Header>Información de transacción</Modal.Header>
                <Modal.Content image>
                <Container textAlign='center'>
                    {
                        this.props.result !== "" ? 
                        <Icon name='check circle outline' color='green' size='huge'></Icon>
                        :""
                    }
                    
                    <Divider></Divider>
                    <Table basic='very'>
                        <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell></Table.HeaderCell>
                            <Table.HeaderCell></Table.HeaderCell>
                        </Table.Row>
                        </Table.Header>

                        <Table.Body>
                        <Table.Row>
                            <Table.Cell><b>CAE:</b></Table.Cell>
                            <Table.Cell>{(this.props.cae!== ""? this.props.cae : 'No obtenido')}</Table.Cell>
                        </Table.Row>
                        <Table.Row>
                            <Table.Cell><b>Vencimiento:</b></Table.Cell>
                            <Table.Cell>{(this.props.dueDateCae!== null? this.props.dueDateCae: 'No obtenido')}</Table.Cell>
                        </Table.Row>

                        </Table.Body>
                    </Table>   
                    <Divider></Divider>

                    <Form.Group grouped >
                        <Checkbox label='Marcar como pagada.' onClick={this.handleChangeToPayed} name='Payed'></Checkbox>
                    </Form.Group>
                    <Form.Group grouped >
                        <Checkbox label='Enviar adjunto al email' onClick={this.handleChangeToPayed} name='Payed'></Checkbox>
                    </Form.Group>
                </Container>
                
                    
                </Modal.Content>
                <Modal.Content>
                {
                    (this.props.observations!== null ?
                        <Message
                        error 
                        header='Verifique y corrija los siguientes errores.'
                        list={[this.props.observations?.map((x,i)=>(
                            x.description
                        ))]}
                    />:null)
                }
                </Modal.Content>
                <Modal.Actions>
                    

                    {
                        Payed?
                        <Button
                        primary
                            content="Aceptar y guardar"
                            labelPosition='right'
                            icon='checkmark'
                            onClick={()=>(this.handleMarkAsPaid(this.props.invoiceId))}
                            positive
                        />
                        :
                        <Button
                        content="Cerrar"
                        labelPosition='right'
                        icon='checkmark'
                        onClick={()=>(this.closeMessage())}
                        positive
                        />
                    }
                </Modal.Actions>
                </Modal>
                
            <Header as='h2'>
                <Icon name='file alternate outline' />
                    <Header.Content>
                        Facturación 
                    <Header.Subheader>Emisión de facturas electrónicas</Header.Subheader>
                </Header.Content>
            </Header>
            <Container fluid>
                <Segment>

                <Form>
                   <Form.Group>
                   <SelectPos handleChange={this.handleChange} size='6' label='Punto de venta'/>
                   <Form.Field width={this.props?.size}>
                    <label>Típo de documento</label>
                    <Select name='DocumentTypeID' onChange={this.handleChange} options={this.props.documentType?.map((item,idx)=>({key:item.documentTypeID,value:item.documentTypeID,text: item.code + " " + item.name + " " + item.letter }))}></Select>
                    </Form.Field>
                    <Form.Input name='InvoiceDate' onChange={this.handleChange} type='date' label='Fecha de comprobante'></Form.Input>
                    <Form.Field width={this.props?.size}>
                    <label>Concepto</label>
                    <Select name='ConceptCode'  onChange={this.handleChange} options={conceptsOptions}></Select>
                    </Form.Field>      
                   </Form.Group>
             
                   <Form.Group inline>
                   <label>Condición de venta</label>
                   <Form.Field
                        control={Radio}
                        label='Pago contado'
                        value='1'
                        name='PaymentMethod'
                        checked={PaymentMethod === '1'}
                        onChange={this.handleChange}
                    />
                    <Form.Field
                        control={Radio}
                        label='Pago tarjeta crédito'
                        value='2'
                        name='PaymentMethod'
                        checked={PaymentMethod === '2'}
                        onChange={this.handleChange}
                    />
                   </Form.Group>
                   <Divider></Divider>
                   <Form.Group>
                   <Form.Field
                        width='4'
                        id='form-input-control-first-name'
                        control={Select}
                        onChange={this.handleChange}
                        options={this.props.vatCondition?.map((i,ix)=>({key:i.code,value:i.code,text:i.name}))}
                        label='Condición frente al IVA'
                        name='VatConditionCode'
                        autocomplete='false'
                    />
                    <Form.Field
                        width='4'
                        id='form-input-control-first-name'
                        control={Select}
                        name='IdentityDocumentTypeCode'
                        onChange={this.handleChange}
                        options={this.props.identityDocumentType?.map((i,ix)=>({key:i.identityDocumentTypeID,value:i.code,text:i.name}))}
                        label='Típo de documento'
                    />
                    <Form.Field
                        width='2'
                        id='form-input-control-first-name'
                        control={Input}
                        onChange={this.handleChange}
                        value={DocumentNumber}
                        name='IdentityDocumentNumber'
                        label='Nro. Documento'
                        autoComplete='off'
                    />
                    </Form.Group>
                   <Form.Group>
                    <Form.Field
                    width='5'
                        id='form-input-control-first-name'
                        control={Input}
                        label='Apellido y nombre / Rasón social'
                        name='ClientName'
                        onChange={this.handleChange}
                        autoComplete='off'
                    />
                    <Form.Field
                        width='5'
                        id='form-input-control-last-name'
                        control={Input}
                        label='Dirección'
                        name='ClientAddress'
                        placeholder=''
                        onChange={this.handleChange}
                        autoComplete='off'
                    />
                    <Form.Field
                        width='4'
                        control={Input}
                        label="Email"
                        name='ClientEmail'
                        onChange={this.handleChange}
                        autoComplete='off'
                    />
                    </Form.Group>
               </Form>
               </Segment>                

                <Segment >
                    <Table Table celled basic='very' collapsing compact='very'>
                        <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell width={5}>Descripción</Table.HeaderCell>
                            <Table.HeaderCell width={1}>Cantidad</Table.HeaderCell>
                            <Table.HeaderCell width={2}>U. Medida</Table.HeaderCell>
                            <Table.HeaderCell width={2}>$/Unidad.</Table.HeaderCell>
                            <Table.HeaderCell width={1}>% Bon.</Table.HeaderCell>
                            <Table.HeaderCell width={2}>IVA</Table.HeaderCell>
                            <Table.HeaderCell width={2}>subtotal</Table.HeaderCell>
                            <Table.HeaderCell ></Table.HeaderCell>
                        </Table.Row>
                        </Table.Header>

                        <Table.Body>
                            
                        {Lines && Lines.map((item)=>(
                            <Table.Row key={item.id}>
                            {/* <Table.Cell > <Form.Input fluid  /></Table.Cell> */}
                                <Table.Cell><Form.Input fluid name='Description' disabled value={item.Description} onChange={(e)=>(this.handleUpdateLine(item.id,e))} /></Table.Cell>
                                <Table.Cell ><Form.Input type='number' name='Qtty' value={item.Qtty} onChange={(e)=>(this.handleUpdateLine(item.id,e))} fluid /></Table.Cell>
                                <Table.Cell ><Form.Select fluid name='unitOfMeasurementID' value={item.unitOfMeasurementID} onChange={(e)=>(this.handleUpdateLine(item.id,e))} options={UOM}/></Table.Cell>
                                <Table.Cell ><Form.Input name='price' value={item.price} onChange={(e)=>(this.handleUpdateLine(item.id,e))} fluid /></Table.Cell>
                                <Table.Cell ><Form.Input name='discount' value={item.discount} onChange={(e)=>(this.handleUpdateLine(item.id,e))} fluid /></Table.Cell>
                                <Table.Cell ><Form.Select fluid name='taxId' value={item.taxId} onChange={(e)=>(this.handleUpdateLine(item.id,e))} options={taxes}/></Table.Cell>
                                <Table.Cell ><Form.Input name='subtotal' fluid value={item.subtotal} onChange={(e)=>(this.handleUpdateLine(item.id,e))} /></Table.Cell>
                                <Table.Cell ><Button fluid color='red' icon='minus' size='mini' onClick={()=>(this.removeLine(item.id))}></Button></Table.Cell>
                            </Table.Row>
                        ))}
                        <Table.Row key='0'>
                            {/* <Table.Cell > <Form.Input fluid  /></Table.Cell> */}
                            <Table.Cell><Form.Input autoComplete='off' name='Description'  value={Description} onChange={this.handleChange} fluid /></Table.Cell>
                            <Table.Cell ><Form.Input type='number' name='Qtty' value={Qtty} onChange={this.handleChangeQtty} fluid /></Table.Cell>
                            <Table.Cell ><Form.Select fluid name='unitOfMeasurementID' value={unitOfMeasurementID} onChange={this.handleChange} options={UOM}/></Table.Cell>
                            <Table.Cell ><Form.Input fluid name='price' value={price} onChange={this.handleChangePrice} /></Table.Cell>
                            <Table.Cell ><Form.Input fluid name='discount' value={discount} onChange={this.handleChangeDiscount} /></Table.Cell>
                            <Table.Cell ><Form.Select fluid name='taxId' onChange={this.handleChangeTax} value={taxId} options={taxes}/></Table.Cell>
                            <Table.Cell ><Form.Input fluid name='subtotal' onChange={this.handleChange} value={subtotal}  /></Table.Cell>
                            <Table.Cell ><Button fluid color='teal' icon='add' size='mini'  onClick={this.addLine}></Button></Table.Cell>

                            
                        </Table.Row>
                        </Table.Body>
                    </Table>
                </Segment>
                <Grid columns={3} >
                    <Grid.Row >
                    <Grid.Column >
                    </Grid.Column>
                    <Grid.Column>

                    </Grid.Column>
                    <Grid.Column>
                    <Table definition>
                    <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell width='6' />
                        <Table.HeaderCell textAlign='center'>Totales</Table.HeaderCell>
                    </Table.Row>
                    </Table.Header>

                    <Table.Body>
   
                    <Table.Row>
                        <Table.Cell textAlign='right'>Subtotal</Table.Cell>
                        <Table.Cell textAlign='right'>{subTotalAmount}</Table.Cell>
                    </Table.Row>
                    <Table.Row>
                        <Table.Cell textAlign='right'>Descuentos</Table.Cell>
                        <Table.Cell textAlign='right'>{TotalDiscount}</Table.Cell>
                    </Table.Row>
                    {
                        TotalTaxes.map((item,i)=>(
                            
                            <Table.Row key={i}>
                                <Table.Cell textAlign='right'>{Tax.taxById(parseInt(item.id)).Name}</Table.Cell>
                                <Table.Cell textAlign='right'> {item.total} </Table.Cell>
                            </Table.Row>
                        ))
                    }
                    <Table.Row >
                        <Table.Cell textAlign='right'><h2>Total</h2></Table.Cell>
                        <Table.Cell textAlign='right'> <h2>{this.ToDecimal(total)}</h2> </Table.Cell>
                    </Table.Row>
                    </Table.Body>
                    <Table.Footer  fullWidth>
                    <Table.Row  colSpan='2'>
                        <Table.HeaderCell colSpan='2'>
                        <Button
                            floated='right'
                            size='big'
                            icon
                            labelPosition='left'
                            color='teal'
                            fluid
                            onClick={()=>(this.handleCreateInvoice())}
                        >
                            <Icon name='check' /> Crear factura
                        </Button>
                       </Table.HeaderCell>
                    </Table.Row>
                    </Table.Footer>
                </Table>
                    </Grid.Column>
                    </Grid.Row>
                
                </Grid>

            </Container>

            </>
        )
    }

}
function mapStateToProps(state){
    return{
        isFetching: state.invoices.isFetching,
        identityDocumentType: state.identityDocumentType.identityDocumentTypes,
        vatCondition: state.vatCondition.vatCondition,
        documentType: state.documentType.DocumentType,
        cae: state.invoices.cae,
        dueDateCae: state.invoices.dueDateCae,
        result: state.invoices.result,
        invoiceId: state.invoices.invoiceId,
        observations: state.invoices.observations,
    }
}

export default connect(mapStateToProps)(EditForm);