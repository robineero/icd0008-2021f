# icd0008-2021f

Programmeerimine C# keeles (ICD0008, 6 EAP), Robert Laursoo 193798IADB

Mirror: `git push --mirror git@github.com:robineero/icd0008-2021f.git`

- [Distributed repo](https://gitlab.cs.ttu.ee/rolaur/icd0009-2020s)

Windowsis migrate ei töötanud kohe, sest puudu olid need:  
https://docs.microsoft.com/en-us/ef/core/cli/dotnet

`dotnet tool install --global dotnet-ef`  
`dotnet tool update --global dotnet-ef`

Veits hiljem:

Installime sellise asja nagu UI scaffolding tool:
- `dotnet tool install --global dotnet-aspnet-codegenerator`
- igaks juhuks update ka `dotnet tool update --global dotnet-aspnet-codegenerator`

#### Migrate, remove, update, drop

**Migrate:** `dotnet ef migrations --project DAL --startup-project ConsoleApp add InitialMigrate`
- reads from AppDbContext how the db should look like (same thing as InitialCreate).

**Update:** `dotnet ef database --project DAL --startup-project WebApp update`    
**Remove:** `dotnet ef migrations remove --project DAL --startup-project WebApp`  
**Drop**: `dotnet ef database --project DAL --startup-project WebApp drop -f`

#### Database connection strings

Barrel: `"Server=barrel.itcollege.ee,1533;User Id=student;Password=Student.Bad.password.0;Database=rolaur_dbname;MultipleActiveResultSets=true"`

Minu oma: `"Server=vps.that.ee,1433;User Id=sa;Password=pass;Database=dbname;MultipleActiveResultSets=true"`

#### Scaffold

Scaffolding Razor pages (in /WebApp dir)

`dotnet aspnet-codegenerator razorpage -m Game -dc AppDbContext -udl -outDir Pages/Games --referenceScriptLibraries`

To create html controllers in the WebApp folder run:

`dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f`

To create API controllers in the WebApp folder run:

`dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f`