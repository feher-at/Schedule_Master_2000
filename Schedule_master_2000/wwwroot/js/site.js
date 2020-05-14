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
    const userSchedulesModel = JSON.parse(response);
    const userID = userSchedulesModel.user.id;
    const divEl = document.getElementById('home');
    while (divEl.firstChild) {
        divEl.removeChild(divEl.firstChild);
    }

    if (userSchedulesModel.schedules.length == 0) {
        const homeDiv = document.createElement("div");
        homeDiv.className = "container";
        const homeH1 = document.createElement("h1");
        homeH1.textContent = "You have no schedules";
        const homeButton = document.createElement("button");
        homeButton.textContent = "Create Schedule";
        homeButton.className = "btn btn-success my-2 my-sm-0";
        homeButton.addEventListener("click", () => { createSchedule(userID); }, false);

        homeDiv.appendChild(homeH1);
        homeDiv.appendChild(homeButton);
        divEl.appendChild(homeDiv);
    }
    else {
        const homeDiv = document.createElement("div");
        homeDiv.className = "container";
        const userSchedules = userSchedulesModel.schedules
        for (let i = 0; i < userSchedules.length; i++) {
            const schedule = userSchedules[i];
            const scheduleH1 = document.createElement("h1");
            scheduleH1.textContent = `${schedule.title}`;
            homeDiv.appendChild(scheduleH1);
        }
        divEl.appendChild(homeDiv);
    }
}
function createSchedule(userID) {
    const divEl = document.getElementById('home');
    while (divEl.firstChild) {
        divEl.removeChild(divEl.firstChild);
    }
    const homeDiv = document.createElement("div");
    homeDiv.className = "container1";
    const homeH1 = document.createElement("h1");
    homeH1.textContent = "Enter the title of your schedule here";
    const homeButton = document.createElement("button");
    homeButton.textContent = "Create Schedule";
    homeButton.addEventListener("click", () => { sendScheduleData(userID); }, false)
    const homeInput = document.createElement("input");
    homeInput.type = "text";
    homeInput.id = "scheduleTitleInput";

    homeDiv.appendChild(homeH1);
    homeDiv.appendChild(homeInput);
    homeDiv.appendChild(homeButton);

    divEl.appendChild(homeDiv)
}

function sendScheduleData(userID) {
    var scheduleTitle = document.getElementById("scheduleTitleInput");

    var schedule = { Title: `${scheduleTitle.value}`, UserID: parseInt(`${userID}`) };

    var xhr = new XMLHttpRequest();

    xhr.open('POST', '/Home/NewSchedule', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            alert(xhr.responseText);
        }
    }
    console.log(schedule);
    xhr.send(JSON.stringify(schedule));
}

function addNewColumns(userID) {
    const divEl = document.getElementById('home');
    while (divEl.firstChild) {
        divEl.removeChild(divEl.firstChild);
    }
    const homeDiv = document.createElement("div");
    homeDiv.className = "container1";
    const homeButton = document.createElement("button");
    homeButton.className = "add_form_field";
    homeButton.textContent = "Add new column";
    const homeSubmit = document.createElement("button");
    homeSubmit.className = "btn btn-success my-2 my-sm-0";
    homeSubmit.textContent = "Create Schedule";
    homeSubmit.addEventListener("click", () => { createSchedule(userID); }, false);


    homeDiv.appendChild(homeButton);
    homeDiv.appendChild(homeSubmit);

    divEl.appendChild(homeDiv);

    var max_fields = 8;
    var wrapper = $(".container1");
    var add_button = $(".add_form_field");

    var x = 1;
    $(add_button).click(function (e) {
        e.preventDefault();
        if (x < max_fields) {
            x++;
            $(wrapper).append('<div><input type="text" name="mytext[]"class="inputField"/><a href="#" class="delete">Delete</a></div>'); //add input box
        } else {
            alert('You Reached the limits')
        }
    });

    $(wrapper).on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
        x--;
    })


}



function createColumns() {
    var columns = document.getElementsByClassName("inputField");
    var columnsList = [];
    if (columns.length > 0) {
        for (let i = 0; i < columns.length; i++) {
            var column = columns[i];
            columnsList.push(column.value)
        }
        var xhr = new XMLHttpRequest();

        xhr.open('POST', '/Home/NewSchedule', true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                alert(xhr.responseText);
            }
        }
        console.log(JSON.stringify(columnsList));
        xhr.send(JSON.stringify(columnsList));
    }

}



