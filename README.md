# AuthServer

A centralized authentication server built with ASP.NET Core 8.0, designed to provide secure authentication and authorization services for distributed applications.

## Overview

AuthServer is a modern, scalable authentication server that will serve as a central hub for managing user authentication, authorization, and identity services across multiple applications and services.

## Features (Planned)

- 🔐 **Secure Authentication**: Token-based authentication using JWT
- 👥 **User Management**: Complete user registration, login, and profile management
- 🔑 **Authorization**: Role-based and claims-based authorization
- 🔄 **Token Management**: Access token and refresh token handling
- 🛡️ **Security**: Industry-standard security practices and encryption
- 📊 **API Documentation**: Swagger/OpenAPI integration for easy API exploration
- 🌐 **Cross-Platform**: Built on .NET 8.0 for cross-platform compatibility

## Technology Stack

- **Framework**: ASP.NET Core 8.0 (LTS)
- **Language**: C# 12
- **API Style**: Minimal APIs
- **Documentation**: Swagger/OpenAPI 3.0
- **Authentication**: JWT Bearer (planned)
- **Database**: To be configured (SQL Server/PostgreSQL/SQLite)

## Prerequisites

Before you begin, ensure you have the following installed:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A code editor (Visual Studio 2022, Visual Studio Code, or JetBrains Rider)
- Git for version control

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Dapplesoft-AD/AuthServer.git
cd AuthServer
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Project

```bash
dotnet build
```

### 4. Run the Application

```bash
dotnet run --project src/AuthServer/AuthServer.csproj
```

The server will start and be available at:
- HTTPS: `https://localhost:7110`
- HTTP: `http://localhost:5004`

### 5. Access the API Documentation

Once the application is running in development mode, navigate to:
- Swagger UI: `https://localhost:7110/swagger`

## Project Structure

```
AuthServer/
├── src/
│   └── AuthServer/          # Main API project
│       ├── Program.cs       # Application entry point
│       ├── appsettings.json # Configuration settings
│       └── AuthServer.csproj
├── AuthServer.sln           # Solution file
├── .gitignore
├── LICENSE
└── README.md
```

## Development

### Building

```bash
# Build the entire solution
dotnet build

# Build in Release mode
dotnet build -c Release
```

### Running

```bash
# Run the application
dotnet run --project src/AuthServer/AuthServer.csproj

# Run with hot reload (watch mode)
dotnet watch --project src/AuthServer/AuthServer.csproj
```

### Testing

```bash
# Run all tests (when tests are added)
dotnet test
```

## Configuration

Configuration settings are stored in `appsettings.json` and `appsettings.Development.json`. Key settings include:

- **Logging**: Configure log levels for different components
- **AllowedHosts**: Specify allowed hosts for the application
- **Connection Strings**: Database connections (to be added)
- **JWT Settings**: Token configuration (to be added)

## API Endpoints

The API endpoints will be documented here as they are developed. Current sample endpoint:

- `GET /weatherforecast` - Sample weather forecast endpoint (to be replaced)

## Security Considerations

- Always use HTTPS in production
- Securely store connection strings and secrets (use User Secrets in development, Azure Key Vault or similar in production)
- Implement rate limiting for authentication endpoints
- Use strong password policies
- Implement account lockout mechanisms
- Enable CORS appropriately for your client applications

## Roadmap

- [ ] Set up authentication infrastructure (JWT)
- [ ] Implement user registration endpoint
- [ ] Implement login endpoint
- [ ] Add refresh token support
- [ ] Implement password reset functionality
- [ ] Add role-based authorization
- [ ] Integrate database (Entity Framework Core)
- [ ] Add email verification
- [ ] Implement two-factor authentication (2FA)
- [ ] Add OAuth2/OpenID Connect support
- [ ] Implement audit logging
- [ ] Add rate limiting
- [ ] Create comprehensive unit and integration tests

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For questions or support, please open an issue in the GitHub repository.

## Acknowledgments

- Built with ASP.NET Core
- Documentation powered by Swagger/OpenAPI
- Inspired by modern authentication best practices

---

**Status**: 🚧 Initial Setup - Active Development