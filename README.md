# Ebay Clone API

## Overview

Ebay Clone API is a backend service that replicates core functionalities of the Ebay platform.  
The project is built with **.NET Core Web API** and follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles to ensure scalability, maintainability, and clear separation of concerns.

The system supports **authentication & authorization**, **product and listing management**, **order processing**, **seller-specific workflows**, **order statistics & reporting**, and **image storage using Cloudinary**, along with **role-based access control**.

---

## Key Features

### Authentication & Authorization

- JWT-based authentication (Access Token & Refresh Token)
- Google OAuth 2.0 login
- Role-based authorization (User, Seller, Admin)
- Secure login, logout, and refresh-token flow

### Architecture & Design Patterns

- Clean Architecture (API, Application, Domain, Infrastructure)
- Domain-Driven Design (DDD)
- CQRS (Command Query Responsibility Segregation)
- Unit of Work pattern
- MediatR for handling commands and queries
- AutoMapper for Entity ↔ DTO mapping

### Marketplace & Business Logic

- Create, approve, cancel, and manage listings
- Shopping cart functionality
- Order lifecycle management (InCart, WaitingOwnerConfirmation, Completed, Canceled)
- Seller-specific order management
- Admin order statistics and reporting (monthly/yearly)

### Database & Data Access

- SQL Server as the database
- Entity Framework Core
- Stored Procedures for complex and performance queries
- Pagination, filtering, and sorting for large datasets

### Media & File Storage

- Image upload and storage using **Cloudinary**
- Product images management
- User avatar upload support

### Security

- Role-based API protection
- Secure password hashing
- Input validation
- Clear separation of responsibilities across layers

---

**Technologies used**: .NET Core (Web API), SQL
Server, EF Core, Auto Mapper, CQRS Pattern, Clean Architecture, Domain-Driven Design, MediatR, Unit Of Work Pattern, Cloudinary, Google OAuth 2.0

---

## Technologies

- [.NET Core](https://dotnet.microsoft.com/en-us/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [MediatR](https://mediatr.io/)

## Prerequisites

The required packages to run the project:

```bash
- .NET SDK >= 9.0.3
- SQL Server >= 18
- Docker >= 20.10.21
```

## Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/phannguyenminhphat1/EbayBE
   ```

2. Install dependencies:

   ```sh
   dotnet restore
   ```

3. Configure environment:

   - Create a `appsetting.json` file and configure the necessary environment variables.

4. Run the application:
   ```sh
   dotnet run || dotnet watch
   ```

## API Endpoints

- **API Base URL:** http://localhost:5185/api

### Authentication

| Method | Endpoint              | Description                        |
| ------ | --------------------- | ---------------------------------- |
| POST   | `/auth/login`         | User login with email and password |
| POST   | `/auth/register`      | Register a new user                |
| POST   | `/auth/logout`        | Logout and revoke refresh token    |
| POST   | `/auth/refresh-token` | Refresh access token               |
| GET    | `/auth/oauth/google`  | Login with Google OAuth            |

---

### Listing

| Method | Endpoint                          | Authorization | Description                               |
| ------ | --------------------------------- | ------------- | ----------------------------------------- |
| GET    | `/listing/get-listings`           | Public        | Get all listings with filter & pagination |
| POST   | `/listing/create-post`            | Authorize     | Create a new listing                      |
| POST   | `/listing/delete-listings`        | Seller, Admin | Delete listings                           |
| POST   | `/listing/approve-cancel-listing` | Admin         | Approve or cancel listing                 |
| POST   | `/listing/upload-images`          | Authorize     | Upload listing images                     |

---

### Order

| Method | Endpoint                           | Authorization | Description                |
| ------ | ---------------------------------- | ------------- | -------------------------- |
| POST   | `/order/add-to-cart`               | Authorize     | Add product to cart        |
| GET    | `/order/get-orders`                | Authorize     | Get user's orders          |
| GET    | `/order/get-orders-by-seller`      | Seller        | Get orders by seller       |
| POST   | `/order/delete-order-details`      | Authorize     | Remove items from cart     |
| PUT    | `/order/update-order-detail`       | Authorize     | Update order item quantity |
| POST   | `/order/buy-products`              | Authorize     | Place an order             |
| PUT    | `/order/cancel-order/{id}`         | Authorize     | Cancel an order            |
| PUT    | `/order/reject-confirm-order/{id}` | Seller        | Reject or confirm order    |
| GET    | `/order/get-order-statistics`      | Admin         | Get order statistics       |

---

### Product

| Method | Endpoint                                    | Description                       |
| ------ | ------------------------------------------- | --------------------------------- |
| GET    | `/product/get-products`                     | Get all products                  |
| GET    | `/product/get-list-listing-products-detail` | Get listing products with filters |
| GET    | `/product/get-listing-product-detail/{id}`  | Get product detail                |

#### Supported Features

- Pagination
- Searching
- Filtering by category, price range, and rating
- Sorting and ordering

---

### User

| Method | Endpoint                | Authorization | Description              |
| ------ | ----------------------- | ------------- | ------------------------ |
| GET    | `/user/get-me`          | Authorize     | Get current user profile |
| PUT    | `/user/update-me`       | Authorize     | Update user profile      |
| POST   | `/user/upload-avatar`   | Authorize     | Upload user avatar       |
| PUT    | `/user/change-password` | Authorize     | Change user password     |

---

## Order Statistics

- Monthly and yearly order statistics
- Revenue and order count analysis
- Admin-only access

---

## Notes

- This project is intended for **learning, practice, and portfolio purposes**
- Designed with **real-world backend architecture patterns**
