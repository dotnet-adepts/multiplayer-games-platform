function StartBattleshipGame(sessionId) {
    hub = location.protocol + `//${document.location.host}/battleshipHub`;
    http = new signalR.HttpConnection(hub, { transport: signalR.TransportType.WebSockets });
    connection = new signalR.HubConnection(http);
    mySessionId = sessionId;

    connection.on('updateExampleValueInView', //obsługa wywołania serwer -> klient
        (newValue) => {
            console.log('New value ' + newValue);
            $("#exampleValue").html(newValue);
        }
    );

    connection.on('playerBoard',
        (board) => {
            myGameBoard = board;
        }
    );
    connection.on('opponentBoard',
        (board) => {
            shootingGameBoard = board;
            refresh();
        }
    );

    connection.on('yourTurn',
        () => {
            document.getElementsByTagName("p")[1].innerHTML = "Twoja tura";
            myTurn = true;
        }
    );

    connection.on('waitForOpponent',
        () => {
            document.getElementsByTagName("p")[1].innerHTML = "Tura przeciwnika";
            myTurn = false;
        }
    );

    connection.on('shipDown',
        (point) => {
            checkFire(SHIP_DESTROYED, point[0], point[1]);
            changeTurn()
        }
    );

    connection.on('shipMiss',
        (point) => {
            checkFire(EMPTY_HIT, point[0], point[1]);
            changeTurn()
        }
    );

    connection.on('gameOver',
        (point) => {
            checkFire(SHIP_DESTROYED, point[0], point[1]);
            //GAAAAAME OOOVER check whose turn it is  :D
        }
    );

    $("checkBoard").click(function () {
        connection.invoke('SetBoard', sessionId, startGameBoard);
    });

    connection.start()
        .then(() => connection.invoke('JoinGame', sessionId))
        .then(() => connection.invoke('GetMyBoard', sessionId))
        .then(() => connection.invoke('GetOpponentBoard', sessionId))
        .then(() => connection.invoke('IsItMyTurn', sessionId)); //wywoływane w trakcie tworzenia połączenia
}

var rows = 10;
var cols = 10;
var squareSize = 30;

var shootingGameBoardContainer = document.getElementById("shootingGameBoard");
var myGameBoardContainer = document.getElementById("myGameBoard");
var myTurn = false;
var EMPTY_NOT_HIT = 0;
var SHIP_NOT_HIT = 1;
var EMPTY_HIT = 2;
var SHIP_DESTROYED = 3;


 var myGameBoard = [
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
 	[0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
 ];


var shootingGameBoard = [
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
];

var myText = "";
var text = "";
var myShootingText = "";
var shootingText = "";

function refresh() {
    for (i = 0; i < cols; i++) {
        for (j = 0; j < rows; j++) {

            if (i == 0 && j == 0) {
                var myNumeration = document.createElement("div");
                var numeration = document.createElement("div");
                myGameBoardContainer.appendChild(myNumeration);
                myGameBoardContainer.appendChild(numeration);
                for (k = 0; k < 10; k++) {
                    myText += '<p3>' + k + '</p3>';
                    text += '<p4>' + String.fromCharCode(97 + k).toUpperCase() + '</p4></br>';
                }
                document.getElementById("myNumeration").innerHTML = myText;
                document.getElementById("numeration").innerHTML = text;
            }

            var mySquare = document.createElement("div");
            myGameBoardContainer.appendChild(mySquare);

            mySquare.id = 's' + j + i;

            var topPosition = j * squareSize;
            var leftPosition = i * squareSize;

            mySquare.style.top = topPosition + 'px';
            mySquare.style.left = leftPosition + 'px';

            if (myGameBoard[j][i] == EMPTY_HIT) {
                mySquare.style.background = '#fff';
            } else if (myGameBoard[j][i] == SHIP_NOT_HIT) {
                mySquare.style.background = '#0066ff';
            } else if (myGameBoard[j][i] == SHIP_DESTROYED) {
                mySquare.style.background = '#CC0000';
            } else if (myGameBoard[j][i] == EMPTY_NOT_HIT) {
                mySquare.style.background = '#CCCCCC';
            }
        }
    }
    
    for (i = 0; i < cols; i++) {
        for (j = 0; j < rows; j++) {

            if (i == 0 && j == 0) {
                var myShootingNumeration = document.createElement("div")
                var shootingNumeration = document.createElement("div");
                shootingGameBoardContainer.appendChild(myShootingNumeration);
                shootingGameBoardContainer.appendChild(shootingNumeration);
                for (k = 0; k < 10; k++) {
                    myShootingText += '<p3>' + k + '</p3>';
                    shootingText += '<p4>' + String.fromCharCode(97 + k).toUpperCase() + '</p4></br>';
                }
                document.getElementById("myShootingNumeration").innerHTML = myShootingText;
                document.getElementById("shootingNumeration").innerHTML = shootingText;
            }
                        
            var square = document.createElement("div");
            shootingGameBoardContainer.appendChild(square);
            
            square.id = 'e' + j + i;
            
            var topPosition = j * squareSize;
            var leftPosition = i * squareSize;
            
            square.style.top = topPosition + 'px';
            square.style.left = leftPosition + 'px';

            if (shootingGameBoard[j][i] == EMPTY_HIT) {
                square.style.background = '#fff';
            } else if (shootingGameBoard[j][i] == SHIP_NOT_HIT) {
                square.style.background = '#0066ff';
            } else if (shootingGameBoard[j][i] == SHIP_DESTROYED) {
                square.style.background = '#CC0000';
            } else if (shootingGameBoard[j][i] == EMPTY_NOT_HIT) {
                square.style.background = '#CCCCCC';
            }
        }
    }
}

function changeTurn()
{
    connection.invoke('IsItMyTurn', mySessionId);
}

if (shootingGameBoardContainer != null)
    shootingGameBoardContainer.addEventListener("click", fireTorpedo, false);

function fireTorpedo(e) {
    if (e.target !== e.currentTarget && myTurn) {
        var row = e.target.id.substring(1, 2);
        var col = e.target.id.substring(2, 3);
        connection.invoke('Move', mySessionId, row, col);
        e.stopPropagation();
    }
}

function getCurrentBoard() {
    if (myTurn)
        return myGameBoard;
    else
        return shootingGameBoard;
}

function getContainerPrefix() {
    if (myTurn)
        return 'e';
    else
        return 's';
}

function checkFire(value, row, col) {
    getCurrentBoard()[row][col] = value;
    var color;
    if (value === EMPTY_HIT) {
        color = '#fff';
    } else if (value === SHIP_DESTROYED) {
        color = '#CC0000';
    }
    document.getElementById(getContainerPrefix() + row + col).style.background = color;
}
