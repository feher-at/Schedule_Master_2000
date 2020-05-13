let loginDiv;
let regDiv;

function ShowLoginForm() {
    loginDiv.style.display = "block";
    regDiv.style.display = "none";
}

function ShowRegistrationForm() {
    loginDiv.style.display = "none";
    regDiv.style.display = "block";
}

document.addEventListener('DOMContentLoaded', (event) => {
    loginDiv = document.getElementById("loginCard");
    regDiv = document.getElementById("registrationCard");
    loginDiv.style.display = "block";
    regDiv.style.display = "none";
});

function GetSchedules() {

    var xhr = new XMLHttpRequest();

    xhr.open('GET', '/Home/Schedule', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            alert(xhr.responseText);
        }
    }
    xhr.send();
}