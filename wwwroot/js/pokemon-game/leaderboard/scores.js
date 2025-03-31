import { getScores } from "../objectAndFetch/fetchTokenAndScore.js";

// scorelist ol from leaderboard.html
const scoreList = document.getElementById("score-list");

// prev page button and next page button
const prevPageButton = document.getElementsByClassName("score-page-button")[0];
const nextPageButton = document.getElementsByClassName("score-page-button")[1];

// currentPage of Score
let pageNumber = 0;
const scoresPerPage = 25;

const getAndSortScores = async () => {
  const scores = await getScores();

  if (!Array.isArray(scores)) {
    console.error("Error: expected array");
    return;
  }

  return scores.sort((a, b) => b.score - a.score); // Sort by highest score
};

const formattedDate = (date) => {
  return new Date(date).toLocaleString("en-DK", {
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    // second: "2-digit", uncomment to dispaly seconds aswell
    hour12: false, // 24 hour clock not am and pm
  });
};

const addScores = (playerName, score, date) => {
  let element = document.createElement("li");
  let span = document.createElement("span");

  span.innerHTML = `Player: <strong>${playerName.padEnd(
    25
  )}</strong> ->  Score: <strong>${String(score).padEnd(
    4
  )}</strong> -> Time and date: <strong>${date}</strong>`;
  element.appendChild(span);
  scoreList.appendChild(element);
};

const clearScores = () => {
  while (scoreList.firstChild) {
    scoreList.removeChild(scoreList.firstChild);
  }
};

const scoreNotFound = () => {
  let element = document.createElement("li");
  let span = document.createElement("span");

  span.innerHTML = `No players found`;
  element.appendChild(span);
  scoreList.appendChild(element);
};

const getStartIndex = () => {
  return pageNumber * scoresPerPage;
};

const getEndIndex = (startIndex) => {
  return startIndex + scoresPerPage;
};

const displayButtons = (scoreLength) => {
  if (scoreLength == 25) {
    nextPageButton.style.display = "block";
  } else {
    nextPageButton.style.display = "none";
  }

  if (pageNumber > 0) {
    prevPageButton.style.display = "block";
  } else {
    prevPageButton.style.display = "none";
  }
};

const displayScores = async (playerName = null) => {
  const sortedScores = await getAndSortScores();
  clearScores();

  // filter scores if user is searcing for name
  const filteredScores = playerName
    ? sortedScores.filter(
        (scoreOwner) =>
          scoreOwner.playerName.toLowerCase() === playerName.toLowerCase()
      ) // if not null filter by name
    : sortedScores; // else filteredScores = sortedScores

  // get start and end index
  const start = getStartIndex();
  const end = getEndIndex(start);
  const scoresToDisplay = filteredScores.slice(start, end);

  // hide or show buttons depending on scoresToDisplay
  displayButtons(scoresToDisplay.length);

  // display scores
  if (scoresToDisplay.length === 0) {
    scoreNotFound();
  } else {
    scoresToDisplay.forEach((score) => {
      const date = formattedDate(score.timeOfScore);
      addScores(score.playerName, score.score, date);
    });
  }
};

const nextPage = () => {
  pageNumber++;
  displayScores();
};

const prevPage = () => {
  if (pageNumber > 0) {
    pageNumber--;
    displayScores();
  }
};

// event handlers
prevPageButton.addEventListener("click", () => {
  prevPage();
});

nextPageButton.addEventListener("click", () => {
  nextPage();
});

export { displayScores };
