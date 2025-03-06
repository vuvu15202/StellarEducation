//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
//"use strict";



//var connection = new signalR.HubConnectionBuilder().withUrl("/notiHub").build();
//document.getElementById("sendButton").disable = true;

//connection.on("ReceivedMess", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messageList").appendChild(li);
//    li.textContent = user + " says " + message;
//});

//connection.start().then(function () {
//    document.getElementById("sendButton").disable = false;
//}
//).catch(function (err) {
//    return console.error(err.toString());
//});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMess", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

//document.getElementById("setName").addEventListener("click", function (event) {
//    var user = document.getElementById("myUserName").value;
//    connection.invoke("SetUserName", user).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});






// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notiHub").build();
//document.getElementById("sendButton").disable = true;

connection.on("ReceivedNoti", function (title, content) {
    ringBell();

    toastr.success(content, title, { timeOut: 3000 });

    document.getElementById("noticount").innerHTML = 1;
    document.getElementById("ulnoti").innerHTML = 
        `
            <li class="border-2 bg-info opacity-10 m-2 p-2" style="border-radius:20px;">
				<div>
					<h5>${title}</h5>
					<div class="ml-2" style="font-size:13px;">
						${content}
					</div>
				</div>
            </li>
        `;
    ringBell();

});

connection.start().then(function () {
    //document.getElementById("sendButton").disable = false;
    var userId = getCookie("userId"); 
    if (userId) {
        connection.invoke("SetUserId", userId).catch(function (err) {
            return console.error(err.toString());
        });
        //ringBell();
    }
}
).catch(function (err) {
    return console.error(err.toString());
});


function getCookie(name) {
    // Split cookies by semicolon
    var cookies = document.cookie.split(';');

    // Loop through each cookie
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        // Trim leading and trailing spaces
        cookie = cookie.trim();
        // Check if this cookie is the one we're looking for
        if (cookie.startsWith(name + '=')) {
            // Return the cookie value
            return cookie.substring(name.length + 1);
        }
    }
    // Return null if cookie not found
    return null;
}

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMess", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

//document.getElementById("setName").addEventListener("click", function (event) {
//    var user = document.getElementById("myUserName").value;
//    connection.invoke("SetUserName", user).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

function ringBell() {
    const bellIcon = document.querySelector('.bell-icon');
    bellIcon.classList.add('ringing');

    // Remove the class after animation ends to reset the state
    bellIcon.addEventListener('animationend', () => {
        bellIcon.classList.remove('ringing');
    });
}