// Code for signalr chat
// TODO move into separate js file

function continueToChat() {
    // Transform form into chat-mode
    $("#spn-nick").text("Zalogowany jako: " + $("#nick").val());
    $("#entrance").hide();
    $("#chat").show();
}

// Initialize the connection with singalr hub
let transportType = signalR.TransportType.WebSockets;
let http = new signalR.HttpConnection(`http://${document.location.host}/general_chat`, { transport: transportType });
let connection = new signalR.HubConnection(http);
connection.start();
connection.on('Send', (nick, message) => {
    appendLine(nick, message);
});

// Send button listener which calls server-side code on the signalr hub
document.getElementById('frm-send-message').addEventListener('submit', event => {
    let message = $('#message').val();
    let nick = $('#spn-nick').text();

    $('#message').val('');

    connection.invoke('Send', nick, message);
    event.preventDefault();
});

// Update the view
function appendLine(nick, message, color) {
    let nameElement = document.createElement('span');
    nameElement.className = "mdl-chip__contact mdl-color--teal mdl-color-text--white";
    nameElement.innerText = 'G'; //`${nick}:`;

    let msgElement = document.createElement('span');
    msgElement.className = "mdl-chip__text";;
    msgElement.innerText = ` ${message}`;

    let formattedMessage = document.createElement("span");
    formattedMessage.className = "mdl-chip mdl-chip--contact";
    formattedMessage.appendChild(nameElement);
    formattedMessage.appendChild(msgElement);

    let li = document.createElement('li');
    li.appendChild(formattedMessage);

    $('#messages').append(li);
};