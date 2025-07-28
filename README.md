# ProjectTracker — Lightweight Project Tracking Tool

### **Overview**

ProjectTracker is a lightweight internal project tracking tool built with **ASP.NET Core 9 (Web API)** and **SQL Server**, designed with clean architecture principles:

*   **Domain Layer**: Core business entities and validation logic.
    
*   **Application Layer**: Service layer, DTOs, and mapping.
    
*   **Infrastructure Layer**: EF Core, database access, and repository pattern.
    
*   **API Layer**: REST endpoints with Swagger for interactive documentation.
    

### **Tech Stack**

*   **.NET 9** (ASP.NET Core Web API)
    
*   **Entity Framework Core (SQL Server)**
    
*   **xUnit** (Unit Testing)
    
*   **Swagger (via Swashbuckle)** for API documentation
    
*   **AutoMapper** for DTO ↔ Entity mapping
    
*   **In-Memory EF Core** for testing
    
*   **Git** for version control
    
**Setup Instructions**
----------------------

### **1\. Clone Repository**
```bash
clone https://github.com/your-org/ProjectTracker.git
```
```bash
  cd ProjectTracker
```
### **2\. Configure Database**
Update appsettings.Development.json in **ProjectTracker.Api**:
```json
{ "ConnectionStrings": { 
              "DefaultConnection": "Server=localhost;Database=ProjectTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;" 
          } 
}
```

### **3\. Run Migrations**
```bash
dotnet ef migrations add InitialCreate -p ProjectTracker.Infrastructure -s ProjectTracker.Api -o Data/Migrations
```
```bash
dotnet ef database update -p ProjectTracker.Infrastructure -s ProjectTracker.Api
```

### **4\. Run the API**
```bash
dotnet run --project ProjectTracker.Api
```

Swagger will be available at:
`https://localhost:5001/swagger`

**API Endpoints**
-----------------
<table border="1" cellpadding="8" cellspacing="0" style="border-collapse: collapse; text-align: left;">
  <thead>
    <tr style="background-color: #f2f2f2;">
      <th>Method</th>
      <th>Endpoint</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="color: green; font-weight: bold;">GET</td>
      <td>/api/projects</td>
      <td>Get all projects</td>
    </tr>
    <tr>
      <td style="color: green; font-weight: bold;">GET</td>
      <td>/api/projects/{id}</td>
      <td>Get project by ID</td>
    </tr>
    <tr>
      <td style="color: blue; font-weight: bold;">POST</td>
      <td>/api/projects</td>
      <td>Create new project</td>
    </tr>
    <tr>
      <td style="color: orange; font-weight: bold;">PUT</td>
      <td>/api/projects/{id}</td>
      <td>Update existing project</td>
    </tr>
    <tr>
      <td style="color: red; font-weight: bold;">DELETE</td>
      <td>/api/projects/{id}</td>
      <td>Delete project</td>
    </tr>
  </tbody>
</table>



**Testing**
-----------

Run unit tests with:
```bash
dotnet test
```

Tests cover:
*   **Repository layer** using EF Core InMemory provider.
    
*   **Service layer** for CRUD operations.
    
