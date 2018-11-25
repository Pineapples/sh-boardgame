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
		console.log(props)
		this.state = {
			game: !props.player.game ? 'login' : gameStateStrings[props.player.game.gameStateId]
			// game: 'President-Policy-Pick'
		}
	}
	//Function below updates screen based on new props received.
	//Will only update the state if the gameState has changed.
	componentWillReceiveProps(nextProps) {
		const newGameState = nextProps.player.game && gameStateStrings[nextProps.player.game.gameStateId];
		if(newGameState !== undefined && this.state.game) {
			this.setState({
				game:gameStateStrings[nextProps.player.game.gameStateId]
			})
		}
	}

	render() {
		const { player, actions } = this.props;
		const { userName } = player;

		//fake data.
		const playerList = ["kees", "Sjaak", "Harry", "Barry", "Henk", "Klaas"];
		const electedPlayers = [playerList[0], playerList[1]]
		player.role = 2; //HITLER
		const policyCards = ['Lib', 'Fac', 'Fac'];

		//Component list. This will link each game state to a component. We can give different props for each.
		//TODO: onClick method to voting round (should invoke action, API call and change in store.)
		//TODO: pass the role of the player to components and add logic whether to show controls for player or wait.
		const components = {
			'login': () => <JoinServer joinServer={actions.joinServer} />,
			'Open': () => null,
			'Choose-President': () => <PlayerCards players={playerList} />,
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
				<IdentityScreen toggleScreen={actions.toggleIDScreen} player={player} />
			</div>
		)
	}
}

export default PlayerComponent
