#!/bin/bash

dotnet ef database --project DAL --startup-project WebApp drop -f
dotnet ef migrations remove --project DAL --startup-project WebApp
dotnet ef migrations --project DAL --startup-project WebApp add InitialMigrate$(date +"%H%M%S")
dotnet ef database --project DAL --startup-project WebApp update
