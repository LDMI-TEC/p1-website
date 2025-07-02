const currentDay = new Date(); // Automatically current day if empty
const day = currentDay.getDay();
const dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

const text = "Today it's ";
let extraFun = "";

if (day == 1) {
    document.querySelector(".mondaysContent").style.display = "block";
    document.querySelector(".mondaysContent").style.backgroundColor = "lightpink";
}

if (day == 2) {
    document.querySelector(".tuesdaysContent").style.display = "block";
    document.querySelector(".tuesdaysContent").style.backgroundColor = "lightblue";
}

if (day == 3) {
    extraFun = " my dudes";
    document.querySelector(".wednesdaysContent").style.display = "block";
    document.querySelector(".wednesdaysContent").style.backgroundColor = "lightpink";
}

if (day == 4) {
    document.querySelector(".thursdaysContent").style.display = "block";
    document.querySelector(".thursdaysContent").style.backgroundColor = "lightblue";
}

if (day == 5) {
    document.querySelector(".fridaysContent").style.display = "block";
    document.querySelector(".fridaysContent").style.backgroundColor = "pink";
}

document.querySelector(".day").innerText = text + dayNames[day] + extraFun;
document.querySelector(".day").style.backgroundColor = "lightblue";
