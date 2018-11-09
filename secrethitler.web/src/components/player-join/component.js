import React, { Component } from 'react';

import JoinServer from './join-server';
import GameStatus from '../../ui-components/game-status'


class PlayerComponent extends Component {
	render() {
		const { player, actions } = this.props
		const { userName } = player

		// If the playerName is null, 0 or undefined,
		// it will return <joinserver> as joinserver is also defined.
		return <div>
			<GameStatus data={userName} />

			{
				!userName && <JoinServer joinServer={actions.joinServer} />
			}
		</div>
	}
}

export default PlayerComponent
