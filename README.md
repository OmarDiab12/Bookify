# Bookify

[![build](https://github.com/OmarDiab12/Bookify/actions/workflows/ci.yml/badge.svg)](https://github.com/OmarDiab12/Bookify/actions/workflows/ci.yml)
![.NET 8](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)
![Redis](https://img.shields.io/badge/Redis-cache-DC382D?logo=redis&logoColor=white)
![Keycloak](https://img.shields.io/badge/Keycloak-auth-4D4D4D?logo=keycloak)
![Docker](https://img.shields.io/badge/Docker-compose-2496ED?logo=docker&logoColor=white)

A production-ready Apartment Booking Platform built with ASP.NET Core and modern software architecture principles including Clean Architecture, Domain-Driven Design (DDD), CQRS, Vertical Slice Architecture, and Event-Driven Design.

---

## Overview

Bookify is a scalable booking management platform that allows users to search apartments, create bookings, and manage reservations through a modern RESTful API.

The project was built to demonstrate enterprise-level backend development practices commonly used in large-scale systems.

---

## Key Features

### Authentication & Authorization
- JWT Authentication
- Keycloak Identity Provider
- Role-Based Authorization
- Permission-Based Authorization

### Apartment Management
- Apartment Listing
- Apartment Search
- Apartment Details
- Amenities Support

### Booking Management
- Create Booking
- Retrieve Booking Details
- Reservation Workflow
- Domain Events

### Infrastructure
- Redis Caching
- Background Jobs
- Structured Logging
- Health Checks
- API Versioning

---

# Architecture

The solution follows Clean Architecture principles.

```text
┌─────────────────────┐
│      Bookify.Api    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Bookify.Application │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Bookify.Domain    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│Bookify.Infrastructure│
└─────────────────────┘
```

### Domain Layer
Contains:

- Entities
- Value Objects
- Domain Events
- Business Rules
- Repository Contracts

### Application Layer
Contains:

- CQRS Commands
- CQRS Queries
- MediatR Handlers
- Validation
- Application Services

### Infrastructure Layer
Contains:

- Entity Framework Core
- Authentication Providers
- Redis Integration
- Persistence
- External Services

### API Layer
Contains:

- Controllers / Endpoints
- Middleware
- Swagger
- Authentication Configuration
- Dependency Injection

---

# Design Patterns

This project demonstrates:

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- Vertical Slice Architecture
- Repository Pattern
- Unit Of Work
- Domain Events
- Dependency Injection
- Middleware Pipeline
- Result Pattern

---

# Technology Stack

### Backend

- ASP.NET Core 8
- C#
- Entity Framework Core
- Dapper
- MediatR
- FluentValidation

### Security

- JWT
- Keycloak

### Infrastructure

- Redis
- Serilog
- Quartz.NET

### DevOps

- Docker
- Docker Compose

### Documentation

- Swagger / OpenAPI

---

# Project Structure

```text
src
│
├── Bookify.Api
├── Bookify.Application
├── Bookify.Domain
└── Bookify.Infrastructure
```

---

# Request Flow

```text
HTTP Request
      │
      ▼
API Endpoint
      │
      ▼
MediatR
      │
      ▼
Command / Query Handler
      │
      ▼
Domain Logic
      │
      ▼
Repository
      │
      ▼
Database
```

---

# Running With Docker

### Clone Repository

```bash
git clone https://github.com/OmarDiab12/Bookify.git
```

### Run Containers

```bash
docker-compose up -d
```

### Verify Services

| Service | Port |
|----------|----------|
| API | 5000 |
| Keycloak | 8080 |
| Seq | 8081 |
| Redis | 6379 |

---

# API Documentation

Swagger:

```text
http://localhost:5000/swagger
```

---

# Observability

### Structured Logging

Implemented using Serilog.

### Log Viewer

Seq Dashboard:

```text
http://localhost:8081
```

### Health Checks

```text
/health
```

---

# Authentication

The system uses:

- JWT Bearer Tokens
- Keycloak Authentication
- Permission-Based Authorization

---

# Testing

Planned coverage includes:

- Unit Testing
- Integration Testing
- Endpoint Testing

Tools:

- xUnit
- Moq
- FluentAssertions

---

# Future Improvements

- Payment Gateway Integration
- Booking Cancellation Workflow
- Email Notifications
- Apartment Reviews
- Admin Dashboard
- Multi-Tenant Architecture
- Event Bus Integration
- Microservices Migration

---

# What This Project Demonstrates

- Enterprise Architecture
- Scalable API Design
- Domain-Driven Design
- CQRS Implementation
- Modern Authentication
- Caching Strategies
- Logging & Monitoring
- Containerized Deployment

---

# Author

Omar Mohamed

.NET Developer

LinkedIn: https://www.linkedin.com/in/omr-diyab/
