
## To create the initial migration, run the following command in the terminal:
dotnet ef migrations add UpdateModelConfigEntity --project ./AutoML.Data --startup-project ./AutoML.Api

## To apply the migration and update the database, run the following command:
dotnet ef database update --project ./AutoML.Data --startup-project ./AutoML.Api
