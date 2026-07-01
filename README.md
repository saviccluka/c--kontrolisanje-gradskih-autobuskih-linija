# Gradski Transport — City Bus Line Management System

A Windows Forms application for managing city public transport, built following the structure of the EvidencijaTimova project.

## About

This application allows:

- User login with different roles (admin/user)
- Viewing city transport lines
- Viewing departure schedules
- Ticket management (in development)
- System statistics (in development)

## Project Structure

The project is organized in layers (layered architecture):

```
c--kontrolisanje-gradskih-autobuskih-linija/
├── Database/                          # SQL Server setup scripts
├── GradskiTransport.Shared/           # Shared models and constants
├── GradskiTransport.Data/             # Database access layer (Repository pattern)
├── GradskiTransport.Business/         # Business logic
├── GradskiTransport.Presentation/     # User interface (Windows Forms)
├── TestConnectionApp/                 # Database connection test app
├── vs/                                # Visual Studio settings
└── GradskiTransport.sln               # Solution file
```

## Requirements

- Visual Studio 2019 or newer
- Microsoft SQL Server (LocalDB or Express)
- .NET 6.0 or newer

## Getting Started

1. Clone or download the project
2. Open `GradskiTransport.sln` in Visual Studio
3. Set up the database:
   - Open SQL Server Management Studio
   - Connect to **localhost** (SQL Server)
   - Run the `Database/CompleteSetup.sql` script
4. Run the application (F5)

### Running the Application

**1. Visual Studio (recommended)**
- Open `GradskiTransport.sln` in Visual Studio
- Press **F5** or click **Start Debugging**

**2. Command Line (.NET CLI)**
```bash
# Navigate to the main project directory
cd "C:\Users\lukas\OneDrive\Desktop\VP Anchi"

# Run the application
dotnet run --project GradskiTransport.Presentation
```

## Login Credentials

**Administrators**
- `admin` / `admin123` (Admin Admin)
- `petar` / `petar123` (Petar Petrović)
- Role: Administrator (full access)

**Regular Users**
- `marko` / `marko123` (Marko Marković)
- `ana` / `ana123` (Ana Jovanović)
- `stefan` / `stefan123` (Stefan Nikolić)
- `milica` / `milica123` (Milica Stojanović)
- Role: User (view only)

## Features

**Line Overview**
- Display of all city transport lines
- Line number, name, and description information
- Sorted by line number

**Departure Schedule**
- Line selection from a dropdown list
- Display of all departures for the selected line
- Departure time and vehicle type display
- Real-time current time updates

**Ticket Management (in development)**
- Generating new tickets
- Validating existing tickets
- Validation history overview
- Ticket type management

**System Statistics (in development)**
- Ticket sales statistics
- Validation analysis per line
- Traffic reports
- Charts and diagrams

## Database

The application uses an SQL Server database with the following tables:

| Table | Description |
|---|---|
| `KORISNICI` | System users with roles (admin/user) |
| `STANICE` | City transport stations |
| `LINIJE` | City transport lines |
| `LINIJA_STANICA` | Line-to-station relationships |
| `POLASCI` | Departure schedule |
| `PUTNICI` | System passengers |
| `KARTE` | Transport tickets |
| `VALIDACIJE` | Ticket validation history |

**Database content:**
- **6 lines** of city transport (15, 23, 28, 31, 45, 67)
- **10 stations** in Belgrade
- **Departures** for all lines
- **Test users** for different roles

## Tech Stack

- C# (.NET 6.0)
- Windows Forms
- SQL Server
- Microsoft.Data.SqlClient
- Layered Architecture (Repository Pattern)

## Architecture

The project uses layered architecture with the following layers:

1. **Presentation Layer** — Windows Forms user interface
2. **Business Layer** — Business logic and validation
3. **Data Layer** — Repository pattern for database access
4. **Shared Layer** — Shared models and constants

## Development Status

The application is under active development. Currently implemented:

- ✅ User login
- ✅ Line overview
- ✅ Departure schedule
- 🔄 Ticket management (in development)
- 🔄 System statistics (in development)

## Author

Luka Savić
