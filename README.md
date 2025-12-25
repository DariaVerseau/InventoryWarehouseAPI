# InventoryWarehouseAPI

> A backend REST API for warehouse, category, and inventory management  
> © **DariaVerseau**, 2025

---

## 📌 Overview

**InventoryWarehouseAPI** is a RESTful web service built with **ASP.NET Core 9**, designed to manage warehouse inventory systems. This project was developed as part of a university course on modern software development practices.

### Key Features:
- 📦 **Warehouse Management** – create and manage storage locations
- 🗂 **Product Categories** – with visibility control, search, and filtering
- 📦 **Products** – linked to categories and suppliers
- 📊 **Inventory Tracking** – real-time stock levels per warehouse
- 🔐 **Authentication** – user registration and login
- 🔍 **Advanced Filtering** – by name, visibility, and search term
- 🧮 **Sorting & Pagination** – up to 5 items per page
- ✏️ **Partial Updates** – via `PATCH` requests
- 🗃 **Database** – PostgreSQL with Entity Framework Core

The project follows **Clean Architecture** principles with layered separation: `API` → `BLL` → `DAL` → `DTO`.

---

## 🛠 Technologies Used

- **Language**: C# 12
- **Framework**: ASP.NET Core 9
- **ORM**: Entity Framework Core + Npgsql
- **Database**: PostgreSQL
- **Architecture**: Clean Architecture, Repository Pattern, DTOs
- **Tools**: JetBrains Rider, Git, Insomnia

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- PostgreSQL (v12+)

### Setup
1. Create a PostgreSQL database:
   ```sql
   CREATE DATABASE "InventoryWarehousedb";
