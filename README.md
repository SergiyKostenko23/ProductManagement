# Product Management API

A RESTful API for managing products.

## Features

- Create products
- Update products
- Delete products
- Search by name
- Search by stock range
- Search by product number
- Increase stock
- Decrease stock
- Automatic product number generation
- Input validation
- Swagger documentation

## Technologies

- ASP.NET Core 10
- Entity Framework Core
- SQLite
- Swagger (OpenAPI)
- FluentValidation
- xUnit
- Docker
- Docker Compose

---

## Running locally

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

Swagger:

```
http://localhost:5088/swagger
```

(Replace the port if your project uses a different one.)

---

## Running with Docker

### Build the image

```bash
docker build -t product-management-api .
```

### Run the container

```bash
docker run --rm -p 8080:8080 product-management-api
```

Swagger will be available at:

```
http://localhost:8080/swagger
```

---

## Running with Docker Compose

Build and start the application:

```bash
docker compose up --build
```

Run in detached mode:

```bash
docker compose up -d
```

Stop the application:

```bash
docker compose down
```

View logs:

```bash
docker compose logs -f
```

Swagger:

```
http://localhost:8080/swagger
```

---

## Running Tests

```bash
dotnet test
```

---

## Database

The project uses SQLite.

Database schema is managed through Entity Framework Core migrations.

Create a new migration:

```bash
dotnet ef migrations add MigrationName
```

Apply migrations:

```bash
dotnet ef database update
```

---

## Project Structure

```
Common/
    Enums/
Controllers/
Data/
    Entities/
    Migrations/
DTOs/
Interfaces/
Mappings/
Middleware/
Repositories/
Services/
Validators/
```