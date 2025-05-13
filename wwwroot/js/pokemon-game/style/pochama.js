import { createPochama } from "../objectAndFetch/pokefactory.js";

// logo and title
const pochama = document.getElementById("pochama");
const logo = document.getElementById("logo");

// set logo
const setLogoAndPochama = async () => {
  const data = await createPochama();

  pochama.src = data[0];
  logo.href = data[1];
};

export { setLogoAndPochama };
