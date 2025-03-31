# LearnEasyOnline Platform

## Purpose

The LearnEasyOnline platform is a basic learning management system (LMS) built using Blazor WebAssembly for the frontend and ASP.NET Core Web API for the backend. It provides user authentication, registration, and course viewing functionalities.  It is designed as a starting point for building a more comprehensive online learning platform.

## Dependencies

### NuGet Packages (API Project)

The following NuGet packages are used in the `LearnEasyOnline.Api` project:

* `Microsoft.EntityFrameworkCore.Sqlite`:  Provides the Entity Framework Core provider for SQLite.
* `Microsoft.EntityFrameworkCore.Tools`:  Provides tools for Entity Framework Core, such as migrations.
* `Microsoft.EntityFrameworkCore.Design`:  Provides design-time services for Entity Framework Core.
* `Microsoft.AspNetCore.Identity.EntityFrameworkCore`:  Provides the Entity Framework Core implementation of ASP.NET Identity.
* `System.IdentityModel.Tokens.Jwt`: Provides support for creating and validating JSON Web Tokens (JWT).

## Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or later.
* [Visual Studio](https://visualstudio.microsoft.com/) (with ASP.NET and web development workload) or [VS Code](https://code.visualstudio.com/) with C# extension.

## How to Run the Application

1.  **Clone the repository:**

    ```bash
    git clone <repo-url>
    cd LearnEasyOnline
    ```

2.  **Set up the database:**

    * Open the solution in Visual Studio.
    * Open the Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console).
    * Make sure the `LearnEasyOnline.Api` project is selected as the default project.
    * Run the following commands to create and update the database:

        ```powershell
        Add-Migration InitialCreate
        Update-Database
        ```

    *This will create an `app.db` file in the API project directory.*

3.  **Configure `appsettings.json`:**

    * In the `LearnEasyOnline.Api` project, open `appsettings.json`.
    * Ensure the `ConnectionStrings:DefaultConnection` is set correctly for your SQLite database.  The default is  `"Data Source=app.db"`.
    * **Important:** Change the `JwtSettings:SecretKey` to a strong, random string.  This is crucial for the security of your application.
    * **Note:** For the `JwtSettings:Issuer` and `JwtSettings:Audience` settings, you should replace `[https://yourdomain.com](https://yourdomain.com)` with the actual domain name of your application (Use **"https://localhost"** for both if you intend running the solution locally.).  These settings are used to validate the JWT tokens.

        ```json
        {
            "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
          "ConnectionStrings": {
            "DefaultConnection": "Data Source=app.db"
          },
          "JwtSettings": {
            "SecretKey": "YourSuperSecretKeyHere", // Replace this!
            "Issuer": "[https://yourdomain.com](https://yourdomain.com)",     // Replace this!
            "Audience": "[https://yourdomain.com](https://yourdomain.com)"   // Replace this!
          }
        }
        ```

4.  **Run the application:**

    * In Visual Studio:
        * Set the `LearnEasyOnline.Api` project as the startup project.
        * Press `Ctrl+F5` to run without debugging, or `F5` to run with debugging.
    * In VS Code:
        * Open the terminal and navigate to the `LearnEasyOnline.Api` directory.
        * Run `dotnet run`.

5.  **Running the Client**

    * In Visual Studio
        * Set the `LearnEasyOnline.Client` project as the startup project.
        * Press `Ctrl+F5` to run without debugging, or `F5` to run with debugging.
     * In VS Code
        * Open the terminal and navigate to the `LearnEasyOnline.Client` directory.
        * Run `dotnet run`.

6.  **Access the application:**

    * The Blazor WebAssembly frontend will typically be accessible at `https://localhost:<port>`, where `<port>` is the port number that Visual Studio or `dotnet run` assigns.  The exact URL will be displayed in the browser when the application starts.
    * The API will also be running on a separate port, and the Blazor application is configured to communicate with it.
