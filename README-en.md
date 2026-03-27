# Testing Program

A simple application for creating and taking tests with weighted answer coefficients. Suitable for quizzes, psychological tests, and surveys with cumulative scoring systems.

## 📥 Download

The latest version of the program can be downloaded from the [Releases](https://github.com/Musyti/TestingProgram_RPO5_3/releases) page.

Two options are available:
- **install_test.exe** — program installer (recommended method)
- **TestingProgram.rar** — archive with ready-to-use program files

## 🚀 Installation and Launch

### Option 1: Installer (recommended)
1. Download `install_test.exe` from the [Releases](https://github.com/Musyti/TestingProgram_RPO5_3/releases) page or via [Direct Link](https://github.com/Musyti/TestingProgram_RPO5_3/releases/download/(v1.0)/install_test.exe).
2. Run the downloaded file
3. Follow the installer instructions
4. After installation completes, launch the program via the desktop shortcut

### Option 2: Archive
1. Download `TestingProgram.rar` from the [Releases](https://github.com/Musyti/TestingProgram_RPO5_3/releases) page
2. Extract the contents to any folder
3. Run `TestingProgram.exe`

> **Note:** On first launch, the program will automatically create `users.json` and `categories.json` files in the same folder.

## 💻 System Requirements

| Component | Requirement |
|-----------|------------|
| Operating System | Windows 7 / 8 / 10 / 11 |
| .NET Framework | version 4.7.2 or higher |
| Architecture | x86 / x64 |
| RAM | from 256 MB |
| Free Space | from 10 MB |

## 🔑 Getting Started

On first launch, the system already has a test administrator:

| Username | Password | Role |
|----------|----------|------|
| `Admin` | `Admin` | Test Taker |
| `Admin1` | `Admin` | Administrator |

You can log in with these credentials or register a new user.

## 👥 User Roles

### Administrator
- Create new tests
- Edit existing tests
- Delete tests
- Manage categories

### Test Taker
- View available tests
- Take tests
- View results

## 📝 Test Structure

Each test contains:
- **10 questions** — each with 4 answer options
- **Points per answer** — each option has its own weight
- **3 results** — score ranges with title and description

## 📁 Program Files

| File | Purpose |
|------|---------|
| `TestingProgram.exe` | Executable file |
| `users.json` | User storage (created automatically) |
| `categories.json` | Test storage (created automatically) |

## 🛠️ Technologies

- **Language:** C# (.NET Framework 4.7.2)
- **Interface:** Windows Forms
- **Data Storage:** JSON

## 📄 License

This project is distributed under the MIT License. For more details, see the [LICENSE](LICENSE) file.

**The program is ready to use immediately after download!**