import React, { Component } from 'react';
import './style.css';

class PlayerCards extends Component {
	render() {
		const { players } = this.props;
		console.log('rendering player cards', this.props);
		const style = "card six columns";

		return (
			<div>
				{
					players.map((player,index) =>
						<div className={style} onClick={() => this.props.choosePlayer(this.props.gameId, player.id)} key={player.UserName}>{player.UserName}</div>
					)
				}
			</div>
		)
	}
}

export default PlayerCards
