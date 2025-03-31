import { setLogoAndPochama } from "./style/pochama.js";
import { startGameLoop } from "./game.js";
import { addCursor } from "../cursor.js"; 

// gameStart button
const ranButton = document.getElementsByClassName("random-button")[0];

// run at startup Methods
setLogoAndPochama();
addCursor();

// event handlers
ranButton.addEventListener("click", async () => {
  await startGameLoop();
});
