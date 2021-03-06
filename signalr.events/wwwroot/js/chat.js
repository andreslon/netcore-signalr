"use strict";
//zone.js which seems to corrupt the WebSocket APIs, this is the solution
Object.defineProperty(WebSocket, 'OPEN', { value: 1, });
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ReceiveMessage", function(message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
connection.start().catch(function(err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function(event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function(err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});