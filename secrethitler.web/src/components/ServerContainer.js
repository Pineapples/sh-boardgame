import React, { Component } from 'react';
import { connect } from 'react-redux';
import APIURL from '../config.json';

import {createServer} from '../actions/serverActions';

const mapDispatchToProps = dispatch => ({
    storeServerData: (serverData) => dispatch(createServer(serverData))
});

const mapStateToProps = (state) => {
    console.log('The store has state: ', state)
    return {
        server: state.server.serverData
    }
};

class ServerContainer extends Component {
    //create server POST call.
    createServer = (event) => {
        console.log('creating server now', APIURL)
        var request = new Request(APIURL.APIURL + 'Game', {
    		method: 'POST',
    		headers: new Headers({
    			'Content-Type': 'application/json'
    		})
            //,body: body
    	});
    	// console.log("Dit is mijn eerste request: ", request, request.body);
    	fetch(request).then(function(responseObject){
    		console.log("Dit is mijn response object:", responseObject)
    		return responseObject.json();
		}).then(data => {
			// console.log(data);
            this.props.storeServerData(data);
            //dispatch action with data.
            //make server reducer, accept gameID and Joinkey.
            //mapstatetoprops so that this screen shows the joinkey.
            //If this has an ID/joinkey maybe also render other components?
		})
    }


    //dispatch action, server created

    render() {
        return(
            <div>
                <button onClick={this.createServer}>Create a new server</button>
                <pre>JoinKey: {this.props.server ? this.props.server.joinKey : ''}</pre>
            </div>
        )
    }
}

export default connect(mapStateToProps,mapDispatchToProps)(ServerContainer);
