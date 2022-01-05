# celestial-objects-catalog
dotnet ef migrations add InitialMigration --project Nasa.Infrastructure -o Persistence/Migrations --startup-project Nasa.API
dotnet ef database update --project Nasa.Infrastructure --startup-project Nasa.API