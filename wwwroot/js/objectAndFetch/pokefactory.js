import { fetchPokemon } from "./pokeRepository.js";

const pokeFactory = (
  id,
  name,
  pokeType1,
  pokeType2,
  sprite,
  height,
  weight,
  hp,
  attack,
  defense,
  specialAttack,
  specialDefense,
  speed
) => {
  return {
    _id: id,
    _name: name,
    _pokeType1: pokeType1,
    _pokeType2: pokeType2,
    _sprite: sprite,
    _height: height,
    _weight: weight,
    _hp: hp,
    _attack: attack,
    _defense: defense,
    _specialAttack: specialAttack,
    _specialDefense: specialDefense,
    _speed: speed,

    get id() {
      return this._id;
    },

    get name() {
      return this._name;
    },

    get pokeType1() {
      return this._pokeType1;
    },

    get pokeType2() {
      if (this._pokeType2 !== null || this._pokeType2 !== undefined)
        return this._pokeType2;
    },

    get sprite() {
      return this._sprite;
    },

    get height() {
      return this._height;
    },

    get weight() {
      return this._weight;
    },

    get hp() {
      return this._hp;
    },

    get attack() {
      return this._attack;
    },

    get defense() {
      return this._defense;
    },

    get specialAttack() {
      return this._specialAttack;
    },

    get specialDefense() {
      return this._specialDefense;
    },

    get speed() {
      return this._speed;
    },
  };
};

// gets a random pokemon between gen 1 and gen 5
const randomPokemonId = () => {
  let randomNumber = Math.floor(Math.random() * 649 + 1);

  return randomNumber;
};

const createPokemon = async () => {
  const data = await fetchPokemon(randomPokemonId());
  let pokemon = null;
  // If the pokemon doesn't have a secondary type
  const type2 = data.types[1]?.type.name || null;

  pokemon = pokeFactory(
    data.id,
    data.name,
    data.types[0].type.name,
    type2,
    data.sprites.versions["generation-v"]["black-white"].animated.front_default,
    data.height,
    data.weight,
    data.stats[0].base_stat,
    data.stats[1].base_stat,
    data.stats[2].base_stat,
    data.stats[3].base_stat,
    data.stats[4].base_stat,
    data.stats[5].base_stat
  );

  return pokemon;
};

const createPokemonSet = async () => {
  return [await createPokemon(), await createPokemon()];
};

const createPochama = async () => {
  const pochamaId = 393;
  let pochamaData;

  const data = await fetchPokemon(pochamaId);

  pochamaData = [
    data.sprites.versions["generation-v"]["black-white"].animated.front_default,
    data.sprites.front_default,
  ];

  return pochamaData;
};

export { createPokemonSet, createPochama };
