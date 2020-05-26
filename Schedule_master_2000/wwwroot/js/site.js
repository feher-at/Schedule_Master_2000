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
        const scheduleDropDown = document.createElement("div");
        scheduleDropDown.className = "schedule_container";
        const SelectEl = document.createElement("select");
        SelectEl.id = "SelectType";
        SelectEl.addEventListener("change", () => { SelectValue(userSchedulesModel); },false)
        const baseOptionEl = document.createElement("option");
        baseOptionEl.value = "0";
        baseOptionEl.textContent = "Schedules:";
        SelectEl.appendChild(baseOptionEl);
        const userSchedules = userSchedulesModel.schedules
        for (let i = 0; i < userSchedules.length; i++) {
            const schedule = userSchedules[i];
            const scheduleOptionEl = document.createElement("option");
            scheduleOptionEl.value = `${schedule.scheduleID}`;
            scheduleOptionEl.textContent = `${schedule.title}`;
            
            SelectEl.appendChild(scheduleOptionEl);
        }
        scheduleDropDown.appendChild(SelectEl);
        divEl.appendChild(scheduleDropDown);
    }
}

function SelectValue(userScheduleModel) {
    let slotId = 1;    
    let columnNumber = 0;

    var MainDiv = document.getElementById("home");
    MainDiv.style.height = "1300px";

    var type = document.getElementById("SelectType");
    var chosenScheduleID = type.options[type.selectedIndex].value;
    const scheduleTable = document.createElement("table");
    scheduleTable.className = "calendar table table - bordered";
    
    const tableHeadEL = document.createElement("thead");

    const columnTrEl = document.createElement("tr");

    const userColumns = userScheduleModel.columns;
    const columnThEl = document.createElement('th');
    columnThEl.style.width = "10%";
    columnTrEl.appendChild(columnThEl);

    for (let i = 0; i < userColumns.length; i++) {
        const column = userColumns[i];
        if (column.scheduleID == chosenScheduleID) {
            columnNumber++;
            const columnThEl = document.createElement('th');
            columnThEl.style.width = "10%";
            columnThEl.innerHTML = column.title;

            columnTrEl.appendChild(columnThEl);
        }
    }
    tableHeadEL.appendChild(columnTrEl)
    scheduleTable.appendChild(tableHeadEL)

    const tablebodyEL = document.createElement("tbody");


    for (let i = 0; i <= 24; i++) {
        const rowTrEl = document.createElement("tr");
        const hourTdEL = document.createElement("td");
        hourTdEL.innerHTML = `${i}:00`
        rowTrEl.appendChild(hourTdEL);

        for (let x = 0; x <= columnNumber-1; x++) {
            const slotTdEL = document.createElement("td");
            slotTdEL.className = "no-event"
            slotTdEL.id = `${slotId}`
            slotTdEL.innerHTML = `${slotId}`
            slotTdEL.addEventListener('click', () => { onSlotClick(slotTdEL.id); }, false)

            slotTdEL.className = "cell";
            slotTdEL.rowSpan = 1;
            slotId = slotId + 1;
            rowTrEl.appendChild(slotTdEL);
        }
        tablebodyEL.appendChild(rowTrEl)

    }
    scheduleTable.appendChild(tablebodyEL)
    MainDiv.appendChild(scheduleTable)

    const userTasks = userScheduleModel.tasks;

    for (let i = 0; i < userTasks.length; i++) {
        const task = userTasks[i];
        var timeSpan = 0;
        var slot = document.getElementById(task.slotID)
        slot.innerHTML = task.title;
        for (let i = 0; i < task.lenght - 1; i++) {
            timeSpan += columnNumber
            var slot = document.getElementById(task.slotID + timeSpan)
            slot.innerHTML = task.title;
        }

    }
}
function onSlotClick(slotId) {
    alert(`You clicked on ${slotId}`);
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
            addNewColumns(xhr.responseText);
        }
    }
    console.log(schedule);
    xhr.send(JSON.stringify(schedule));
}

function addNewColumns(schedule) {
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
    homeSubmit.addEventListener("click", () => { createColumns(schedule); }, false);


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



function createColumns(schedule) {
    console.log(schedule);
    var currentSchedule = JSON.parse(schedule)
    var columns = document.getElementsByClassName("inputField");
    var columnsList = [];
    if (columns.length > 0) {
        for (let i = 0; i < columns.length; i++) {
            var column = columns[i];
            columnModel = { UserID: parseInt(`${currentSchedule.userID}`), ScheduleID: parseInt(`${currentSchedule.scheduleID}`), Title: `${column.value}` }
            columnsList.push(columnModel)
        }
        var xhr = new XMLHttpRequest();

        xhr.open('POST', '/Home/NewColumn', true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                GetSchedules();
            }
        }
        console.log(JSON.stringify(columnsList));
        xhr.send(JSON.stringify(columnsList));
    }

}



