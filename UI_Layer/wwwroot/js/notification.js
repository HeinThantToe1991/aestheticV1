
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();
connection.onclose(function (error) {
    if (error) {
        // Handle error scenarios
        console.error("Connection closed due to an error:", error);

        if (error.message === "Failed to complete negotiation with the server: Error: CONNECTION_ID_CHANGED") {
            // Handle specific error scenario if needed
        } else {
            // Display an alert for general connection error
/*            alert("Connection was disconnected. Please check your network or try again later.");*/
            //window.location.href = "/BackendSystem/error/DisconnectedError";
            $('#disconnectedModel').modal("show");
        }
    } else {
        // Display an alert for a clean disconnection (e.g., when stopping the IIS server)
      /*  window.location.href = "/BackendSystem/error/DisconnectedError";*/
        //$('#disconnectedModel').modal("show");
    }
});
connection.start().then(function () {

    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("OnConnected", function () {
  
    OnConnected();
});

function OnConnected() {
    connection.invoke("SaveUserConnection").catch(function(err) {
        return console.error(err.toString());
    });
}

connection.on("ReceivedNotification", function (message) {
    DisplayGeneralNotification(message, 'General Message');
});
connection.on("UpdateActiveUsers", function (activeUsers) {

    var activeUsersContainer = document.getElementById("activeUserDashboard");
    activeUsersContainer.innerHTML = "";
    var users = JSON.parse(activeUsers);
    users.forEach(function (user) {
        var userHtml = `         
            <div class="progress-group">
                 ${user.UserType}
                <span class="float-right"><b>${user.UserCount}</b></span>
                <div class="progress progress-sm">
                    <div class="progress-bar" style="width: ${(user.UserCount / 100 * 100)}%"></div>
                </div>
            </div>
        `;
        // Append the userHtml to the container
        activeUsersContainer.innerHTML += userHtml;
    });
});
// Example using JavaScript client (replace this with your actual client implementation)
//window.addEventListener("beforeunload", function (event) {
//    connection.invoke("Disconnect").catch(error => console.error(error));
//});
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