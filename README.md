todo mapper belongs in application instead of duplicating
# Overview
A starter project I built that explores a meld of vertical slice code layout with CQRS, in the same vain as Jason Taylor's Clean Architecture. Additionally, as suggested by Uncle Bob Martin in his Onion Architecture, it will also explore using a Model-View-Presenter pattern to decouple the front-end models from the business models.

## Getting Started
You will need .NET Core 3.1,  VS 2019 and SQL Server 2017 or greater. This project leverages C# 8 features

### Running against a SQL database
- Create a SQL database called MyMovieLibrary
- In appsettings.json modify the "DefaultConnection" property to a connection string that is able to connect to that database
- In the root of the project there is a file called "migrate.ps1". Run it using powershell. This will generate the database tables and schema in the database you configured in the previous two steps.

### Running against an in-memory database
If you have trouble with the above and are short on time, in appsettings.json you can set the "UseInMemoryDatabase" property to true and the project will work fine just like that.

### Running the project
Set the "Web" project as the startup project in Visual Studio, run it, and away you go! Empty database tables will be populated with sample seed data when the application starts.



