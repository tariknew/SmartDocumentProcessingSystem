## Technologies Used

- **Backend**: ASP.NET Core Web API with Entity Framework, both the API and the SQL database are containerized using Docker.
- **Frontend**: Angular, also containerized using Docker.

### Prerequisites

- **Node.js (For Angular)**
- **SQL Server (for database)**
- **.NET 8.0 SDK (For ASP.NET Core)**

### Running the Application (without docker)

1. Run the Angular frontend:

```bash
ng serve
```

1. Run the ASP.NET Core backend:

```bash
dotnet run
```

### How to Install (with docker)

1. Make sure you have Docker Desktop installed. Then run

```bash
docker-compose up
```

```bash
The backend is at: http://localhost:5055/swagger/index.html
The frontend is at: http://localhost:4444
```
