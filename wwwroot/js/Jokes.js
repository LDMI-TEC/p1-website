const userId = {
    name : null,
    identity : null,
    Image: null,
    message: null,
    date: null
}

const userComment = document.querySelector(".usercomment");
const publishBtn = document.querySelector("#publish");
const comments = document.querySelector(".Comments")
const userName = document.querySelector(".user");

userComment.addEventListener("input", s => {
    if (!userComment.value) {
        publishBtn.setAttribute("disabled", "disabled");
        publishBtn.classList.remove("abled")
    }else{
        publishBtn.removeAttribute("disabled");
        publishBtn.classList.add("abled")
    }
})

function addpost(){
    console.log("the button works!")
    if (!userComment.value) return;
    userId.name = userName.value;
    if(userId.name === "Anonymous"){
        userId.identity = false;
        userId.Image = "anonymous.png";
    }else{
        userId.identity = true;
        userId.Image = "user.png"
    }
    userId.message = userComment.value;
    userId.date = new Date().toLocaleString();
    let published = 
    `<div class = "parents">
    <img src="Resources/pix/users-.png">
       <div>
       <h1>${userId.name}</h1>
       <textarea>${userId.message}</textarea>
       <div class="engagements"> <img src ="Resources/pix/like.png"><img src ="Resources/pix/share.png">  </div>
       <span class = "date"> ${userId.date}</span>
       </div>
    </div>`;

    comments.innerHTML += published;
    userComment.value = "";
    let commentsNum = document.querySelectorAll(".parents").length;
    document.getElementById("comment").textContent = commentsNum;
}
publishBtn.addEventListener("click",addpost)

