# Meeting Management System API

This project is a **backend API** for managing meetings, users, and decisions.  
It helps teams to plan meetings, add participants, and track decisions in one system.

---

##  Goal

- Organize meetings in an easy way  
- Add and manage users or participants  
- Save meeting notes and decisions  
- Track tasks and decision progress  
- Learn Clean Architecture and good backend structure  

---

##  Tech Stack

- **Language:** C# (.NET 9 / ASP.NET Core Web API)  
- **Database:** Microsoft SQL Server (MSSQL)  
- **ORM:** Entity Framework Core (Code First)  
- **Authentication:** JSON Web Token (JWT)  
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, Persistence, Presentation, WebAPI)  
- **CI/CD:** Azure DevOps  
- **Logging:** Serilog  
- **Validation:** FluentValidation  

---

##  Folder Structure

```
src
 ├── core
 │    ├── application   → use case (CQRS, DTO, validator)
 │    └── domain        → business rules, entity, interface
 ├── external
 │    ├── infrastructure → cross-cutting concerns (email sender, logging, etc.)
 │    ├── persistence    → EF Core implementation (DbContext, Repository)
 │    └── presentation   → non-API presentation layers (e.g., gRPC, SignalR, Blazor)
 └── WebApi              → ASP.NET Core Web API (main entry point)
```

---
##  Main Features

- Register and login users  
- Create and manage meetings  
- Add participants to meetings  
- Add and track decisions  
- Assign people to decisions  
- Send **email notifications** (for invitations, assignments, or decisions)  
- JWT secure access  
- Swagger documentation  

---

##  Setup & Run

1. **Clone the repo**

   ```bash
   git clone https://github.com/KeremHavlc/MeetingManagementSystem.API.git
   ```

2. **Open the project**

   Open the `.sln` file in Visual Studio or VS Code.

3. **Set your database connection**

   In `appsettings.Development.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=MeetingDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. **Run migrations**

   ```bash
   dotnet ef database update
   ```

5. **Start the API**

   ```bash
   dotnet run --project src/WebApi
   ```

6. **Open Swagger**

   ```
   https://localhost:{port}/swagger
   ```

---

##  Azure DevOps (CI/CD)

You can use **Azure DevOps** to build and deploy this project.

### Example Steps

1. Create a new project in **Azure DevOps**  
2. Connect your GitHub repo or push it to Azure Repos  
3. Create a **Pipeline (YAML)** that includes:
   - Restore NuGet packages  
   - Build project  
   - Run tests  
   - Publish build artifacts  
4. Create a **Release Pipeline** to:
   - Deploy to Azure App Service  
   - Add variables for connection strings and secrets  
5. Use **Azure Key Vault** for JWT secrets and passwords  
6. Use branches like:
   - `main` → production  
   - `develop` → testing  
   - `feature/...` → new features  

---

##  Deployment

- Create an **Azure App Service** (example: `mms-api-prod`)  
- Add connection string and JWT secret in **App Service > Configuration**  
- Connect your DevOps release pipeline to deploy automatically  

---

##  Example API Usage

### Register

```http
POST /api/auth/register
{
  "email": "user@email.com",
  "password": "P@ssword1",
  "firstName": "user",
  "lastName": "user"
}
```

### Login

```http
POST /api/auth/login
{
  "email": "user@email.com",
  "password": "P@ssword1"
}
```

---

##  Architecture Rules

- **Domain** layer → only business rules  
- **Application** layer → use cases, DTOs, validators (CQRS, MediatR)  
- **Persistence** → EF Core implementation and repository logic  
- **Infrastructure** → cross-cutting concerns like email, logging  
- **Presentation** → SignalR, Blazor, or gRPC layers  
- **WebApi** → controllers, middlewares, JWT, Swagger  
- Follow **SOLID** and **Clean Code** rules  
- Dependencies flow from top to down: `WebApi → Application → Domain`  

---

##  Development Notes

- Use `dotnet watch run` for fast development  
- Logs are written using **Serilog**  
- Handle errors using **Exception Middleware**  
- Use **FluentValidation** for request validation  
- Use **Mapster** for mapping DTOs and Entities  

---

##  Contribute

1. Fork the project  
2. Create a new branch (`feature/...`)  
3. Commit your changes  
4. Open a Pull Request  

---


##  Frontend (Client)

If you want to see the **frontend (React)** part of this project,  
you can visit the following repository:  

 [Meeting Management System Client](https://github.com/KeremHavlc/MeetingManagementSystem.CLIENT)

---
## Contact

**Kerem Havlucu**  
[GitHub Profile](https://github.com/KeremHavlc)  
**E-Mail:** keremhvlc@gmail.com
