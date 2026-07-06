<div align="center">
  
# 📚 LibRazor - Library and Book Management System
  
A modern and dynamic library management platform built with **ASP.NET Core Razor Pages** and **ADO.NET**.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC292B?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap_5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)

[Features](#-features) • [Screenshots](#-screenshots) • [Installation](#-installation) • [Technologies](#-technologies)

</div>

---

## 💡 About The Project

**LibRazor** is a comprehensive web application that allows you to easily manage your books and authors, as well as export system data. The project is designed to execute high-performance SQL queries using **ADO.NET** directly in the data access layer.

## ✨ Features

- 🔐 **Authentication:** Secure cookie-based login, registration, and session management.
- 📖 **Book & Author Module:** Fast CRUD (Create, Read, Update, Delete) operations powered by ADO.NET infrastructure.
- 🔍 **Dynamic Search:** Quick filtering across records.
- 📊 **Advanced Export:**
  - Download data in **Excel (.xlsx)** format with a single click *(using ClosedXML)*.
  - Export data as professional-looking **PDF** documents *(using QuestPDF)*.
- 🎨 **Modern Interface:** Fully responsive and stylish design built with Bootstrap 5 (Stylish Portfolio theme).

---

## 📸 Screenshots

<details>
<summary><b>1. Home Page</b></summary>
<br>
<img src="https://github.com/user-attachments/assets/22f4e824-b21d-4abd-9659-08ba5e902816" alt="Home Page" />
</details>

<details>
<summary><b>2. Authentication (Login & Register)</b></summary>
<br>

<img src="https://github.com/user-attachments/assets/fcf82274-59b9-43d6-a0c8-53a3f84d280f" alt="Login Page" />

</details>

<details>
<summary><b>3. Book Management (CRUD & Export)</b></summary>
<br>

<img src="https://github.com/user-attachments/assets/9fc529cd-f812-4429-acd1-e923dddd1c3e" alt="Books Index" />

</details>

<details>
<summary><b>4. Author Management (CRUD)</b></summary>
<br>

<img src="https://github.com/user-attachments/assets/ddf7a2ea-b9bf-46f4-9d7f-2e0a87cfaadf" alt="Authors Index" />

</details>

<details>
<summary><b>5. Reports</b></summary>
<br>

<img src="https://github.com/user-attachments/assets/f10227d5-d110-4559-ba7b-c83802745430" alt="Reports" />

</details>

---

## 🛠 Technologies

The following modern technologies and libraries were used during the development process:

| Category | Technology / Library |
|---|---|
| **Backend** | `C#`, `ASP.NET Core Razor Pages (.NET 8)` |
| **Database** | `MS SQL Server` (LocalDB) |
| **Data Access** | `ADO.NET` (`Microsoft.Data.SqlClient`) |
| **Frontend** | `HTML5`, `CSS3`, `Bootstrap 5`, `Font Awesome` |
| **Tools / Libraries** | `ClosedXML` (Excel), `QuestPDF` (PDF) |

---

## ⚙️ Installation (Local Development)

Follow these steps to run the project on your local machine.

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or LocalDB)

### Steps

1. **Clone the Repository**
   ```bash
   git clone <repo-url>
   cd LibRazor
   ```

2. **Prepare the Database**
   Run the following script to create a database named `LibRazorDb` and its tables on SQL Server:
   <details>
   <summary><b>Show SQL Script</b></summary>
   
   ```sql
   CREATE DATABASE LibRazorDb;
   GO
   USE LibRazorDb;
   GO
   
   -- Users Table
   CREATE TABLE Kullanicilar (
       Id INT IDENTITY(1,1) PRIMARY KEY,
       AdSoyad NVARCHAR(100),
       Email NVARCHAR(100) UNIQUE,
       Sifre NVARCHAR(255)
   );
   
   -- Your table creation scripts for Books and Authors...
   ```
   </details>

3. **Install Packages**
   ```bash
   dotnet restore
   ```

4. **Run the Project**
   ```bash
   dotnet run
   ```
   Open your browser and navigate to `https://localhost:<port>` to start using the application.
