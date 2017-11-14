
function JoinLobby(lobbyId, gameName, minNumberOfPlayers) {
    console.log('lid: ' + lobbyId);
    console.log('gn: ' + gameName);
    var hub = `http://${document.location.host}/lobbyHub`;
    var http = new signalR.HttpConnection(hub, { transport: signalR.TransportType.WebSockets });
    var connection = new signalR.HubConnection(http);
    connection.start()
        .then(() => connection.invoke('JoinLobby', lobbyId, gameName));

    connection.on('updatePlayers',
        (players) => {
            console.log('Connected players: ' + players);

            //update number of players
            $("#numberOfPlayers").html(players.length);

            //update connected players lise
            $("#connectedPlayers").html("");
            for (var i = 0; i < players.length; i++) {
                $("#connectedPlayers").append('<li>' + players[i] + '</li>');
            }

            //show start game button
            if ($("#startGame").length) { //if lobby owner
                if (players.length >= minNumberOfPlayers) {
                    $("#startGame").show();
                } else {
                    $("#startGame").hide();
                }
            }
        }
    );

    connection.on('handleFullLobby',
        function() {
            $("#fullLobby").show();
        }
    );

    connection.on('startGame',
        (redirectUrl) => {
            window.location.replace(redirectUrl);
        }
    );


    $("#startGame").click(function () {
        connection.invoke('StartGame', lobbyId, gameName);
    });
}