import React, { Component } from 'react';
// import './style.css';

class PolicyPicker extends Component {
	render() {
		const policies = this.props.policies;
		// const style = "card six columns";
		return (
			<div>
				{
					policies.map((policy,index) =>
						<div key={index}>{policy}</div>
					)
				}
			</div>
		)
	}
}

export default PolicyPicker
