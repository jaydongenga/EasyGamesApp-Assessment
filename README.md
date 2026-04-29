# EasyGames Transaction Manager
### EasyGames Developer Assessment 2026

A full-stack web application built with **C# ASP.NET Core MVC** and **MS SQL Server** for managing client transactions.

---

## 🖥️ Screenshots

> Home screen showing client list with search and sort

![Home Screen](screenshots/home.png)

> Client selected showing transactions, add form, and edit modal

![Transactions](screenshots/transactions.png)

---

## ✅ Features

### Compulsory
- View all clients in a list with their current balance
- Select a client to view all their transactions
- Add a new Debit or Credit transaction for a client
- Client balance updates automatically when a transaction is added
- Edit the comment on any transaction via a modal popup
- All data saved to MS SQL Server database

### Bonus
- 🔍 **Search** — filter clients by name in real time
- 🔃 **Sort** — order clients by Name A→Z, Name Z→A, Balance High→Low, Balance Low→High
- 🎨 **UI Design** — dark gold-themed interface using Bootstrap 5 + custom CSS
- 💬 **JavaScript** — modal popup for editing transaction comments
- ⚡ **Dapper** — lightweight database querying (no Entity Framework)
- 🗄️ **Stored Procedures** — all database operations use SQL stored procedures

---

## 🛠️ Technologies Used

| Layer | Technology |
|---|---|
| Frontend | HTML, CSS, Bootstrap 5, JavaScript |
| Backend | C# — ASP.NET Core MVC (.NET 8) |
| Database | MS SQL Server Express |
| ORM | Dapper |
| Database Logic | Stored Procedures (6 total) |
| Version Control | Git + GitHub |

---

## 🗄️ Database Structure

### Tables
- **TransactionType** — stores Debit (1) and Credit (2) types
- **Client** — stores client name, surname and current balance
- **Transaction** — stores each transaction linked to a client and type

### Stored Procedures
| Procedure | Purpose |
|---|---|
| `sp_GetClients` | Get all clients with optional search and sort |
| `sp_GetClientByID` | Get a single client by ID |
| `sp_GetTransactionsByClient` | Get all transactions for a client |
| `sp_AddTransaction` | Add a transaction and update client balance |
| `sp_UpdateComment` | Edit a transaction comment |
| `sp_GetTransactionTypes` | Get Debit and Credit types |

---

## 🚀 How to Run the Project

### Prerequisites
- Visual Studio 2022
- .NET 8 SDK
- MS SQL Server Express
- SQL Server Management Studio (SSMS)

### Steps

**1. Set up the database**
- Open SSMS and connect to your SQL Server
- Run the `StoredProcedures.sql` file included in this repo
- This creates the `EasyGamesDB` database, all tables, stored procedures and sample data

**2. Configure the connection string**
- Open `appsettings.json`
- Update the connection string if your SQL Server instance name is different:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EasyGamesDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**3. Run the application**
- Open `EasyGamesApp.sln` in Visual Studio 2022
- Press `F5` or click the green ▶ Play button
- The app opens in your browser automatically

---

## 📁 Project Structure

```
EasyGamesApp/
├── Controllers/
│   └── ClientController.cs       ← Handles all page requests
├── Data/
│   └── TransactionRepository.cs  ← All database calls using Dapper
├── Models/
│   ├── Client.cs
│   ├── Transaction.cs
│   ├── TransactionType.cs
│   └── ClientViewModel.cs
├── Views/
│   ├── Client/
│   │   └── Index.cshtml          ← Main page (UI)
│   └── Shared/
│       └── _Layout.cshtml        ← Shared layout with Bootstrap
├── Program.cs                    ← App startup and dependency injection
├── appsettings.json              ← Database connection string
└── StoredProcedures.sql          ← Full database setup script
```

---

## 👤 Author

**Jaydon Genga**
EasyGames Developer Assessment — 2026
