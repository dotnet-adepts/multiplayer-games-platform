// set grid rows and columns and the size of each square
var rows = 10;
var cols = 10;
var squareSize = 30;
var myGameBoardContainer = document.getElementById("myGameBoard");
var possibleShip = [4, 3, 2, 2, 1];
var clickCounter = 0;

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
var placingMode = false;

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


myGameBoardContainer.addEventListener("click", placeShip, false);

function changePlacingMode() {
    placingMode = !placingMode;
    if(!placingMode && clickCounter > 0) {
        possibleShip[clickCounter - 1]--;
        clickCounter = 0;
    }
}

function placeShip(e) {
    if(placingMode) {
        // if item clicked (e.target) is not the parent element on which the event listener was set (e.currentTarget)
        if (e.target !== e.currentTarget) {
            // extract row and column # from the HTML element's id
            var row = e.target.id.substring(1,2);
            var col = e.target.id.substring(2,3);
            //alert("Clicked on row " + row + ", col " + col);

            // if player clicks a square with no ship, change the color and change square's value
            if (clickCounter === possibleShip.length || possibleShip[clickCounter] === 0) {
                alert("Nie mozna postawic statku o takiej wielkosci!");
            }  else if (startGameBoard[row][col] === 0 && clickCounter < possibleShip.length && possibleShip[clickCounter] > 0) {
                e.target.style.background = '#0066ff';
                // set this square's value to 3 to indicate that they fired and missed
                startGameBoard[row][col] = 1;
                clickCounter++;

                // if player clicks a square with a ship, change the color and change square's value
            } else if (startGameBoard[row][col] === 1) {
                alert("Tu juz jest statek!");
            }
        }
        e.stopPropagation();
    }
}

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    ev.target.appendChild(document.getElementById(data));
}