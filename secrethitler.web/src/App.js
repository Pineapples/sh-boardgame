import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link, Switch } from "react-router-dom";
import './style/App.css';

import PlayerContainer from './components/PlayerContainer';
import ServerContainer from './components/ServerContainer';
// import { simpleAction } from './actions/simpleAction'

// const mapStateToProps = state => ({
// 	...state
// });
//
//
// const mapDispatchToProps = dispatch => ({
// 	simpleAction: () => dispatch(simpleAction())
// });


class App extends Component {
	//  simpleAction = (event) => {
	// 	this.props.simpleAction();
	// }


  render() {
	return (
	<Router>
		<div id="app">
			<Switch>
				<Route exact path="/" component={Home} />
		        <Route path="/server" component={ServerContainer} />
		        <Route path="/player" component={PlayerContainer} />
			</Switch>
		</div>
  	</Router>
	);
  }
}

//Home component renders the two links to decide which route to go.
//This is a different component so that the links can disappear after clicking.
const Home = () => (
	<div>
		<Link to="server"><button>Create game</button></Link>
		<Link to="player"><button>Join Game</button></Link>
	</div>
);

export default App;
