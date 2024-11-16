# BookExchange 

The BookExchange project is designed following the Clean Architecture principles, ensuring a clear separation of concerns, maintainability, and scalability. This project is built using .NET 8 and C# 12.0, and it includes a server-side application and a client-side application.


## Project Architecture
The project is divided into several layers:
1.	Domain Layer: Contains the core business logic and entities.
2.	Application Layer (Use Cases): Contains the application-specific business rules.
3.	Infrastructure Layer: Contains the implementation details for external services, data access, and other infrastructure concerns.
4.	Presentation Layer: Contains the user interface logic.

## Folder Structure
```bash
BookExchange
├── Application
│   ├── Domain
│   │   ├── Domain.csproj
│   │   ├── Entities
│   │   │   └── User.cs
│   │   └── Interfaces
│   │       └── IUserService.cs
│   └── UseCases
│       ├── UseCases.csproj
│       └── User
│           ├── RegisterUser.cs
│           ├── CreatePasswordResetToken.cs
│           ├── ResetPassword.cs
│           └── IUserService.cs
├── Infrastructure
│   ├── DataAccess
│   │   ├── DataAccess.csproj
│   │   ├── Repositories
│   │   │   └── UserRepository.cs
│   │   └── DatabaseContext.cs
│   └── Authorization
│       ├── Authorization.csproj
│       └── Services
│           └── AuthorizationService.cs
├── Presentation
│   ├── BookExchange.Server
│   │   ├── BookExchange.Server.csproj
│   │   ├── Controllers
│   │   │   └── UserController.cs
│   │   ├── Models
│   │   │   ├── CreateUserRequest.cs
│   │   │   ├── UpdateUserRequest.cs
│   │   │   ├── PasswordResetRequest.cs
│   │   │   └── ResetPasswordRequest.cs
│   │   └── Program.cs
│   └── BookExchange.Client
│       ├── BookExchange.Client.csproj
│       ├── CHANGELOG.md
│       ├── src
│       │   ├── app
│       │   │   ├── app.component.ts
│       │   │   ├── app.module.ts
│       │   │   └── ...
│       │   ├── assets
│       │   │   └── ...
│       │   ├── environments
│       │   │   ├── environment.prod.ts
│       │   │   └── environment.ts
│       │   └── ...
│       ├── angular.json
│       ├── karma.conf.js
│       ├── package.json
│       └── tsconfig.json
└── BookExchange.sln
```

## How to Run
### Prerequisites
•	.NET 8 SDK
•	Node.js and npm (for the client-side application)
•	Visual Studio or any other IDE that supports .NET development

### Running the Server-Side Application
1.	Navigate to the server project directory:
```bash
   cd Presentation/BookExchange.Server
```
2.	Restore the dependencies:
```bash
   dotnet restore
```
3.	Build the project:
```bash
   dotnet build
```
4.	Run the project:
```bash
   dotnet run
```
### Running the Client-Side Application
1.	Navigate to the client project directory:
```bash
   cd Presentation/BookExchange.Client
```
2.	Install the dependencies:
```bash
    npm install
```
3.	Run the Angular application:
```bash
   ng serve
```
4.	Open the application in your browser: Navigate to http://127.0.0.1:4200

## Important Note
#### CORS Configuration: The server is configured to allow requests from http://127.0.0.1:4200 (the Angular app URL). This can be modified in the Program.cs file.
#### Swagger: Swagger is enabled in development mode for API documentation. You can access it at http://localhost:<port>/swagger.
#### Middleware: Custom middleware for authorization is used in the server application.