import React, { Component } from 'react';

import './style.scss'

class GameHeader extends Component {
	render() {
		const { playerName, gameState } = this.props;
		const headerText = {
			'login': 'Login with your name and the ID of the server',
			'wait-for-game-start': 'welcome' + playerName + '. Wait for game start',
			'voting-round': 'Vote for the first president',
			'ja-nein': 'Place your ballod for the following government'
		}
		return <div>
			<h2>
			{ gameState === 'wait-for-game-start' & playerName !== undefined ? playerName : headerText[gameState]}
			</h2>
		</div>
	}
}

export default GameHeader
