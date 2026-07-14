# Product Management

A full-stack Product Management application built with **ASP.NET Core** and **Angular**.

## Dashboard

The application provides an inventory dashboard with:

- Total products
- Total inventory value
- Total units in stock
- Low stock products
- Quick stock replenishment

---

## API Documentation

- docs/swagger.json
- docs/openapi.yaml

---

## Features

### Backend

- RESTful API
- Create, update and delete products
- Automatic product number generation
- Search products by name
- Search products by stock range
- Search products by product number
- Increase stock
- Decrease stock
- Dashboard statistics
- Input validation
- Swagger documentation

### Frontend

- Dashboard
- Product List
- Product Details
- Create Product
- Edit Product
- Delete Product
- Search products
- Stock range filtering
- Sorting
- Pagination
- Low stock dashboard
- Add stock dialog
- Remove stock dialog
- Responsive Angular Material UI

---

## Technologies

### Backend

- ASP.NET Core 10
- Entity Framework Core
- SQLite
- FluentValidation
- Swagger (OpenAPI)
- xUnit

### Frontend

- Angular 20
- Angular Material
- TypeScript
- RxJS

### DevOps

- Docker
- Docker Compose

---

# Application Architecture

```
Angular
       │
       ▼
ASP.NET Core Web API
       │
       ▼
Services
       │
       ▼
Repositories
       │
       ▼
SQLite Database
```

---

# Project Structure

```
ProductManagement
│
├── ProductManagementAPI
│
│   ├── Controllers
│   ├── Services
│   ├── Repositories
│   ├── Data
│   ├── DTOs
│   ├── Validators
│   └── Tests
│
└── ProductManagementFrontend
    │
    ├── Core
    ├── Shared
    ├── Features
    │     ├── Dashboard
    │     └── Products
    └── Assets
```

---

# Running locally

## Backend

Restore dependencies

```bash
dotnet restore
```

Apply migrations

```bash
dotnet ef database update
```

Run

```bash
dotnet run
```

Swagger

```
http://localhost:8080/swagger
```

---

## Frontend

Install dependencies

```bash
npm install
```

Run

```bash
ng serve
```

Open

```
http://localhost:4200
```

---

# Docker

Build everything

```bash
docker compose up --build
```

Detached

```bash
docker compose up -d
```

Stop

```bash
docker compose down
```

---

# Testing

Backend

```bash
dotnet test
```

---


# Screenshots

Dashboard

> Add screenshot

Products

> Add screenshot

Product Details

> Add screenshot

Create Product

> Add screenshot

---

# Author

**Sergiy Kostenko**

GitHub:
https://github.com/SergiyKostenko23