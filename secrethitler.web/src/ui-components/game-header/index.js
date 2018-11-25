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
			'Vote-For-Government': 'Vote for the proposed government'
		}
		return <div>
			<h2>
			{ headerText[gameState]}
			</h2>
		</div>
	}
}

export default GameHeader
