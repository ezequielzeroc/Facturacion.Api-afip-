export const Tax = {
    taxById
}
function taxById(id)
{
    switch(id)
    {
        case 1:	return {Name:"No gravado",Value:0}; //no gravado
        case 2:	return {Name:"Exento",Value:0};  //exento
        case 3:	return {Name:"IVA 0%",Value:0};  // 0%
        case 4:	return {Name:"IVA 10.50 %",Value:10.50}; 
        case 5:	return {Name:"IVA 21.00 %",Value:21.00}; 
        case 6:	return {Name:"IVA 27.00 %",Value:27.00}; 
        case 8:	return {Name:"IVA 5.00 %",Value:5.00}; 
        case 9:	return {Name:"IVA 2.5 %",Value:2.50}; 
        default:
            return {Name:"Sin IVA",Value:0};  // 0%
    }
}