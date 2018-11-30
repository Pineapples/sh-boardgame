import React, { Component } from 'react';

class JoinServer extends Component {
	// Creating the refs in the constructor so that the entire component has access to them.
	constructor(props){
		super(props);
		this.playerName = React.createRef();
		this.joinKey = React.createRef();
	}

	//Send action to join server
	joinServer = event => {
		let userName = this.playerName.current.value;
		let gameID = this.joinKey.current.value;
		//stringify
		const deBody = JSON.stringify({"userName": userName})
		this.props.joinServer(deBody, gameID);
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
