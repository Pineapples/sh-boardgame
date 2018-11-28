import { connect } from 'react-redux';
import { bindActionCreators } from 'redux'

import * as actions from '../../store/actions'

import Component from './component'

const mapStateToProps = (state) => {
	console.log('The store has state: ', state)
	return {
		server: state.server
	}
};

//map dispatch actions to the properties of the components (lest we forget)
const mapDispatch = dispatch => {
	return {
		actions: bindActionCreators(actions, dispatch)
	}
}

export default connect(mapStateToProps,mapDispatch)(Component);
