import React, { Component } from 'react';

// import './style.scss'

class GameHeader extends Component {
	render() {
		const { playerName, gameState } = this.props;
		const headerText = {
			'login': 'Login with your name and the ID of the server',
			'Open': 'Welcome ' + playerName + ', Wait for game start',
			'Choose-President': 'Vote for the first president',
			'Choose-Chancellor': 'Pick your chancellor',
			'Vote-For-Government': 'Place your ballod for the electorate'
		}
		return <div>
			<h2>
			{ headerText[gameState]}
			</h2>
		</div>
	}
}

export default GameHeader
