// set grid rows and columns and the size of each square
var rows = 10;
var cols = 10;
var squareSize = 30;

// get the container element
var shootingGameBoardContainer = document.getElementById("shootingGameBoard");
var myGameBoardContainer = document.getElementById("myGameBoard");

/* create the 2d array that will contain the status of each square on the board
   and place ships on the board (later, create function for random placement!)

   0 = empty, 1 = part of a ship, 2 = a sunken part of a ship, 3 = a missed shot
*/

// var myGameBoard = [
// 						[0, 0, 0, 1, 1, 1, 1, 0, 0, 0],
// 						[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
// 						[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
// 						[0, 0, 0, 0, 0, 0, 1, 0, 0, 0],
// 						[0, 0, 0, 0, 0, 0, 1, 0, 0, 0],
// 						[1, 0, 0, 0, 0, 0, 1, 1, 1, 1],
// 						[1, 0, 0, 0, 0, 0, 0, 0, 0, 0],
// 						[1, 0, 0, 1, 0, 0, 0, 0, 0, 0],
// 						[1, 0, 0, 1, 0, 0, 0, 0, 0, 0],
// 						[1, 0, 0, 0, 0, 0, 0, 0, 0, 0]
// ];

var myGameBoard = startGameBoard;
var shootingGameBoard = [
					[0, 0, 0, 1, 1, 1, 1, 0, 0, 0],
					[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
					[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
					[0, 0, 0, 0, 0, 0, 1, 0, 0, 0],
					[0, 0, 0, 0, 0, 0, 1, 0, 0, 0],
					[1, 0, 0, 0, 0, 0, 1, 1, 1, 1],
					[1, 0, 0, 0, 0, 0, 0, 0, 0, 0],
					[1, 0, 0, 1, 0, 0, 0, 0, 0, 0],
					[1, 0, 0, 1, 0, 0, 0, 0, 0, 0],
					[1, 0, 0, 0, 0, 0, 0, 0, 0, 0]
];

var myText = "";
var text = "";
var myShootingText = "";
var shootingText = "";

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



        if (myGameBoard[j][i] == 0) {
            mySquare.style.background = '#bbb';
        } else if (myGameBoard[j][i] == 1) {
            mySquare.style.background = '#0066ff';
        }
	}
}
// myGameBoardContainer.disabled = true;

// make the grid columns and rows
for (i = 0; i < cols; i++) {
    for (j = 0; j < rows; j++) {

        if (i == 0 && j == 0) {
            var myShootingNumeration = document.createElement("div")
            var shootingNumeration = document.createElement("div");
            myGameBoardContainer.appendChild(myShootingNumeration);
            myGameBoardContainer.appendChild(shootingNumeration);
            for (k = 0; k < 10; k++) {
                myShootingText += '<p3>' + k + '</p3>';
                shootingText += '<p4>' + String.fromCharCode(97 + k).toUpperCase() + '</p4></br>';
            }
            document.getElementById("myShootingNumeration").innerHTML = myShootingText;
            document.getElementById("shootingNumeration").innerHTML = shootingText;
        }


        // create a new div HTML element for each grid square and make it the right size
        var square = document.createElement("div");
        shootingGameBoardContainer.appendChild(square);

        // give each div element a unique id based on its row and column, like "s00"
        square.id = 'e' + j + i;

        // set each grid square's coordinates: multiples of the current row or column number
        var topPosition = j * squareSize;
        var leftPosition = i * squareSize;

        // use CSS absolute positioning to place each grid square on the page
        square.style.top = topPosition + 'px';
        square.style.left = leftPosition + 'px';
    }
}

var hitCount = 0;

// set event listener for all elements in gameboard, run fireTorpedo function when square is clicked
shootingGameBoardContainer.addEventListener("click", fireTorpedo, false);

// initial code via http://www.kirupa.com/html5/handling_events_for_many_elements.htm:
function fireTorpedo(e) {
    // if item clicked (e.target) is not the parent element on which the event listener was set (e.currentTarget)
	if (e.target !== e.currentTarget) {
        // extract row and column # from the HTML element's id
		var row = e.target.id.substring(1,2);
		var col = e.target.id.substring(2,3);
        //alert("Clicked on row " + row + ", col " + col);
				
		// if player clicks a square with no ship, change the color and change square's value
		if (shootingGameBoard[row][col] == 0) {
		    e.target.style.background = '#bbb';
			// set this square's value to 3 to indicate that they fired and missed
		    shootingGameBoard[row][col] = 3;
			myGameBoard[row][col] = 3;
			
		// if player clicks a square with a ship, change the color and change square's value
		} else if (shootingGameBoard[row][col] == 1) {
			e.target.style.background = '#0066ff';
			// set this square's value to 2 to indicate the ship has been hit
			shootingGameBoard[row][col] = 2;
            myGameBoard[row][col] = 2;
			
			// increment hitCount each time a ship is hit
			hitCount++;
			// this definitely shouldn't be hard-coded, but here it is anyway. lazy, simple solution:
			if (hitCount == 17) {
			    alert("Wszystkie statki przeciwnika zostały zatopione! Wygrana!");
			    shootingGameBoardContainer.disabled = true;
			}
			
		// if player clicks a square that's been previously hit, let them know
		} else if (shootingGameBoard[row][col] > 1) {
			alert("To pole już jest odkryte");
		}		
    }
    e.stopPropagation();
}
