### HW03 - BattleShip
**Deadline: 2021-10-14 23:59:59**

- Add support for gameboard configurations. Support creating and saving of new configurations. Allow loading configurations from disk (serialize and deserialize from json).

- Add support for game state save and load (json).

- Enable nullable reference types systemwide (add Directory.Build.props with correct parameters - copy it from course docs).

- Fix all the warnings in your codebase.

**Notes:**

**Ship** - collection of coordinates with BoardSquareState IsShip.true and IsBomb.false.

**Whose turn is it** - defined in Player.cs  

No need to see your own board (with ships) when playing. See your own board only when generating it.

TODO:

- New game setup
- Continue previous game
- Game statistics for player