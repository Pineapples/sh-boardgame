import React, { Component } from 'react';
// import './style.css';

class JaNeinScreen extends Component {
	render() {
		const { electedPlayers } = this.props
		const style = "card six columns";
		return (
			<div>
				<h3>President: {electedPlayers[0]}. Chancellor: {electedPlayers[1]}</h3>
				<div className="voting-drawer twelve columns">
					<div className={style}>Ja</div>
					<div className={style}>Nein</div>
				</div>
			</div>
		)
	}
}

export default JaNeinScreen
