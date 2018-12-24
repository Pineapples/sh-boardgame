import * as SignalR from '@aspnet/signalr';

const connection = new SignalR.HubConnectionBuilder()
	.withUrl('http://localhost:5000/GameConnectionHub')
	.configureLogging(SignalR.LogLevel.Information)
	.build();

export function signalRRegistration(store){
	connection.on("GameInfo", (game) => {
		console.log("RECEIVED GameInfo", game)
		store.dispatch({type: 'UPDATE_GAME', payload: game});
	});


	// TODO handle closing connections or failing to start connection (by retrying)
	// connection.onclose(async () => {
	//     await start();
	// });

	connection.start()

};
