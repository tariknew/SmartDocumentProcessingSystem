# SmartDocumentProcessingSystem

This management system enables users to extract structured data from PDF and CSV files and display the processed results on the frontend with validation.
It is primarily designed for the sample PDF and CSV files provided with the project. The parsing logic is not fully generalized for all possible document formats, but it currently performs well for the supported use cases.

## Technologies Used

- **Backend**: ASP.NET Core Web API with Entity Framework, both the API and the SQL database are containerized using Docker.
- **Frontend**: Angular, also containerized using Docker.

### Prerequisites

- **Node.js (For Angular)**
- **SQL Server (for database)**
- **.NET 8.0 SDK (For ASP.NET Core)**

### Running the Application (without docker)

1. Install angular dependencies in project:

```bash
npm install
```

2. Run the Angular frontend:

```bash
ng serve
```

3. Run the ASP.NET Core backend:

```bash
dotnet run
```

### How to Install (with docker)

1. Make sure you have Docker Desktop installed. Then run provided command in the location where docker-compose.yml is:

```bash
docker-compose up
```

```bash
The backend is at: http://localhost:5055/swagger/index.html
The frontend is at: http://localhost:4444
```
