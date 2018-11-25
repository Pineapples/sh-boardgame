import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link, Switch } from "react-router-dom";
import './style/normalize.css';
import './style/skeleton.css';
import './style/App.css';

import PlayerContainer from './components/player';
import ServerContainer from './components/host-server';

class App extends Component {
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
