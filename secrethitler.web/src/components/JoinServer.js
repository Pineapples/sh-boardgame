import React, { Component } from 'react';
import { connect } from 'react-redux';
import APIURL from '../config.json';
import { joinServer } from '../actions/playerActions';

const mapDispatchToProps = dispatch => ({
    joinGame: (playerData) => dispatch(joinServer(playerData))
});

class JoinServer extends Component {
    // Creating the refs in the constructor so that the entire component has access to them.
    constructor(props){
        super(props);
        this.playerName = React.createRef();
        this.joinKey = React.createRef();
    }
//doe request om server te joinen!
//on succes update redux store.
//deze component zou dan moeten unmounten.
    joinServer = (event) => {
        // console.log('joining the server ', APIURL);
        // console.log(event, this.playerName.current.value, this.joinKey.current.value);
        let userName = this.playerName.current.value;
        let gameID = this.joinKey.current.value;

        let deBody = {"userName": userName};
        deBody = JSON.stringify(deBody);
        var request = new Request(APIURL.APIURL + 'Game/Join/' + gameID, {
            method: 'POST',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: deBody
        });
        // console.log("Dit is mijn eerste request: ", request, request.body);
        fetch(request).then(function(responseObject){
            console.log("Dit is mijn response object:", responseObject)
            return responseObject.json();
        }).then(data => {
            console.log(data);
            this.props.joinGame(data);
        })
    }
    render() {
        return(
            <div>
                <label>Enter your username: <input ref={this.playerName} type="text" /></label>
                <label>Enter the server code: <input ref={this.joinKey} type="text"/></label>
                <button onClick={ this.joinServer }>
                    Join server
                </button>
            </div>

        )
    }
}

export default connect(null, mapDispatchToProps)(JoinServer);
