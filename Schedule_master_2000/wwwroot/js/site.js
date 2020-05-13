// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".jumbotron").css({ height: $(window).height() + "px" });

$(window).on("resize", function () {
    $(".jumbotron").css({ height: $(window).height() + "px" });
});

function GetSchedules() {

    var xhr = new XMLHttpRequest();

    xhr.open('GET', '/Home/Schedule', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            onSchedulesReceived(xhr.responseText);
        }
    }
    xhr.send();
}

function onSchedulesReceived(response) {
    const userSchedules = JSON.parse(response);
    const divEl = document.getElementById('home');
    while (divEl.firstChild) {
        divEl.removeChild(divEl.firstChild);
    }

    if (userSchedules.schedules.length == 0) {
        const homeDiv = document.createElement("div");
        homeDiv.className = "container";
        const homeH1 = document.createElement("h1");
        homeH1.textContent = "You have no schedules";
        const homeButton = document.createElement("button");
        homeButton.textContent = "Create Schedule";
        homeButton.className = "btn btn-success my-2 my-sm-0";
        homeButton.addEventListener("click", () => { addNewTask(); }, false);

        homeDiv.appendChild(homeH1);
        homeDiv.appendChild(homeButton);
        divEl.appendChild(homeDiv);
    }
}

function addNewTask() {
    alert("Cant create new tasks yet! ")
}

