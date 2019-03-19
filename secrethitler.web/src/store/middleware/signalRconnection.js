import * as SignalR from '@aspnet/signalr';

const connection = new SignalR.HubConnectionBuilder()
	.withUrl('http://localhost:5000/GameConnectionHub')
	.configureLogging(SignalR.LogLevel.Trace)
	.build();

export function signalRRegistration(store){
	connection.on("GameInfo", (game) => {
		console.log("RECEIVED GameInfo", game);
	});

	connection.on("StartGame", (game) => {
		store.dispatch({
			type: "UPDATE_GAME",
			payload: game
		})
	})
	// TODO handle closing connections or failing to start connection (by retrying)
	// connection.onclose(async () => {
	//     await start();
	// });

	connection.start();
};

//joinGame is called from the signalRMiddleware.
//This will add a player to a signalR group
export function joinGame(playerId) {
	connection.invoke('JoinGame', playerId);
}
