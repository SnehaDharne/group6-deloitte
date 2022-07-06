Steps to follow in Visual Studio to run the project:
1) Go to View > Other Windows > Package Manager Console
2) cd into the project folder, e.g.:
```
cd .\Deloitte_Project
```
3) Install the dependencies:
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.0.0
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.0.0
Install-Package Microsoft.EntityFrameworkCore.Design -Version 3.0.0
```
4) Add a migration:
```
dotnet ef migrations add InitialCreate
```
5) Update the database:

```
dotnet ef database update
```
6) Run the project as Deloitte_Project, **NOT** IIS Express.
