import React, { Component } from 'react';
import request from 'request'

import APIURL from '../../../config.json';

class JoinServer extends Component {
	// Creating the refs in the constructor so that the entire component has access to them.
	constructor(props){
		super(props);
		this.playerName = React.createRef();
		this.joinKey = React.createRef();
	}
	//doe request om server te joinen!
	//on succes update redux store.
	//deze component zou dan moeten unmounten.
	joinServer = event => {
		// console.log('joining the server ', APIURL);
		// console.log(event, this.playerName.current.value, this.joinKey.current.value);
		let userName = this.playerName.current.value;
		let gameID = this.joinKey.current.value;

		const deBody = JSON.stringify({"userName": userName})

		request.post({
			url: APIURL.APIURL + 'Game/Join/' + gameID,
			body: deBody,
			headers: {
				'Content-Type': 'application/json'
			}
		}, ( err, res, body ) => {
			if (body) {
				this.props.joinServer(JSON.parse(body))
			}
		})
	}
	render() {
		return <div>
			<label>Enter your username: <input ref={this.playerName} type="text" /></label>
			<label>Enter the server code: <input ref={this.joinKey} type="text"/></label>
			<button onClick={ this.joinServer }>
				Join server
			</button>
		</div>
	}
}

export default JoinServer
