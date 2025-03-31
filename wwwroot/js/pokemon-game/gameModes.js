import { questions } from "./questions.js";

// the containers the player will click on
const leftPokemon = document.getElementById("left-poke");
const rightPokemon = document.getElementById("right-poke");

// if 0 then left if 1 then right 0 = not yet
let userChoice = -1;

// for checking the answer immediately
let pokemonSet;
let checkAnswerCallback;

// is the game active used to prevent the user to spam click in order to avoid game over
let gameActive = true;

// handles user input (click)
const userInput = (element) => {
  // if the game is not active return. this prevents many clicks from the user
  if (!gameActive) {
    return;
  }

  if (element.id === "left-poke") {
    userChoice = 0;
  } else if (element.id === "right-poke") {
    userChoice = 1;
  }

  gameActive = false;

  // used to check immediately
  if (checkAnswerCallback) {
    checkAnswerCallback();
  }
};

const getRandomQuestion = (currentPokemonSet, promter, checkAnswerFunc) => {
  pokemonSet = currentPokemonSet;
  checkAnswerCallback = checkAnswerFunc; // stores the function to evaluate result immediate
  gameActive = true;

  const quizToGet = Math.floor(Math.random() * Object.keys(questions).length);
  const question = questions[quizToGet];

  if (!question || question === null) {
    console.error("question doesn't exist");
    return () => false; // return empty function for the game.js's checkAnswer function
  }

  promter.innerHTML = question.text;
  return () => compareStat(question.stat, question.highOrLow);
};

const compareStat = (statName, highOrLow) => {
  if (userChoice === -1) {
    return false;
  }

  let correctChoice;

  if (highOrLow === "highest") {
    correctChoice =
      pokemonSet[userChoice][statName] >= pokemonSet[1 - userChoice][statName];
  } else if (highOrLow === "lowest") {
    correctChoice =
      pokemonSet[userChoice][statName] <= pokemonSet[1 - userChoice][statName];
  }

  userChoice = -1;
  return correctChoice;
};

// event handlers
leftPokemon.addEventListener("click", () => {
  userInput(leftPokemon);
});

rightPokemon.addEventListener("click", () => {
  userInput(rightPokemon);
});

export { getRandomQuestion };
