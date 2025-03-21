import { setLogoAndPochama } from "./style/pochama.js";
import { startGameLoop } from "./game/game.js";

// gameStart button
const ranButton = document.getElementsByClassName("random-button")[0];

// run at startup Methods
setLogoAndPochama();

// event handlers
ranButton.addEventListener("click", async () => {
  await startGameLoop();
});
