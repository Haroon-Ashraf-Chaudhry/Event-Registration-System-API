# Event Registration System API

A RESTful Event Registration Management System built with ASP.NET Core Web API, Entity Framework Core, SQLite, and the Repository Pattern.

The system enables event creation, participant registration, event capacity management, registration cancellation, and event tracking while ensuring data integrity, preventing duplicate registrations, and avoiding overbooking scenarios.

---

# Features

## Event Management

* Create new events
* Validate future event dates
* Enforce unique event names
* Configure event capacity

## Registration Management

* Register users for events
* Prevent duplicate registrations
* Track registration timestamps
* Prevent registration when an event is full

## Event Tracking

* Retrieve all events
* Filter upcoming events
* View registration statistics
* Calculate available seats in real time

## Registration Cancellation

* Cancel existing registrations
* Release occupied seats automatically
* Exclude cancelled registrations from active participant counts

## Data Integrity

* Repository Pattern implementation
* Database-level uniqueness constraints
* Input validation
* Proper HTTP status codes
* Persistent storage using SQLite

---

# Technology Stack

| Technology            | Purpose                    |
| --------------------- | -------------------------- |
| ASP.NET Core Web API  | Backend Framework          |
| C#                    | Programming Language       |
| Entity Framework Core | ORM                        |
| SQLite                | Database                   |
| Swagger               | API Documentation          |
| Repository Pattern    | Data Access Layer          |
| REST API              | Communication Architecture |

---

# Architecture

The project follows a layered architecture with Repository Pattern separation.

```text
Client
   │
   ▼
Controllers
   │
   ▼
Repositories
   │
   ▼
Entity Framework Core
   │
   ▼
SQLite Database
```

### Benefits

* Separation of concerns
* Improved maintainability
* Better testability
* Cleaner controller logic
* Easier future scalability

---

# Project Structure

```text
EventRegistrationAPI
│
├── Controllers
│   ├── EventsController.cs
│   └── RegistrationsController.cs
│
├── Data
│   └── AppDbContext.cs
│
├── DTOs
│   ├── CreateEventDto.cs
│   └── RegisterUserDto.cs
│
├── Models
│   ├── Event.cs
│   └── Registration.cs
│
├── Repositories
│   │
│   ├── Interfaces
│   │   ├── IEventRepository.cs
│   │   └── IRegistrationRepository.cs
│   │
│   ├── EventRepository.cs
│   └── RegistrationRepository.cs
│
├── Migrations
│
├── Program.cs
├── appsettings.json
└── README.md
```

---

# Database Schema

## Events

| Column     | Type     |
| ---------- | -------- |
| Id         | Integer  |
| Name       | String   |
| TotalSeats | Integer  |
| EventDate  | DateTime |

---

## Registrations

| Column       | Type     |
| ------------ | -------- |
| Id           | Integer  |
| UserName     | String   |
| RegisteredAt | DateTime |
| IsCancelled  | Boolean  |
| EventId      | Integer  |

---

## Relationships

```text
Event (1)
  │
  └──────< Registration (Many)
```

Each event can have multiple registrations.

Each registration belongs to exactly one event.

---

# API Endpoints

## Create Event

### Request

```http
POST /api/events
```

```json
{
  "name": "Tech Conference 2026",
  "totalSeats": 100,
  "eventDate": "2026-12-15T09:00:00"
}
```

### Success Response

```json
{
  "id": 1,
  "name": "Tech Conference 2026",
  "totalSeats": 100,
  "eventDate": "2026-12-15T09:00:00"
}
```

---

## Get All Events

### Request

```http
GET /api/events
```

### Success Response

```json
[
  {
    "id": 1,
    "name": "Tech Conference 2026",
    "eventDate": "2026-12-15T09:00:00",
    "totalSeats": 100,
    "totalRegistrations": 15,
    "availableSeats": 85
  }
]
```

---

## Get Upcoming Events

### Request

```http
GET /api/events?upcomingOnly=true
```

---

## Register User

### Request

```http
POST /api/registrations
```

```json
{
  "userName": "John Doe",
  "eventId": 1
}
```

### Success Response

```json
{
  "message": "Registration successful."
}
```

---

## Cancel Registration

### Request

```http
DELETE /api/registrations/{registrationId}
```

### Success Response

```json
{
  "message": "Registration cancelled successfully."
}
```

---

# Business Rules

## Event Creation

* Event name must be unique.
* Event date must be in the future.
* Total seats must be greater than zero.

## Registration

* A user cannot register twice for the same event.
* Registration is rejected if the event is full.
* Registration timestamp is stored automatically.

## Cancellation

* Cancelled registrations do not count toward active participants.
* Available seats are recalculated automatically.

---

# Validation & Error Handling

The API returns meaningful error messages for invalid operations.

### Duplicate Event

```json
{
  "message": "Event name already exists."
}
```

### Event Full

```json
{
  "message": "Event is full."
}
```

### Duplicate Registration

```json
{
  "message": "User is already registered for this event."
}
```

### Event Not Found

```json
{
  "message": "Event not found."
}
```

### Registration Not Found

```json
{
  "message": "Registration not found."
}
```

---

# Getting Started

## Prerequisites

* .NET 8 SDK
* Visual Studio 2022 / VS Code
* SQLite

---

## Clone Repository

```bash
git clone https://github.com/yourusername/EventRegistrationAPI.git
cd EventRegistrationAPI
```

---

## Install Dependencies

```bash
dotnet restore
```

---

## Apply Database Migrations

```bash
dotnet ef database update
```

---

## Run Application

```bash
dotnet run
```

---

## Open Swagger

```text
https://localhost:xxxx/swagger
```

Swagger UI provides interactive API testing and complete endpoint documentation.

---

# Repository Pattern Implementation

The application uses Repository Pattern to abstract data access logic from controllers.

### Interfaces

```text
IEventRepository
IRegistrationRepository
```

### Implementations

```text
EventRepository
RegistrationRepository
```

This approach promotes:

* Loose coupling
* Maintainable code
* Better testing support
* Clean separation of concerns

---

# Future Enhancements

* Service Layer Implementation
* JWT Authentication & Authorization
* Role-Based Access Control
* Event Categories
* Search & Pagination
* Unit Testing
* Integration Testing
* Docker Containerization
* Email Notifications
* Logging with Serilog
* Clean Architecture

---

# Assessment Requirements Coverage

✔ Create Event

✔ View Events

✔ Register User

✔ Cancel Registration

✔ Persistent Data Storage

✔ Event Capacity Management

✔ Duplicate Registration Prevention

✔ Input Validation

✔ Error Handling

✔ Repository Pattern

✔ SQLite Database

✔ Swagger Documentation

✔ RESTful API Design
