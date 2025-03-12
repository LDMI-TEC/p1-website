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

    _stats: {
      _hp: hp,
      _attack: attack,
      _defense: defense,
      _specialAttack: specialAttack,
      _specialDefense: specialDefense,
      _speed: speed,

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
    },

    get stats() {
      return this._stats;
    },
  };
};

export default pokeFactory;