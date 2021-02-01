export const Status = {
    Invoice
}
function Invoice(id)
{
    switch(id)
    {
        case 1:	return {Name:"Creada",Value:0};
        case 2:	return {Name:"Borrador",Value:0}; 
        case 3:	return {Name:"Anulada",Value:0};  
        case 4:	return {Name:"Autorizada",Value:0};  

        default:
            return {Name:"Sin estado",Value:0}; 
    }
}
