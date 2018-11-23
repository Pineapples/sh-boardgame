import React, { Component } from 'react';
import { CSSTransitionGroup } from 'react-transition-group';

import {roleStrings} from '../../../helpers/roleTranslator';
import './style.css';

class IdentityScreen extends Component {

	render() {
		const role = this.props.player && roleStrings[this.props.player.role];
		const party = role === 'Hitler' ? 'Fascist' : role;

		//Check whether a role has been filled.
		let bodyText;
		if(role) {
			bodyText = <p>You are {role === 'Hitler' ? 'Hitler, ' : null}
			 a member of the {party} party</p>;
		 } else {
			 bodyText = <p>Here you will find your secret identity when the game has started</p>;
		 }

		const isOpened = this.props.player.idScreenOpen === true ? 'opened' : 'closed';
		return(
			<div id="identity-screen" className={isOpened} onClick={this.props.toggleScreen}>
				<CSSTransitionGroup
				 transitionName="slide"
				 transitionEnterTimeout={250}
				 transitionLeaveTimeout={250}>
				{isOpened === 'opened' &&
						<div key={role}>
							{bodyText}
						</div>
				}
				</CSSTransitionGroup>
			</div>
		)
	}
}

export default IdentityScreen
