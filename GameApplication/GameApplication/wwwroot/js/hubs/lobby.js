
function JoinLobby(lobbyId, token) {
    var hub = `http://${document.location.host}/lobbyHub`;
    var http = new signalR.HttpConnection(hub, { transport: signalR.TransportType.WebSockets });
    var connection = new signalR.HubConnection(http);
    connection.start()
        .then(() => connection.invoke('JoinLobby', lobbyId, token));
    connection.on('Send',
        (nick, message) => {
            appendLine(nick, message);
        });
}