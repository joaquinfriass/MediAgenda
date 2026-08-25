# 🩺 MediAgenda

<p align="center">
  <strong>A cross-platform medical appointment management application built with .NET MAUI.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET_MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET MAUI" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/XAML-0C54C2?style=for-the-badge&logo=xaml&logoColor=white" alt="XAML" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" alt="SQLite" />
</p>

---

## 📖 About the Project

**MediAgenda** is a cross-platform application designed to manage medical appointments between patients and healthcare professionals.

The application allows patients to explore available clinics and professionals, filter them by medical specialty, select available dates and times, request appointments, and manage their existing bookings.

The project was developed as part of the **Professional Practice II** course, applying concepts related to software analysis, interface design, application development, data persistence, and software quality.

> [!NOTE]
> MediAgenda is an academic demonstration project. The healthcare professionals, clinics, patients, schedules, and credentials included in the application are sample data.

---

## ✨ Main Features

### 🔐 Patient authentication

- Local patient authentication
- Email and password validation
- Session management
- User-friendly login feedback
- Demo patient account included

### 📅 Medical appointment booking

- Selection by medical specialty
- Healthcare professional selection
- Clinic information for each professional
- Date selection based on specialty schedules
- Available time-slot generation
- Appointment-reason registration
- Duplicate schedule validation
- Prevention of conflicting appointments

### 📋 Appointment management

- View requested appointments
- View accepted appointments
- View completed appointments
- Appointment details including:
  - Medical specialty
  - Healthcare professional
  - Clinic
  - Date and time
- Appointment cancellation
- Status-based appointment organization

### 👨‍⚕️ Healthcare professionals

- Professional directory
- Medical specialty information
- Professional license number
- Associated clinic
- Specialty-based filtering

### 🏥 Medical clinics

- Clinic directory
- Address information
- City
- Contact phone number
- Associated healthcare professionals

### 💾 Local data persistence

- Local SQLite database
- Entity Framework Core integration
- Automatic database creation
- Preloaded sample data
- Relational data model

---

## 🏗️ Architecture

MediAgenda follows a simple layered organization:

```text
┌───────────────────────────────┐
│ User Interface                │
│ .NET MAUI + XAML              │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│ Application Services          │
│ Authentication & Appointments │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│ Entity Framework Core         │
│ Data access and relationships │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│ Local SQLite Database         │
│ mediagenda.db                 │
└───────────────────────────────┘
```

---

## 🛠️ Technology Stack

### Application

- .NET 8
- .NET MAUI
- C#
- XAML
- MAUI Shell and NavigationPage
- Asynchronous programming with `async` and `await`

### Data persistence

- SQLite
- Entity Framework Core 8
- Code-first relational models
- Automatic database initialization
- Seed data

### Development tools

- Visual Studio 2022
- .NET SDK
- Android SDK and emulator
- NuGet
- Git and GitHub

---

## 🗃️ Data Model

The local database contains the following main entities:

| Entity | Purpose |
|---|---|
| `Paciente` | Stores patient information and authentication data |
| `Clinica` | Stores clinic contact and location information |
| `Profesional` | Stores healthcare professionals and specialties |
| `Turno` | Stores appointments, dates, reasons, and statuses |

### Relationships

```text
Clinic
  └── has many Healthcare Professionals

Healthcare Professional
  ├── belongs to one Clinic
  └── has many Appointments

Patient
  └── has many Appointments

Appointment
  ├── belongs to one Patient
  └── belongs to one Healthcare Professional
```

---

## 📂 Project Structure

```text
MediAgenda/
├── MediAgenda.sln                  # Visual Studio solution
├── MediAgenda/
│   ├── Data/
│   │   └── AppDbContext.cs         # Entity Framework database context
│   ├── Models/
│   │   ├── Clinica.cs              # Clinic model
│   │   ├── Paciente.cs             # Patient model
│   │   ├── Profesional.cs          # Healthcare professional model
│   │   ├── Turno.cs                # Appointment model
│   │   └── Usuario.cs              # User model
│   ├── Services/
│   │   ├── AutenticacionService.cs # Authentication logic
│   │   └── TurnoService.cs         # Appointment business logic
│   ├── Platforms/                  # Platform-specific configuration
│   ├── Resources/                  # Images, fonts, icons, and styles
│   ├── LoginPage.xaml              # Login interface
│   ├── MenuPage.xaml               # Main navigation menu
│   ├── MainPage.xaml               # Appointment booking
│   ├── MisTurnosPage.xaml          # Patient appointments
│   ├── ClinicasPage.xaml           # Clinic directory
│   ├── ProfesionalesPage.xaml      # Professional directory
│   ├── MauiProgram.cs              # Services and application setup
│   └── MediAgenda.csproj           # Project configuration
└── README.md
```

---

## ✅ Prerequisites

Before running MediAgenda, install:

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- The **.NET Multi-platform App UI development** workload
- Android SDK and an Android emulator for mobile execution
- Git

The project currently targets:

- Android
- Windows

---

## 🚀 Installation

### 1. Clone the repository

```bash
git clone https://github.com/joaquinfriass/MediAgenda.git
cd MediAgenda
```

### 2. Restore the dependencies

```bash
dotnet restore MediAgenda.sln
```

### 3. Open the solution

Open the following file in Visual Studio:

```text
MediAgenda.sln
```

### 4. Select the target platform

From the Visual Studio toolbar, select:

- An Android emulator or connected Android device
- Windows Machine

### 5. Run the application

Press:

```text
F5
```

Alternatively, use the .NET CLI.

For Windows:

```bash
dotnet build MediAgenda/MediAgenda.csproj -t:Run -f net8.0-windows10.0.19041.0
```

For Android:

```bash
dotnet build MediAgenda/MediAgenda.csproj -t:Run -f net8.0-android
```

An Android emulator or connected device must be available before running the Android command.

---

## 💾 Database Setup

No external database server is required.

MediAgenda automatically creates a local SQLite database when the application starts:

```text
mediagenda.db
```

The database is stored inside the application data directory of the selected platform.

Entity Framework Core initializes:

- Database tables
- Entity relationships
- Sample clinics
- Sample healthcare professionals
- A demo patient

---

## 🔑 Demo Credentials

Use the following credentials to access the application:

```text
Email: juan@example.com
Password: 12345678
```

In this academic version, the demo patient's DNI is used as the password.

> [!WARNING]
> This authentication method is intended only for demonstration purposes. A production system should store securely hashed passwords and use a complete authentication and authorization mechanism.

---

## 🧪 Sample Data

The application includes sample information for testing:

### Medical specialties

- Cardiology
- General Medicine
- Dermatology
- Pediatrics

### Clinics

- Clínica Santa María — Formosa
- Sanatorio del Sol — Corrientes
- Centro Médico Norte — Resistencia

### Appointment schedules

Availability is generated according to the selected specialty:

| Specialty | Available days | Hours |
|---|---|---|
| Cardiology | Monday, Wednesday, Friday | 09:00–15:00 |
| General Medicine | Monday–Friday | 09:00–20:00 |
| Dermatology | Tuesday, Thursday, Friday | 16:00–20:00 |
| Pediatrics | Monday, Tuesday, Friday | 09:00–14:00 |

Appointments are offered in 30-minute intervals for the next 30 days.

---

## 🔄 Appointment Lifecycle

Appointments can move through the following states:

```text
Requested → Accepted → Completed
     │
     └──────────────→ Cancelled
```

The current patient interface allows users to request, view, and cancel appointments.

---

## 🧪 Software Quality

The project applies:

- Separation between UI, services, models, and data access
- Relational entity modeling
- Asynchronous database operations
- Form validation
- Schedule-conflict prevention
- Exception handling
- Reusable application services
- Dependency injection
- User-friendly validation messages
- Seed data for repeatable demonstrations

---

## 📌 Project Status

**Functional academic demo — Version 1.0**

The principal appointment-management features are implemented and operational. The project is intended for academic evaluation, learning, and demonstration purposes.

---

## 🔮 Future Improvements

- Patient registration
- Secure password hashing
- Role-based access for patients, professionals, and administrators
- Professional appointment administration
- Email and push-notification reminders
- Calendar integration
- Medical appointment rescheduling
- Patient profile management
- Cloud database synchronization
- REST API integration
- Automated unit and UI tests
- Accessibility improvements
- Production-ready session management
- Dynamic use of the authenticated patient's ID

---

## 👨‍💻 Author

**Joaquín Frías**  
Software Development Technician

<p>
  <a href="https://github.com/joaquinfriass">
    <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white" alt="GitHub" />
  </a>
  <a href="https://www.linkedin.com/in/joaquin-frias-b78935242">
    <img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" alt="LinkedIn" />
  </a>
  <a href="mailto:gjoaquinfrias@gmail.com">
    <img src="https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white" alt="Gmail" />
  </a>
</p>

**GitHub:**  
<https://github.com/joaquinfriass>

**LinkedIn:**  
<https://www.linkedin.com/in/joaquin-frias-b78935242>

**Email:**  
gjoaquinfrias@gmail.com

---

<p align="center">
  Built with ❤️ by Joaquín Frías · 2026
</p>
