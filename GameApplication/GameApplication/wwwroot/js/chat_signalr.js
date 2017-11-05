// Code for signalr chat
// TODO move into separate js file

function continueToChat() {
    // Transform form into chat-mode
    $("#spn-nick").text($("#nick").val());
    $("#entrance").hide();
    $("#chat").show();
}

// Initialize the connection with singalr hub
let transportType = signalR.TransportType.WebSockets;
let http = new signalR.HttpConnection(`http://${document.location.host}/chat`, { transport: transportType });
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
    let nameElement = document.createElement('strong');
    nameElement.innerText = `${nick}:`;

    let msgElement = document.createElement('em');
    msgElement.innerText = ` ${message}`;

    let li = document.createElement('li');
    li.appendChild(nameElement);
    li.appendChild(msgElement);

    $('#messages').append(li);
};