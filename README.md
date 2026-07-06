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

> **Note:** After running the application, you can drag and drop your screenshots or GIFs here to make your README more vibrant.

<details>
<summary><b>1. Home Page</b></summary>
<br>
<img src="https://via.placeholder.com/800x400.png?text=Home+Page" alt="Home Page" />
</details>

<details>
<summary><b>2. Authentication (Login & Register)</b></summary>
<br>

| Login | Register |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x250.png?text=Login+Page" alt="Login" /> | <img src="https://via.placeholder.com/400x250.png?text=Register+Page" alt="Register" /> |

</details>

<details>
<summary><b>3. Book Management (CRUD & Export)</b></summary>
<br>

| Books List (Index) | Add New Book (Create) |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x250.png?text=Books+List" alt="Books Index" /> | <img src="https://via.placeholder.com/400x250.png?text=Create+Book" alt="Book Create" /> |

| Edit Book (Edit) | Delete Book (Delete) |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x250.png?text=Edit+Book" alt="Book Edit" /> | <img src="https://via.placeholder.com/400x250.png?text=Delete+Book" alt="Book Delete" /> |

</details>

<details>
<summary><b>4. Author Management (CRUD)</b></summary>
<br>

| Authors List (Index) | Add New Author (Create) |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x250.png?text=Authors+List" alt="Authors Index" /> | <img src="https://via.placeholder.com/400x250.png?text=Create+Author" alt="Author Create" /> |

| Edit Author (Edit) | Delete Author (Delete) |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x250.png?text=Edit+Author" alt="Author Edit" /> | <img src="https://via.placeholder.com/400x250.png?text=Delete+Author" alt="Author Delete" /> |

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