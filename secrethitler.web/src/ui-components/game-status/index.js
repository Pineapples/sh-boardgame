import React, { Component } from 'react';

import './style.scss'

class PlayerComponent extends Component {
	render() {
		const { data } = this.props
		return <div> { data && data.length ? data : 'No game data to show'} </div>
	}
}

export default PlayerComponent
