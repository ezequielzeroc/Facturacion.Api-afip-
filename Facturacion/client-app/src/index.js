import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './components/App';
import * as serviceWorker from './serviceWorker';
import reducers from './reduces';
import { applyMiddleware, createStore } from 'redux';
import {Provider} from 'react-redux'
import  ReduxThunk  from 'redux-thunk'

const store = createStore(
  reducers,
  applyMiddleware(ReduxThunk)
)
ReactDOM.render(
  <Provider store={store}>
    {/* <React.StrictMode> */}
      <App />
    {/* </React.StrictMode> */}
  </Provider>,
  document.getElementById('root')
);

serviceWorker.unregister();
