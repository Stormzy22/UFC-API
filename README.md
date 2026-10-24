# 🥊 UFC API

A RESTful API built with **C# and ASP.NET Core (.NET 8)** to manage UFC fighters, weight divisions, and nations. This project explores two approaches to building APIs in .NET: **controller-based APIs** and **Minimal APIs**.

Both implementations use the same models and service interfaces, with support for either in-memory data or a SQL Server database.

## 🛠️ Technologies

- **C# / .NET 8** — Application logic and backend development
- **ASP.NET Core Web API** — HTTP endpoints and request handling
- **Entity Framework Core 8** — Database access and migrations
- **Microsoft SQL Server** — Relational data storage
- **Swagger / OpenAPI** — Interactive API documentation and testing
- **Dependency Injection** — Connecting endpoints to service implementations
- **Visual Studio** — Development and debugging

## ✨ Features

- **Fighter management:** Create, retrieve, update, and delete UFC fighters.
- **Division management:** Manage weight divisions and their gender categories.
- **Nation management:** Manage countries and their ISO codes.
- **RESTful endpoints:** Support for GET, POST, PUT, and DELETE requests.
- **Two API styles:** A controller-based API and a Minimal API in the same solution.
- **Flexible data storage:** Switch between in-memory services and SQL Server-backed services.
- **Input validation:** Data annotations for required fields, string lengths, and numeric ranges.
- **Swagger UI:** Explore and test endpoints directly from the browser in development mode.

## 🗂️ Project Structure

```text
ApiUFC.sln
├── ApiUFC/                  # Controller-based API
│   ├── Controllers/         # Fighters, Divisions, and Nations endpoints
│   ├── Data/                # Entity Framework DbContext
│   ├── Interfaces/          # Service contracts
│   ├── Models/              # Fighter, Division, and Nation models
│   ├── Services/            # In-memory and database implementations
│   ├── Migrations/          # Entity Framework database migration
│   └── Program.cs           # Service registration and configuration
└── ApiUFC.Minimal/          # Minimal API implementation
    └── Program.cs           # Endpoint groups and configuration
```

### How It Works

1. A client sends an HTTP request to an API endpoint.
2. The endpoint calls the appropriate service through an interface.
3. The service reads or modifies data, either in memory or through Entity Framework Core.
4. The API returns an HTTP response with the result.

The service interfaces make it possible to change the data source without rewriting the endpoints.

## 🌐 API Endpoints

All three resources — `fighters`, `divisions`, and `nations` — support these operations:

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/api/{resource}` | Get all records |
| `GET` | `/api/{resource}/{id}` | Get one record by ID |
| `POST` | `/api/{resource}` | Create a record |
| `PUT` | `/api/{resource}/{id}` | Update a record |
| `DELETE` | `/api/{resource}/{id}` | Delete a record |

Replace `{resource}` with `fighters`, `divisions`, or `nations`.

### Example: Create a Fighter

**Request:** `POST /api/fighters`

```json
{
  "fullName": "Example Fighter",
  "nickname": "The Challenger",
  "divisionId": 1,
  "nationId": 1,
  "wins": 12,
  "losses": 2,
  "draws": 0
}
```

A successful creation returns **HTTP 201 Created**.

## 🗄️ Database

The SQL Server version uses **Entity Framework Core** with three tables:

- `Fighters` — Names, nicknames, division and nation IDs, and fight records
- `Divisions` — Weight division names and gender categories
- `Nations` — Country names and ISO codes

An initial EF Core migration is included in the project.

**Current configuration:**

| Project | Default data source |
| --- | --- |
| `ApiUFC` (controllers) | In-memory services |
| `ApiUFC.Minimal` | SQL Server services |

You can change the `useDatabase` boolean in each project's `Program.cs` to choose between the two implementations. **In-memory changes are not persisted when the application restarts.**

## 🚀 Running the Project

### Requirements

- .NET 8 SDK
- Visual Studio with the **ASP.NET and web development** workload, or another .NET-compatible editor
- SQL Server Express if using database-backed services
- SQL Server Management Studio (SSMS) is optional, but helpful for viewing the database

### 1. Clone and open

Clone this repository using the green **Code** button on GitHub, then open `ApiUFC.sln` in Visual Studio.

Alternatively, from the solution folder, restore dependencies:

```bash
dotnet restore ApiUFC.sln
```

### 2. Configure SQL Server (database mode only)

The included local development configuration uses a SQL Server Express instance named `SQLEXPRESS` and a database named `UfcApiDb`.

The connection string in the relevant project's `appsettings` configuration is:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=UfcApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Update it if your SQL Server instance uses a different name. The certificate setting above is intended for **local development**, not a production deployment.

To apply the included migration, install the EF Core command-line tool if needed:

```bash
dotnet tool install --global dotnet-ef --version 8.0.22
```

Then, from the solution folder, run:

```bash
dotnet ef database update --project ApiUFC/ApiUFC.csproj --startup-project ApiUFC/ApiUFC.csproj
```

### 3. Start either API

**Controller-based API:**

```bash
dotnet run --project ApiUFC/ApiUFC.csproj --launch-profile https
```

Swagger UI: `https://localhost:7067/swagger`

**Minimal API:**

```bash
dotnet run --project ApiUFC.Minimal/ApiUFC.Minimal.csproj --launch-profile https
```

Swagger UI: `https://localhost:7090/swagger`

These URLs come from the included launch profiles; they may differ if you change the development settings. Swagger is enabled in the `Development` environment.

## 📚 What I Learned

This project gave me hands-on practice with:

- Designing REST endpoints with ASP.NET Core
- Comparing controller-based APIs with Minimal APIs
- Applying separation of concerns through models, interfaces, and services
- Using dependency injection to select service implementations
- Performing asynchronous CRUD operations
- Working with Entity Framework Core, SQL Server, and migrations
- Validating request data and returning appropriate HTTP responses

## 🔧 Future Improvements

- Introduce DTOs to separate API requests and responses from database entities
- Add automated unit and integration tests
- Add pagination, filtering, and sorting for large collections
- Model and enforce database relationships between fighters, divisions, and nations
- Move the data-source setting into application configuration
- Add centralized error handling and more detailed API documentation

---

Built as a learning project to strengthen my **C# and .NET backend development** skills.
