
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.start().then(function () {

    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("OnConnected", function () {
    debugger;
    alert('OnConnected');
    OnConnected();
});

function OnConnected() {
    //var username = $('#hfUsername').val();
    //connection.invoke("SaveUserConnection", username).catch(function(err) {
    //    return console.error(err.toString());
    //});
}

connection.on("ReceivedNotification", function (message) {
    DisplayGeneralNotification(message, 'General Message');
});

connection.on("ReceivedPersonalNotification", function (message, username) {
    DisplayPersonalNotification(message, 'Hey ' + username);
});

//connection.on("ReceivedGroupNotification", function (message, username) {
//    DisplayGroupNotification(message, 'Team ' + username);
//});

function DisplayGeneralNotification(message, title) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 4000
        };
        toastr.info(message, title);

    }, 1300);
}

function DisplayPersonalNotification(message, title) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 4000
        };
        toastr.success(message, title);

    }, 1300);
}