# TeamManagementAPI
TeamManagementAPI is a web API designed to efficiently manage teams within an organization.

## Running the Project
Before diving into the project, ensure you have globally installed .NET 8.0 on your machine.

### Publish Database

There are two recommended methods to publish the database.

**1. Using Visual Studio (Recommended):**
- Build your project using the Release configuration in Visual Studio.
- Right-click on the SQL project `TeamManagement.Database`.
- Select `Publish...`.
- Choose `Edit...`.
- Navigate to the `Browse` tab and locate your local SQL Server database (either on your machine or Docker).
- Select the local DB and press `OK`.
- Assign a name to the database (e.g., `team_management`).
- Click `Publish`.

Now that your database is ready, proceed to building the project.

**2. Using Queries:**
- Create a database named `team_management` on your SQL Server.
- Run the SQL scripts found in `\TeamManagement.Database\dbo\Tables`.
- Execute the SQL scripts in `\TeamManagement.Database\dbo\StoredProcedures`.
- Run the SQL script `Script.PostDeployment.sql` in `\TeamManagement.Database\dbo\StoredProcedures`.

Now that your database is up and populated, move on to building the project.

### Build Project

As dotnet core doesn't support SQL projects, build the project using the following command where the .sln file is located:

```bash
dotnet build .\TeamManagement.sln --configuration releaseWithoutDatabases
```
Now, run the project:
```bash
dotnet run
```
Open your browser and access the API through [Swagger](https://localhost:5080/swagger/index.html).

_Note: If you prefer, there is a Dockerfile that can be built and ran using docker._

## Testing the Project
Follow these steps to run project tests:
- Navigate to the folder **_{root}\TeamManagement.Tests** in the terminal;
- Run tests with the command:
```bash
dotnet test
```

## Developed With
* [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Feel free to reach out if you have any questions or need further assistance!
