import request from 'request'
import API from '../../config.json'

const actions = {
	'POST': (action, next) => {
		request.post({
			url: API.URL + action.fetch.url,
			body: action.payload,
			headers: {
				'Content-Type': 'application/json'
			}
		}, ( err, res, body ) => {
			if (body) {
				console.log('dit heb ik gefetchdtd', JSON.parse(body));
				action.payload = JSON.parse(body);
				next(action)
			}
		})
	},
	'GET': (action, next) => {
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
		actions[type](action, next)
	} else {
		next(action)
	}
}
