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

    connection.on('waitForOpponent',
        () => {
            //Wyswietlenie uzytkownikowi ze sutawil swoja plansze poprawnie teraz musi czekac na przeciwnika
            document.getElementsByTagName("p")[1].innerHTML = "Oczekiwanie, aż przeciwnik rozstawi swoje statki";
        }
    );

    connection.on('playersReady',
        (redirectUrl) => {
            window.location.replace(redirectUrl);
        }
    );

    connection.on('tooManyShips',
        (tooMany) => {
            if (tooMany) {
                document.getElementsByTagName("p")[1].innerHTML = "Rozstawiono za dużo statków!";
                setTimeout(
                    function () {
                        document.getElementsByTagName("p")[1].innerHTML = "";
                    }, 5000);
            }
            else {
                document.getElementsByTagName("p")[1].innerHTML = "Rozstawiono za mało statków!";
                setTimeout(
                    function () {
                        document.getElementsByTagName("p")[1].innerHTML = "";
                    }, 5000);
            }
        }        
    );

    connection.on('badInput',
        () => {
            document.getElementsByTagName("p")[1].innerHTML = "Nieprawidłowe dane";
            setTimeout(
                function () {
                    document.getElementsByTagName("p")[1].innerHTML = "";
                }, 5000);
        }
    );

    connection.on('errorsInShipPlacement',
        () => {
            document.getElementsByTagName("p")[1].innerHTML = "Twoje statki nie mogą się ze sobą stykać!";
            setTimeout(
                function () {
                    document.getElementsByTagName("p")[1].innerHTML = "";
                }, 5000);
        }
    );

    $("#checkBoard").click(function () {
        connection.invoke('SetBoard', sessionId, startGameBoard);
    });

    $("#updateExampleButton").click(function () {
        connection.invoke('UpdateExample', sessionId); // wywołanie klient -> serwer
    });
}

// set grid rows and columns and the size of each square
var rows = 10;
var cols = 10;
var squareSize = 30;
var myGameBoardContainer = document.getElementById("myGameBoard");
var possibleShip = [4, 3, 2, 1];

/* 0 = empty, 1 = part of a ship, 2 = a sunken part of a ship, 3 = a missed shot
*/

var startGameBoard = [
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

// make the grid columns and rows
for (i = 0; i < cols; i++) {
    for (j = 0; j < rows; j++) {

        if(i == 0 && j == 0){
            var myNumeration = document.createElement("div")
            var numeration = document.createElement("div");
            myGameBoardContainer.appendChild(myNumeration);
            myGameBoardContainer.appendChild(numeration);
            for(k = 0; k < 10; k++){
                myText += '<p3>' + k + '</p3>';
                text += '<p4>' + String.fromCharCode(97 + k).toUpperCase() + '</p4></br>';
            }
            document.getElementById("myNumeration").innerHTML = myText;
            document.getElementById("numeration").innerHTML = text;
        }


        // create a new div HTML element for each grid square and make it the right size
        var mySquare = document.createElement("div");
        myGameBoardContainer.appendChild(mySquare);

        // give each div element a unique id based on its row and column, like "s00"
        mySquare.id = 's' + j + i;

        // set each grid square's coordinates: multiples of the current row or column number
        var topPosition = j * squareSize;
        var leftPosition = i * squareSize;

        // use CSS absolute positioning to place each grid square on the page
        mySquare.style.top = topPosition + 'px';
        mySquare.style.left = leftPosition + 'px';
        
        if (startGameBoard[j][i] === 0) {
            mySquare.style.background = '#bbb';
        } else if (startGameBoard[j][i] === 1) {
            mySquare.style.background = '#0066ff';
        }
    }
}

//document.getElementsByTagName("p")[1].innerHTML = "Stwórz swoje ustawienie statków!/n Ustawienie powinno zawierać:";

myGameBoardContainer.addEventListener("click", placeShip, false);

function placeShip(e) {
    // if item clicked (e.target) is not the parent element on which the event listener was set (e.currentTarget)
    if (e.target !== e.currentTarget) {
        // extract row and column # from the HTML element's id
        var row = e.target.id.substring(1,2);
        var col = e.target.id.substring(2,3);
        //alert("Clicked on row " + row + ", col " + col);

        // if player clicks a square with no ship, change the color and change square's value
        if (startGameBoard[row][col] === 0) {
            e.target.style.background = '#0066ff';
            // set this square's value to 3 to indicate that they fired and missed
            startGameBoard[row][col] = 1;

            // if player clicks a square with a ship, change the color and change square's value
        } else if (startGameBoard[row][col] === 1) {
            e.target.style.background = '#bbb';
            startGameBoard[row][col] = 0;
        }
    }
        e.stopPropagation();    
}



