
function StartBattleshipGame(sessionId) {
    var hub = location.protocol + `//${document.location.host}/battleshipHub`;
    var http = new signalR.HttpConnection(hub, { transport: signalR.TransportType.WebSockets });
    var connection = new signalR.HubConnection(http);

    connection.start()
        .then(() => connection.invoke('JoinGame', sessionId)); //wywoływane w trakcie tworzenia połączenia

    connection.on('updateExampleValueInView', //obsługa wywołania serwer -> klient
        (newValue) => {
            console.log('New value ' + newValue);
            $("#exampleValue").html(newValue);
        }
    );

    $("#updateExampleButton").click(function () {
        connection.invoke('UpdateExample', sessionId); // wywołanie klient -> serwer
    });
}