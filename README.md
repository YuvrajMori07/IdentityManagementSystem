# IdentityManagementSystem
Identity Management System

# Authentication Solution

This repository provides a modular authentication system built with ASP.NET Core (.NET 8, C# 12), leveraging MediatR, JWT authentication, Entity Framework Core, and clean architecture principles. The solution is organized into multiple projects for maintainability and scalability.

## Technologies Used
- ASP.NET Core 8.0
- C#
- Clean Architecture
- CQRS Pattern
- Identity (Role and User Management)
- SQL Server
- Dapper
- Entity Framework
- AutoMapper
- MediatR
- JWT Authentication and Authorization
## Projects Overview

### 1. **Api**
- **Purpose:** Exposes RESTful endpoints for authentication and user management.
- **Key Features:**
  - ASP.NET Core Web API with controller-based routing.
  - JWT Bearer authentication configured via `Program.cs`.
  - Dependency injection for services and MediatR handlers.
  - CORS policy for secure cross-origin requests.
  - Swagger/OpenAPI integration for API documentation and testing.
- **Packages Used:**  
  - `Swashbuckle.AspNetCore` (Swagger)
  - `Microsoft.AspNetCore.Authentication.JwtBearer`
  - `MediatR`
  - `AutoMapper`
  - `EntityFrameworkCore` (SQL Server)
- **Startup Configuration:**  
  - Registers controllers, authentication, CORS, Swagger, and application/infrastructure services.

### 2. **Application**
- **Purpose:** Contains business logic, MediatR commands/queries, DTOs, and service interfaces.
- **Key Features:**
  - MediatR for CQRS pattern (commands/queries).
  - DTOs for data transfer between layers.
  - `IIdentityService` interface for user and role management abstraction.
  - FluentValidation for input validation.
  - AutoMapper for object mapping.
- **Packages Used:**  
  - `MediatR`
  - `FluentValidation`
  - `AutoMapper`

### 3. **Infrastructure**
- **Purpose:** Implements data access and external service logic.
- **Key Features:**
  - Entity Framework Core for database operations.
  - Dapper for lightweight data access.
  - ASP.NET Core Identity for user and role management.
  - Implements `IIdentityService`.
- **Packages Used:**  
  - `Microsoft.EntityFrameworkCore`
  - `Dapper`
  - `Microsoft.AspNetCore.Identity`

### 4. **Domain**
- **Purpose:** Contains core domain models and interfaces.
- **Key Features:**
  - Domain entities and value objects.
  - MediatR for domain events.

## Key Concepts Implemented

- **JWT Authentication:**  
  Secure API endpoints using JSON Web Tokens, with configuration for issuer, audience, and expiry.

- **CQRS with MediatR:**  
  Separation of command and query responsibilities using MediatR handlers.

- **Dependency Injection:**  
  All services, handlers, and repositories are injected via ASP.NET Core DI.

- **Entity Framework Core & Dapper:**  
  Relational data access using EF Core and Dapper for performance-critical queries.

- **ASP.NET Core Identity:**  
  User and role management, password hashing, and authentication.

- **Swagger/OpenAPI:**  
  Interactive API documentation and testing.

- **CORS Policy:**  
  Restricts API access to trusted origins.

- **Unit & Integration Testing:**  
  xUnit tests for controllers and MediatR handlers, using Moq for mocking dependencies.

## Example Test Case

Unit tests for the `AuthController` ensure correct behavior for login functionality:


## Getting Started

1. **Clone the repository**
2. **Configure connection strings and JWT settings in `appsettings.json`**
3. **Run database migrations (if applicable)**
4. **Start the API project**
5. **Access Swagger UI at `/swagger` for API documentation**

## Folder Structure
/Api  
├── Controllers/                 # API controllers (e.g., AuthController, UserController, RoleController)  
├── Properties/                  # Configuration files (e.g., launchSettings.json)  
├── Program.cs                   # Application startup and configuration  
├── Api.csproj                   # API project file  

/Application  
├── Commands/  
│   ├── Auth/                    # Authentication-related commands  
│   ├── User/                    # User-related commands (Create, Update, Delete)  
│   ├── Role/                    # Role-related commands (Create, Update, Delete)  
├── DTOs/                        # Data Transfer Objects  
├── Queries/  
│   ├── User/                    # User-related queries  
│   ├── Role/                    # Role-related queries  
├── Common/  
│   ├── Interfaces/              # Interfaces (e.g., IIdentityService, ITokenGenerator)  
│   ├── Exceptions/              # Custom exception classes  
├── Application.csproj           # Application layer project file  

/Infrastructure  
├── Data/                        # EF Core DbContext and related data access  
├── Identity/                    # ASP.NET Core Identity implementation (e.g., ApplicationUser)  
├── Services/                    # Infrastructure services (e.g., IdentityService, TokenGenerator)  
├── Migrations/                  # Database migration files  
├── Infra.csproj                 # Infrastructure layer project file  

/Domain  
├── Entities/                    # Domain models and aggregates  
├── Events/                      # Domain events  
├── Domain.csproj                # Domain layer project file  

/Test  
├── Api                         # Unit tests for Api controllers  
├── Application                 # Unit tests for Application
  
**Note:**  
This solution is designed for extensibility and follows best practices for modern .NET development.
