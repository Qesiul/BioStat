# BioStat 🧬

**BioStat** is a modern desktop application built with WPF and .NET, designed to calculate and manage key body composition metrics such as **BMI**, **NFFMI**, **RMR**, and **WHR**.

The application emphasizes clean architecture (MVVM), simplicity, and performance — ideal for users who want a lightweight yet powerful tool to track body-related statistics locally, without cloud dependencies.

---

## 📌 Features

- 🧮 **Calculate key metrics**: BMI, NFFMI, RMR, WHR and more.
- 💾 **Local database**: Uses SQLite with Entity Framework Core.
- 🧠 **MVVM Architecture**: Ensures maintainability and clean separation of concerns.
- 🎨 **Minimalist UI**: Custom and minimalistic design with smooth layout and styling.
- 🔒 **No account required**: Data is stored and used locally.
- 📁 **Persistent storage**: Measurements are saved to disk and can be retrieved across sessions.

---

## 🛠️ Technologies Used

| Stack         | Details                                 |
|---------------|------------------------------------------|
| UI Framework  | WPF (.NET 9.0)                           |
| Backend       | Entity Framework Core 9 + SQLite         |
| Architecture  | MVVM (Model-View-ViewModel)              |
| Styling       | XAML with custom themes and layout       |
| Packaging     | Self-contained `.exe` build via `dotnet` |

---

## 🚀 Getting Started

### 📦 Requirements
- Windows 10+
- No .NET runtime required (self-contained build)

### 📤 Published Build
1. Navigate to Releases

2. Download the latest .zip

3. Extract and run BioStat.exe

### 🗃️ Database Handling
- The application uses Entity Framework Core with a local SQLite database

- On first run, the database is automatically created in the app’s root directory

- No external configuration is required — everything is bundled

### 🧑‍💻 Important Notes
- Database path is explicitly set to avoid bin/Debug duplication issues

- Migration scripts can be generated using standard EF CLI tools

- All views are dynamically bound using DataTemplates via App.xaml

### 🧪 Running the App (Dev)

```bash
git clone https://github.com/wielki-bo/biostat.git
cd biostat
dotnet restore
dotnet run
```

## 👤 Author
Created  by Luis "Qesiul" Wysocki

## 📄 License
MIT License.
This project is open source and free to use for personal or commercial purposes.



