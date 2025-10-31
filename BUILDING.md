# Building OpenStrata.MSBuild

This guide is for **contributors and developers** who want to build, modify, or contribute to the OpenStrata.MSBuild repository itself.

> **👥 For Users**: If you want to **use** OpenStrata to build Power Platform solutions, see the main [README.md](README.md) instead.

## 🏗️ **Repository Structure**

```
OpenStrata.MSBuild/
├── src/
│   ├── MSBuild/                    # Core MSBuild SDKs
│   │   ├── MSBuild/                # Core functionality
│   │   ├── MSBuild.Solution/       # Dataverse solutions
│   │   ├── MSBuild.Plugin/         # Dynamics 365 plugins
│   │   ├── MSBuild.ConfigData/     # Configuration data
│   │   ├── MSBuild.PCF/            # Power Apps components
│   │   ├── MSBuild.PowerPages/     # Power Pages sites
│   │   ├── MSBuild.Package/        # Package management
│   │   ├── MSBuild.Deployment/     # Deployment tools
│   │   └── MSBuild.Stratify/       # Core Innovation - Strati packages
│   ├── Deployment/
│   │   └── Deployment.Sdk/         # Deployment SDK
│   ├── Shared/                     # Shared libraries
│   └── shortcuts.ps1               # Development utilities
├── CONTRIBUTING.md                 # Contributing guidelines
├── CODE_OF_CONDUCT.md             # Code of conduct
├── SECURITY.md                    # Security policy
└── LICENSE.txt                    # Apache 2.0 license
```

## 🛠️ **Development Environment Setup**

### **Prerequisites**

- Visual Studio 2022 or VS Code
- .NET SDK 6.0+
- PowerShell 5.1+

### **Clone and Setup**

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Open-Strata/OpenStrata.MSBuild.git
   cd OpenStrata.MSBuild
   ```

2. **Set up environment variables:**
   ```powershell
   # Optional: Set local package feed
   $env:OPENSTRATA_LOCAL_PACKAGE_FEED = "C:\YourLocalPackages"
   
   # For publishing: Set NuGet API key
   $env:NUGET_API_KEY = "your-nuget-api-key"
   ```

3. **Load development shortcuts:**
   ```powershell
   . .\src\shortcuts.ps1
   ```

## 🔨 **Building the Solution**

### **Available Commands**

The `shortcuts.ps1` file provides convenient commands for development:

```powershell
# Restore NuGet packages for all solutions
restore

# Build all solutions
build

# Publish packages to NuGet (requires NUGET_API_KEY)
push2nuget

# Stop all dotnet processes (useful for cleanup)
killdotnet
```

### **Manual Build Process**

If you prefer manual commands:

```powershell
# Restore packages
dotnet restore src/OpenStrata.MSBuild.sln

# Build solution
dotnet build src/OpenStrata.MSBuild.sln

# Build for release
dotnet build src/OpenStrata.MSBuild.sln -c Release
```

## 🔐 **Strong Name Key Management**

For security reasons, strong name key files (.snk) are **not included** in the repository. They will be auto-generated during the first build, or you can create them manually:

```powershell
# Generate strong name key manually
sn -k YourAssembly.snk
```

**Important**: 
- Never commit .snk files to source control
- The build process automatically generates keys if they don't exist
- See [SECURITY-STRONGNAME.md](SECURITY-STRONGNAME.md) for detailed information

## 📦 **Package Publishing**

### **Local Testing**

For local testing, set up a local package feed:

```powershell
# Set local package feed environment variable
$env:OPENSTRATA_LOCAL_PACKAGE_FEED = "C:\YourLocalPackages"

# Build and publish locally
build
```

### **Publishing to NuGet**

To publish packages to NuGet.org:

```powershell
# Set your NuGet API key
$env:NUGET_API_KEY = "your-nuget-api-key"

# Build and publish
push2nuget
```

Or specify parameters directly:

```powershell
push2nuget -key "your-api-key" -source "https://api.nuget.org/v3/index.json"
```

## 🧪 **Testing**

### **Unit Tests**

```powershell
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### **Integration Testing**

Test the templates and SDKs:

```powershell
# Install templates locally for testing
dotnet new install ./path/to/template/package

# Create test project
dotnet new os-dotnet -pn "TestPublisher" -pp "test"

# Build test project
cd TestPublisher
dotnet build
```

## 📝 **Development Guidelines**

### **Code Standards**

- Follow existing code style and patterns
- Add XML documentation for public APIs
- Include unit tests for new functionality
- Update documentation for breaking changes

### **Commit Guidelines**

- Use conventional commit messages
- Keep commits focused and atomic
- Reference issues in commit messages
- Update CHANGELOG.md for user-facing changes

### **Pull Request Process**

1. Fork the repository
2. Create a feature branch from `main`
3. Make your changes following the guidelines
4. Add or update tests as needed
5. Update documentation if required
6. Submit a pull request with a clear description

## 🔍 **Debugging and Troubleshooting**

### **Common Issues**

**Build Failures:**
- Ensure all prerequisites are installed
- Try cleaning: `dotnet clean` then `dotnet restore`
- Check environment variables are set correctly

**NuGet Publishing Issues:**
- Verify NUGET_API_KEY is set correctly
- Check package version conflicts
- Ensure you have publishing permissions

**Template Issues:**
- Clear template cache: `dotnet new uninstall OpenStrata.NET.Templates`
- Reinstall templates: `dotnet new install OpenStrata.NET.Templates`

### **Getting Help**

- Check [GitHub Issues](https://github.com/Open-Strata/OpenStrata.MSBuild/issues) for known problems
- Review [Contributing Guidelines](CONTRIBUTING.md) for detailed processes
- Join [GitHub Discussions](https://github.com/Open-Strata/OpenStrata.MSBuild/discussions) for questions

## 📚 **Additional Resources**

- **[Contributing Guidelines](CONTRIBUTING.md)** - Detailed contribution process
- **[Security Policy](SECURITY.md)** - Security practices and reporting
- **[Strong Name Keys](SECURITY-STRONGNAME.md)** - Assembly signing documentation
- **[Code of Conduct](CODE_OF_CONDUCT.md)** - Community standards

---

**Building OpenStrata.MSBuild with ❤️**