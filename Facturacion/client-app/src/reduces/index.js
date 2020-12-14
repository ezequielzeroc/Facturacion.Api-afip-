import { combineReducers } from 'redux';
import auth from './auth';
import account from './account';
import pos from './pos';
import invoices from './invoices';
import identityDocumentType from './IdentityDocumentType';
import vatCondition from './vatCondition';
import documentType from './documentType'
export default combineReducers({
    auth,
    account,
    pos,
    invoices,
    identityDocumentType,
    vatCondition,
    documentType,

});