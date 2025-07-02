import { setLogoAndPochama } from "./style/pochama.js";
import { displayScores } from "./leaderboard/scores.js";
import { addCursor } from "../cursor.js"; 

// search for your score form
const searchForm = document.getElementById("search-score");
// ol that contains the scores
const scoreList = document.getElementById("score-list");

// run at startup functions
setLogoAndPochama();
addCursor();
displayScores();

// event handlers
searchForm.addEventListener("submit", (event) => {
  event.preventDefault(); // prevents form from reloading the page

  // input value (name) from html form
  const playerName = document.getElementById("search-name").value;

  if (playerName === "") {
    displayScores();
    scoreList.style.listStyle = "decimal";
  } else {
    displayScores(playerName);
    scoreList.style.listStyle = "none";
  }
});
