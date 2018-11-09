import React, { Component } from 'react';
import request from 'request'

import APIURL from '../../config.json';

class ServerComponent extends Component {
	constructor (props) {
		super(props)

		this.state = {}
	}
	//create server POST call.
	createServer = (event) => {
		request.post({url: APIURL.APIURL + 'Game', form: {}}, ( err, res, body ) => {
			if (body) {
				this.props.actions.createServer(JSON.parse(body))
			}
		})
	}

	componentWillReceiveProps(nextProps) {
		if (nextProps.server !== this.state.server) {
			this.setState({
				server: nextProps.server
			})
		}
	}

	render() {
		const {server} = this.state
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
			</div>
		)
	}
}

export default ServerComponent
