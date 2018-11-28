/*
 * src/store.js
*/

import { createStore, applyMiddleware } from 'redux';
import { persistStore, persistReducer } from 'redux-persist';
import storageSession from 'redux-persist/lib/storage/session'
import autoMergeLevel2 from 'redux-persist/lib/stateReconciler/autoMergeLevel2';

import thunk from 'redux-thunk';
import { apiMiddleware } from './middleware/apiMiddleware.js';
import rootReducer from './reducers';

const persistConfig = {
 key: 'root',
 storage: storageSession,
 stateReconciler: autoMergeLevel2
};

const pReducer = persistReducer(persistConfig, rootReducer);

export const configureStore = createStore(pReducer, applyMiddleware(thunk, apiMiddleware));
export const persistor = persistStore(configureStore);
