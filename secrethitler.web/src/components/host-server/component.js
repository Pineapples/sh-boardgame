import React, { Component } from 'react';
import request from 'request'

import APIURL from '../../config.json';

class ServerComponent extends Component {
	//create server POST call.
	createServer = (event) => {
		request.post({url: APIURL.APIURL + 'Game', form: {}}, ( err, res, body ) => {
			if (body) {
				this.props.actions.createServer(JSON.parse(body))
			}
		})
	}

	socketTest = (event) => {
		console.log(APIURL.APIURL + 'Game/' + this.props.server.id)
		request.get(APIURL.APIURL + 'Game/'+ this.props.server.id, (err, res, body) => {
			console.log(err, res, 'body:',JSON.parse(body))
		})
	}

	render() {
		const server = this.props.server
		return(
			<div>
				<button onClick={this.createServer}>Create a new server</button>
				<pre>
					JoinKey:
					{
						server
							? server.joinKey
							: ''
					}
				</pre>
					<button onClick={this.socketTest}>klik mij voor een socket test</button>

			</div>
		)
	}
}

export default ServerComponent
