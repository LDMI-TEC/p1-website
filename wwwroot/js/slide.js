let slideIndex = 1;
showSlides(slideIndex);
function plusSlides(n){
    showSlides(slideIndex += n);
}
function currentSlide(n){
    showSlides(slideIndex = n);
}
function showSlides(n){
    let i;
    let slides=document.getElementsByClassName("mySlides");
    let dots=documenet.getElementsByClassName("dot");
    if(n > slides.length){slideIndex = 1}
    if (n < 1){slideIndex = slides.length}
    for(i = 0; i < slides.length; i++){
        slides[i].style.display="none";
    }
    for(i=0;i<dots.length;i++){
        dots[i].className=dots[i].className.replace("active","");
    }
    slides[slideIndex-1].style.display="block";
    dots[slideIndex-1].className +="active";
}



function completeTask(taskId) { 

    document.getElementById(taskId).classList.add("completed"); 

} 
function submitComment() { 

    let comment = document.getElementById("comment").value; 

    if(comment) { 

        alert("Comment submitted: " + comment); 

    } 

} 
function addActivity() { 

    let activity = document.getElementById("activity").value; 

    let time = document.getElementById("time").value; 

    if(activity && time) { 

        let schedule = document.querySelector(".schedule"); 

        let newTask = document.createElement("div"); 

        newTask.classList.add("task"); 

        newTask.innerHTML = `<span>${time} - ${activity}</span> <button onclick="this.parentElement.classList.add('completed')">Done</button>`; 

        schedule.appendChild(newTask); 

    } 

} 