import request from 'request'
import API from '../../config.json';

//Hardcore API middleware.
//Alle HTTP requests gaan hier langs. We filteren alle acties met een fetch.
export const apiMiddleware = store => next => action => {
	if(action.fetch) {
		if(action.fetch.type === 'POST'){
			console.log('posten met', API.URL + action.fetch.url, action.payload)
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
		} else if(action.fetch.type === 'GET'){
			console.log('getten met', API.URL + action.fetch.url, action.payload)
			request.get(API.URL + action.fetch.url, (err, res, body) =>{
				if (body){
					console.log('Deze heb k geget', JSON.parse(body));
					action.payload = JSON.parse(body);
					next(action)
				}
			})
		}

	} else {
		next(action);
	}
}
