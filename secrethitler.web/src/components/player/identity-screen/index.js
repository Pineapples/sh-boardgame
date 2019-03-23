import React, { Component } from 'react';
import { CSSTransitionGroup } from 'react-transition-group';

import {roleStrings} from '../../../helpers/roleTranslator';
import './style.css';

class IdentityScreen extends Component {
	constructor() {
		super()
		this.handleButtonPress = this.handleButtonPress.bind(this)
		this.handleButtonRelease = this.handleButtonRelease.bind(this);
		this.state = {
			screenOpen: false
		}
	}
	handleButtonPress () {
		this.setState({
			screenOpen: true
		})
	}

	handleButtonRelease () {
		this.setState({
			screenOpen: false
		})
	}
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

		const isOpened = this.state.screenOpen === true ? 'opened' : 'closed';
		return(
			<div
			onTouchStart={this.handleButtonPress}
			onTouchEnd={this.handleButtonRelease}
			onMouseDown={this.handleButtonPress}
			onMouseUp={this.handleButtonRelease}
			id="identity-screen" className={isOpened} >
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
