**Rebuild db**

~~~bash

dotnet ef database --project DAL --startup-project WebApp drop -f
dotnet ef migrations remove --project DAL --startup-project WebApp
dotnet ef migrations --project DAL --startup-project WebApp add InitialMigrate
dotnet ef database --project DAL --startup-project WebApp update

~~~


@(coord.BoardSquareState.IsShip ? "checked" : ""

## TODO

- [x] Setup done boolean (so setup can be done only once or until first move is done)
- [ ] Game over if no ships on the board (after starting play)