import React, { Component } from 'react';

import JoinServer from './join-server';
import GameHeader from '../../ui-components/game-header';
import PlayerCards from '../../ui-components/player-cards';


class PlayerComponent extends Component {
	constructor(props) {
		super(props);
		this.state = {
			game: 'voting-round'
		}
	}
	render() {
		const { player, actions } = this.props
		const { userName } = player

		//fake data.
		const playerList = [
			"kees", "Sjaak", "Harry", "Barry", "Henk", "Klaas"
		];
		//Component list. This will link each game state to a component. We can give different props for each.
		//TODO: if player needs to log in: don't show the other components.
		const components = {
			'login': () => <JoinServer joinServer={actions.joinServer} />,
			'wait-for-game-start': () => null,
			'voting-round': () => <PlayerCards players={playerList} />
		}
		// If the playerName is null, 0 or undefined,
		// it will return <joinserver> as joinserver is also defined.
		return (
			<div>
				<GameHeader playerName={userName} gameState={this.state.game} />
				<div id="gamebody">
					{
						components[this.state.game]()
					}
				</div>

			</div>
		)
	}
}

export default PlayerComponent
