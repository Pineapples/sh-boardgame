import request from 'request'
import API from '../../config.json'

const actions = {
	'POST': (store, action, next) => {
		let headers = {'Content-Type': 'application/json'};
		console.log('POSTING DEM SWEET DATAS', action)
		//add the X-Player header to calls that need this.
		if(action.fetch.headers && action.fetch.headers['X-Player']){
			headers = {...headers, 'X-Player' : store.getState().player.id}
		};

		request.post({
			url: API.URL + action.fetch.url,
			body: action.payload,
			headers: headers
		}, ( err, res, body ) => {
			if (body) {
				console.log('dit heb ik gefetchdtd', JSON.parse(body));
				action.payload = JSON.parse(body);
				next(action)
			}
		})
	},
	'GET': (store, action, next) => {
		request.get(API.URL + action.fetch.url, (err, res, body) =>{
			if (body){
				console.log('Deze heb k geget', JSON.parse(body));
				action.payload = JSON.parse(body);
				next(action)
			}
		})
	},
	'DELETE': (action, next) => {
		next(action)
	},
	'PATCH': (action, next) => {
		next(action)
	}
}

//Hardcore API middleware.
//Alle HTTP requests gaan hier langs. We filteren alle acties met een fetch.
export const apiMiddleware = store => next => action => {
	const type = action && action.fetch && action.fetch.type
	if (type && actions[type]) {
		actions[type](store, action, next)
	} else {
		next(action)
	}
}
