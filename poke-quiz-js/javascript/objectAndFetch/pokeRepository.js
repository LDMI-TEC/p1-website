import pokeFactory from "./pokefactory.js";

const baseUrl = "https://pokeapi.co/api/v2/pokemon/";
let fetchCount = 0;

// gets a random pokemon between gen 1 and gen 5
const randomPokemonId = () => {
  let randomNumber = Math.floor(Math.random() * 649 + 1);

  return randomNumber;
};

const fetchRandomPokemon = async () => {
  let pokemon;

  try {
    const response = await fetch(baseUrl + randomPokemonId());
    if (!response.ok) {
      throw new Error(`Response status: ${response.status}`);
    }
    fetchCount++;
    const data = await response.json();
    console.log(`Fetching data...\nPokemon ${fetchCount} Data:\n`);
    console.log(data);
    // If the pokemon doesn't have a secondary type
    const type2 = data.types[1]?.type.name || null;

    pokemon = pokeFactory(
      data.id,
      data.name,
      data.types[0].type.name,
      type2,
      data.sprites.versions["generation-v"]["black-white"].animated
        .front_default,
      data.height,
      data.weight,
      data.stats[0].base_stat,
      data.stats[1].base_stat,
      data.stats[2].base_stat,
      data.stats[3].base_stat,
      data.stats[4].base_stat,
      data.stats[5].base_stat
    );
  } catch (error) {
    console.error(`Error trying to fetch data ${error}`);
  }

  return pokemon;
};

const fetchTwoMons = async () => {
  return [await fetchRandomPokemon(), await fetchRandomPokemon()];
};

const fetchPochama = async () => {
  const pochamaId = 393;
  let pochamaData;

  try {
    const response = await fetch(baseUrl + `${pochamaId}`);
    if (!response.ok) {
      throw new Error("Problem fetching Pochama!");
    }
    const data = await response.json();

    pochamaData = [
      data.sprites.versions["generation-v"]["black-white"].animated
        .front_default,
      data.sprites.front_default,
    ];
  } catch (error) {
    console.error(error);
  }

  return pochamaData;
};

export { fetchTwoMons, fetchPochama };