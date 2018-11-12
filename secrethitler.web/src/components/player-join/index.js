/* component player interface
 * main container for the player.
*/
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux'
import PlayerComponent from './component'

import * as actions from '../../store/actions'

const mapStateToProps = (state) => {
	console.log('The store has state: ', state)
	return {
		player: state.player
	}
};

const mapDispatch = dispatch => {
	return {
		actions: bindActionCreators(actions, dispatch)
	}
}

export default connect(mapStateToProps, mapDispatch)(PlayerComponent);
