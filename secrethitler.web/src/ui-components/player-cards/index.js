import React, { Component } from 'react';
import './style.css';

class PlayerCards extends Component {
	render() {
		const { players } = this.props
		const style = "card six columns";
		return (
			<div>
				{
					players.map((player,index) =>
						<div className={style} key={player}>{player}</div>
					)
				}
			</div>
		)
	}
}

export default PlayerCards
