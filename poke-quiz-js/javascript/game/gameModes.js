// the containers the player will click on
const leftPokemon = document.getElementById("left-poke");
const rightPokemon = document.getElementById("right-poke");

// if 0 then left if 1 then right 0 = not yet
let userChoice = -1;

// for checking the anwser immediately
let pokemonSet;
let checkAnswerCallback;

// is the game active used to prevent the user to spam click in order to avoid gameover
let gameActive = true;

// handles userinput (click)
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

const getRandomQuestion = (currentPokemonSet, promter, checkAnwserFunc) => {
  pokemonSet = currentPokemonSet;
  checkAnswerCallback = checkAnwserFunc; // stores the function to evaluate result immediate
  gameActive = true;

  const quizToGet = Math.floor(Math.random() * 5 + 1);

  switch (quizToGet) {
    case 1: {
      promter.innerHTML = "Which Pokemon has the highest attack?!";
      return highestAttack;
    }
    case 2: {
      promter.innerHTML = "Which Pokemon has the lowest attack?!";
      return lowestAttack;
    }
    case 3: {
      promter.innerHTML = "Which Pokemon has the highest defense?!";
      return highestDefense;
    }
    case 4: {
      promter.innerHTML = "Which Pokémon weighs the most?!";
      return highestWeight;
    }
    case 5: {
      promter.innerHTML = "Which Pokémon is the tallest?!";
      return highestHeight;
    }
    default: {
      console.error("Quiz doesn't exists");
      return () => false; // returns a fake/dummy function to avoid errors
    }
  }
};

const highestAttack = () => {
  if (userChoice === -1) {
    return false;
  }
  let correctChoice;

  if (userChoice === 0) {
    correctChoice = pokemonSet[0].stats.attack >= pokemonSet[1].stats.attack;
  } else if (userChoice === 1) {
    correctChoice = pokemonSet[1].stats.attack >= pokemonSet[0].stats.attack;
  }

  userChoice = -1;
  return correctChoice;
};

const lowestAttack = () => {
  if (userChoice === -1) {
    return false;
  }
  let correctChoice;

  if (userChoice === 0) {
    correctChoice = pokemonSet[0].stats.attack <= pokemonSet[1].stats.attack;
  } else if (userChoice === 1) {
    correctChoice = pokemonSet[1].stats.attack <= pokemonSet[0].stats.attack;
  }

  userChoice = -1;
  return correctChoice;
};

const highestDefense = () => {
  if (userChoice === -1) {
    return false;
  }
  let correctChoice;

  if (userChoice === 0) {
    correctChoice = pokemonSet[0].stats.defense >= pokemonSet[1].stats.defense;
  } else if (userChoice === 1) {
    correctChoice = pokemonSet[1].stats.defense >= pokemonSet[0].stats.defense;
  }

  userChoice = -1;
  return correctChoice;
};

const highestWeight = () => {
  if (userChoice === -1) {
    return false;
  }
  let correctChoice;

  if (userChoice === 0) {
    correctChoice = pokemonSet[0].weight >= pokemonSet[1].weight;
  } else if (userChoice === 1) {
    correctChoice = pokemonSet[1].weight >= pokemonSet[0].weight;
  }

  userChoice = -1;
  return correctChoice;
};

const highestHeight = () => {
  if (userChoice === -1) {
    return false;
  }
  let correctChoice;

  if (userChoice === 0) {
    correctChoice = pokemonSet[0].height >= pokemonSet[1].height;
  } else if (userChoice === 1) {
    correctChoice = pokemonSet[1].height >= pokemonSet[0].height;
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