$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/alert")
        .build();

    hub.on("ImportantEvent", function (message) {
        alert(message);
    });

    hub.start();
});