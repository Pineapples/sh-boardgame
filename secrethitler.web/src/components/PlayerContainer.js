/* component player interface
 * main container for the player.
*/

import React, { Component } from 'react';
import { connect } from 'react-redux';
import JoinServer from './JoinServer';

const mapStateToProps = (state) => {
    console.log('The store has state: ', state)
    return {
        player: state.player
    }
};

class PlayerContainer extends Component {
    //If redux store has a player name + id then render game object
    //otherwise render a login component. WOW
    //j6r31uvl
    render() {
        //dit is nu heel raunchy. Welke prop krijgen we mee waar dit wel even goed bij is. GameID misschien?
        let playerName = '';
        if(this.props.player.playerData !== undefined){
            playerName = this.props.player.playerData.userName
        }
        return(
            <div>
                {playerName.length > 0 ?
                    'Wauw je bent ingelogd ' + playerName : <JoinServer />}
                </div>

        )
    }
}

export default connect(mapStateToProps)(PlayerContainer);
