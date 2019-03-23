import React, { Component } from 'react';

import JoinServer from './join-server';
import IdentityScreen from './identity-screen';
import GameHeader from '../../ui-components/game-header';
import PlayerCards from '../../ui-components/player-cards';
import JaNeinScreen from '../../ui-components/ja-nein-screen';
import PolicyPicker from '../../ui-components/policy-picker';
import { gameStateStrings } from '../../helpers/gameStateServerTranslator.js';

class PlayerComponent extends Component {
	constructor(props) {
		super(props);
		this.state = {
			game: !props.player.game ? 'login' : gameStateStrings[props.player.game.gameStateId]
			// game: 'Choose-President'
		}
	}
	//Function below updates screen based on new props received.
	//Will only update the state if the gameState has changed.
	componentWillReceiveProps(nextProps) {
		const newGameState = nextProps.player.game && gameStateStrings[nextProps.player.game.gameStateId];
		if(newGameState !== undefined && this.state.game) {
			this.setState({
				game: gameStateStrings[nextProps.player.game.gameStateId]
			})
		}
	}

	render() {
		const { player, actions } = this.props;
		const { userName } = player;
		const playerList = this.props.player.game && this.props.player.game.players;

		//fake data.
		// const playerList = [{UserName: "kees", id: 1}, {UserName: "Sjaak", id:2}, {UserName: "Harry", id:3}, {UserName: "Barry", id:4}, {UserName: "Henk", id:5}, {UserName: "Klaas", id:6}];
		const electedPlayers = playerList ? [playerList[0], playerList[1]] : null;
		 // player.role = 2; //HITLER
		const policyCards = ['Lib', 'Fac', 'Fac'];

		//Component list. This will link each game state to a component. We can give different props for each.
		//TODO: pass the role of the player to components and add logic whether to show controls for player or wait.
		const components = {
			'login': () => <JoinServer joinServer={actions.joinServer} />,
			'Open': () => null,
			'Choose-President': () => <PlayerCards gameId={player.gameId} choosePlayer={actions.choosePlayer} players={playerList} />,
			'Choose-Chancellor': () => <PlayerCards players={playerList} />,
			'Vote-For-Government': () => <JaNeinScreen electedPlayers={electedPlayers} />,
			'President-Policy-Pick': () => <PolicyPicker policies={policyCards} />,
			'Chancellor-Policy-Pick': () =>  <PolicyPicker policies={policyCards} />
		}

		return (
			<div id="game-container">
				<GameHeader playerName={userName} gameState={this.state.game} />
				<div id="game-body">
					{
						components[this.state.game]()
					}
				</div>
				<IdentityScreen player={player} />
			</div>
		)
	}
}

export default PlayerComponent
