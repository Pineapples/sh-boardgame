import React from 'react';
import ReactDOM from 'react-dom';

import { Provider } from 'react-redux'
import { PersistGate } from 'redux-persist/lib/integration/react';
import {configureStore, persistor} from './store/store';
import { signalRRegistration } from './store/middleware/signalRmiddleware.js';

import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';

const store = configureStore;
signalRRegistration(store);

ReactDOM.render(
	<Provider store={store}>
		<PersistGate loading={null} persistor={persistor}>
			<App />
		</PersistGate>
	 </Provider>,
 document.getElementById('root')
);

serviceWorker.unregister();
