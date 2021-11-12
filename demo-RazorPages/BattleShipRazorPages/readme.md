**Rebuild db**

~~~bash

dotnet ef database --project DAL --startup-project WebApp drop -f
dotnet ef migrations remove --project DAL --startup-project WebApp
dotnet ef migrations --project DAL --startup-project WebApp add InitialMigrate
dotnet ef database --project DAL --startup-project WebApp update

~~~

ASPNETCORE_ENVIRONMENT=DevelopmentSchool dotnet ef database --project DAL --startup-project WebApp drop -f

@(coord.BoardSquareState.IsShip ? "checked" : "")

## TODO

- [x] Setup done boolean (so setup can be done only once or until first move is done)
- [ ] Game over if no ships on left the board (after starting play and having ships initially)
- [ ] Count ships during setup and return if not enough
- [ ] Place some ships during board generation

## Extra

- [x] Setup postgres and make this app work with postgres
- [ ] Random board generation