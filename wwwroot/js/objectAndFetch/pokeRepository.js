const baseUrl = "https://pokeapi.co/api/v2/pokemon/";
let fetchCount = 0;

const fetchPokemon = async (id) => {
  try {
    const response = await fetch(baseUrl + id);
    if (!response.ok) {
      throw new Error(`Response status: ${response.status}`);
    }
    fetchCount++;
    const data = await response.json();
    console.log(`Fetching data...\nPokemon ${fetchCount} Data:\n`);
    console.log(data);

    return data;
  } catch (error) {
    console.error(`Error trying to fetch data ${error}`);
    return null;
  }
};

export { fetchPokemon };
