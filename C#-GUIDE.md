# C# Development in GitHub Codespaces

## Overview
This workspace is set up for C# development using .NET 8.0 and includes all necessary extensions and configurations.

## What's Included

### Extensions
- **C# Dev Kit**: Provides IntelliSense, debugging, and project management
- **C# Extension**: Core C# language support

### Project Structure
- `HelloWorld/`: A sample C# console application
- `.devcontainer/`: Configuration for consistent development environment
- `.vscode/`: VS Code specific settings for tasks and debugging

## Getting Started

### 1. Basic Commands
```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Create a new project
dotnet new console -n MyNewProject

# Add a package
dotnet add package Newtonsoft.Json

# Restore packages
dotnet restore

# Run tests
dotnet test
```

### 2. Project Types
You can create different types of projects:
```bash
# Console application
dotnet new console -n MyConsoleApp

# Web API
dotnet new webapi -n MyWebApi

# Class library
dotnet new classlib -n MyLibrary

# ASP.NET Core MVC
dotnet new mvc -n MyMvcApp

# Blazor Server
dotnet new blazorserver -n MyBlazorApp
```

### 3. Development Workflow

#### Building and Running
- Use `Ctrl+Shift+P` → "Tasks: Run Task" → "build" to build
- Use `F5` to run with debugging
- Use `Ctrl+F5` to run without debugging

#### Package Management
- Packages are managed through `.csproj` files
- Use `dotnet add package` to add dependencies
- Use `dotnet restore` to restore packages

#### Testing
```bash
# Create a test project
dotnet new xunit -n MyProject.Tests

# Run tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### 4. Debugging
- Set breakpoints by clicking in the gutter
- Use `F5` to start debugging
- Use `F10` to step over, `F11` to step into
- View variables in the Debug Console

### 5. Common NuGet Packages
```bash
# JSON serialization
dotnet add package Newtonsoft.Json

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# HTTP client
dotnet add package Microsoft.Extensions.Http

# Logging
dotnet add package Microsoft.Extensions.Logging

# Configuration
dotnet add package Microsoft.Extensions.Configuration
```

### 6. Tips for Codespaces
- All dependencies are automatically installed
- The environment is consistent across different machines
- Use port forwarding for web applications
- The `.devcontainer` configuration ensures everyone has the same setup

## Example Projects

### Web API Example
```bash
cd /workspaces/ps4
dotnet new webapi -n MyWebApi
cd MyWebApi
dotnet run
```

### Adding Entity Framework
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

## Troubleshooting

### Common Issues
1. **Build fails**: Check package references and restore with `dotnet restore`
2. **IntelliSense not working**: Restart the C# extension or reload the window
3. **Debugging not working**: Check the launch.json configuration

### Useful Commands
```bash
# Check .NET version
dotnet --version

# List installed SDKs
dotnet --list-sdks

# Clean build artifacts
dotnet clean

# Publish application
dotnet publish -c Release
```

## Next Steps
1. Explore the sample HelloWorld project
2. Try creating different project types
3. Add external packages and dependencies
4. Set up unit tests
5. Deploy to Azure or other cloud platforms

Happy coding with C# in Codespaces! 🚀
