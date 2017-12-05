function JoinLobby(lobbyId, gameName, minNumberOfPlayers) {
    var hub = location.protocol + `//${document.location.host}/lobbyHub`;
    var http = new signalR.HttpConnection(hub, { transport: signalR.TransportType.WebSockets });
    var connection = new signalR.HubConnection(http);
    connection.start()
        .then(() => connection.invoke('JoinLobby', lobbyId, gameName));

    connection.on('updatePlayers',
        (players) => {
            console.log('Connected players: ' + players);

            //update number of players
            $("#numberOfPlayers").html(players.length);



            //update connected players list
            $("#connectedPlayers").html("");
            for (var i = 0; i < players.length; i++) {
                // Prepare list elements
                var li = document.createElement("li");
                li.classList.add("mdl-list__item", "mdl-list__item--two-line");

                var personSpan = document.createElement("span");
                personSpan.classList.add("mdl-list__item-primary-content");

                var glyphIcon = document.createElement("i");
                glyphIcon.classList.add("material-icons", "mdl-list__item-avatar");
                if (i == 0) {
                    glyphIcon.append("stars");
                } else {
                    glyphIcon.append('person');
                }

                var personName = document.createElement("span");
                personName.append(players[i]);

                personSpan.append(glyphIcon);
                personSpan.append(personName);
                li.append(personSpan);

                $("#connectedPlayers").append(li);
            }

            //show start game button
            if ($("#startGame").length) { //if lobby owner
                if (players.length >= minNumberOfPlayers) {
                    $("#startGame").show();
                } else {
                    $("#startGame").hide();
                }
            }
        });

    connection.on('handleFullLobby',
        function() {
            $("#fullLobby").show();
            $("#successLobby").hide();
        }
    );

    connection.on('startGame',
        (redirectUrl) => {
            window.location.replace(redirectUrl);
        }
    );


    $("#startGame").click(function() {
        connection.invoke('StartGame', lobbyId, gameName);
    });
}