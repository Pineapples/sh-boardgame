import React, { Component } from 'react';

class ServerComponent extends Component {
	//create server POST call.
	createServer = (event) => {
		this.props.actions.createServer();
	}

	socketTest = (event) => {
		if(this.props.server.id) {
			this.props.actions.gameInfo(this.props.server.id)
		}
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
