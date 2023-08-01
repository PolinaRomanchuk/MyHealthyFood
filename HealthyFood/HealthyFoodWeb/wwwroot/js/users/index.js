$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/userChat")
        .build();

    $('.send-message').click(function () {
        const text = $('.message-text').val();
        hub.invoke("AddNewMessage", text);
    });

    hub.on("SomeOneAddNewMessage", function (message, userName) {
        console.log(userName + ': ' + message);
        const div = $(`<div>${userName}: ${message}</div>`);
        $('.chat-block').append(div);
    });

    hub.on("SayHiToNewUser", function (userName) {
        const div = $(`<div>${userName} Enter to the chat</div>`);
        $('.chat-block').append(div);
    });

    hub.start()
        .then(function () {
            hub.invoke("NewUserOpenChat");
        });

    //hub.client.userLeft = function (data) {
    //    $('#contents').append('<li>Bye<strong>' + '</strong>:' + data + '</li>');
    //};

});