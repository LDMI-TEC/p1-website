const currentDay = new Date(); //Automatically current day
const day = currentDay.getDay();
const dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];//Day 0-6

const text = "Today it's ";

if (day == 3) {
    extraFun = " my dudes"

    document.querySelector(".day").innerText = text + dayNames[day] + extraFun;
}

document.querySelector(".day").innerText = text + dayNames[day];