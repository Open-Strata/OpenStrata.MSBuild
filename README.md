# OpenStrata.MSBuild

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE.txt)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![PowerPlatform](https://img.shields.io/badge/Power%20Platform-Compatible-orange.svg)]()

> **🚀 MSBuild SDKs for the Microsoft Power Platform ecosystem**

OpenStrata.MSBuild provides a comprehensive suite of MSBuild SDKs that standardize and automate the development workflow for Microsoft Power Platform solutions. These tools enable developers to build, package, deploy, and manage Power Platform components using familiar .NET development practices.

## 🎯 **Project Vision**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform capabilities.

## ✨ **Key Features**

- **🔧 Automated Build Processes** - Standardized MSBuild targets for Power Platform projects
- **📦 Package Management** - NuGet-based distribution of Power Platform components
- **🚀 Deployment Automation** - Streamlined deployment workflows for Dataverse solutions
- **🔐 Security & Signing** - Automated assembly signing and security best practices
- **🏗️ Project Templates** - Scaffolding for common Power Platform development patterns
- **🔄 CI/CD Integration** - Ready for GitHub Actions, Azure DevOps, and other CI/CD platforms

## 🏛️ **Architecture Overview**

The solution is organized into several specialized MSBuild SDKs:

### **Core MSBuild SDK**
- **`OpenStrata.MSBuild`** - Core MSBuild tasks and shared functionality

### **Power Platform Components**
- **`OpenStrata.MSBuild.Solution`** - Dataverse solution management and packaging
- **`OpenStrata.MSBuild.Plugin`** - Dynamics 365 plugin development and deployment
- **`OpenStrata.MSBuild.ConfigData`** - Configuration data management
- **`OpenStrata.MSBuild.PCF`** - Power Apps Component Framework (PCF) controls
- **`OpenStrata.MSBuild.PowerPages`** - Power Pages website development
- **`OpenStrata.MSBuild.DocumentTemplates`** - Document template handling

### **Packaging & Deployment**
- **`OpenStrata.MSBuild.Package`** - Package creation and distribution
- **`OpenStrata.MSBuild.Deployment`** - Deployment automation and workflows
- **`OpenStrata.MSBuild.Stratify`** - Solution layering and stratification

### **Development Tools**
- **`OpenStrata.Deployment.Sdk`** - Deployment SDK for Power Platform solutions

## 🚀 **Quick Start**

### **Prerequisites**

- **.NET SDK 6.0+** or **.NET Framework 4.7.2+**
- **Visual Studio 2022** or **VS Code** with C# extension
- **PowerShell 5.1+** (for development shortcuts)
- **Power Platform CLI** (for solution management)

### **Installation**

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

3. **Build the solution:**
   ```powershell
   # Load development shortcuts
   . .\src\shortcuts.ps1
   
   # Restore and build
   restore
   build
   ```

### **Using in Your Project**

Add OpenStrata MSBuild packages to your Power Platform projects:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net472</TargetFramework>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="OpenStrata.MSBuild.Solution" Version="1.*" />
    <PackageReference Include="OpenStrata.MSBuild.Plugin" Version="1.*" />
  </ItemGroup>
</Project>
```

## 📁 **Project Structure**

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
│   │   └── MSBuild.Stratify/       # Solution layering
│   ├── Deployment/
│   │   └── Deployment.Sdk/         # Deployment SDK
│   ├── Shared/                     # Shared libraries
│   └── shortcuts.ps1               # Development utilities
├── CONTRIBUTING.md                 # Contributing guidelines
├── CODE_OF_CONDUCT.md             # Code of conduct
├── SECURITY.md                    # Security policy
└── LICENSE.txt                    # Apache 2.0 license
```

## 🛠️ **Development**

### **Development Environment Setup**

1. **Prerequisites:**
   - Visual Studio 2022 or VS Code
   - .NET SDK 6.0+
   - PowerShell 5.1+

2. **Clone and setup:**
   ```bash
   git clone https://github.com/Open-Strata/OpenStrata.MSBuild.git
   cd OpenStrata.MSBuild
   ```

3. **Load development shortcuts:**
   ```powershell
   . .\src\shortcuts.ps1
   ```

### **Available Commands**

```powershell
# Restore NuGet packages
restore

# Build all projects
build

# Publish to NuGet (requires NUGET_API_KEY)
push2nuget
```

### **Strong Name Key Management**

For security reasons, strong name key files (.snk) are not included in the repository. They will be auto-generated during the first build, or you can create them manually:

```powershell
# Generate strong name key
sn -k YourAssembly.snk
```

See [SECURITY-STRONGNAME.md](SECURITY-STRONGNAME.md) for detailed information.

## 📚 **Documentation**

- **[Contributing Guidelines](CONTRIBUTING.md)** - How to contribute to the project
- **[Code of Conduct](CODE_OF_CONDUCT.md)** - Community standards
- **[Security Policy](SECURITY.md)** - Security practices and reporting
- **[Strong Name Keys](SECURITY-STRONGNAME.md)** - Assembly signing documentation

## 🤝 **Contributing**

We welcome contributions from the community! Please read our [Contributing Guidelines](CONTRIBUTING.md) before submitting pull requests.

### **Quick Contribution Steps**

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 **License**

This project is licensed under the Apache 2.0 License - see the [LICENSE.txt](LICENSE.txt) file for details.

## 🆘 **Support**

- **Issues**: [GitHub Issues](https://github.com/Open-Strata/OpenStrata.MSBuild/issues)
- **Discussions**: [GitHub Discussions](https://github.com/Open-Strata/OpenStrata.MSBuild/discussions)
- **Documentation**: [Wiki](https://github.com/Open-Strata/OpenStrata.MSBuild/wiki)

## 🏷️ **Project Status**

> ⚠️ **Note**: The OpenStrata initiative is currently a Proof-of-Concept effort. We welcome and invite you to evaluate OpenStrata products, provide feedback, and make suggestions or requests. Please be mindful this is an active development effort. Not all features are fully operational.

---

**Made with ❤️ by the OpenStrata Initiative**