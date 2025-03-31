import {
  submitScore,
} from "./objectAndFetch/fetchTokenAndScore.js";

const submit = async (playerName, score, token) => {
  await submitScore(playerName, score, token);
};

export { submit };
