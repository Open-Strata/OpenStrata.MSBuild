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

The OpenStrata.MSBuild solution provides specialized MSBuild SDKs for different types of Power Platform projects. **An OpenStrata solution consists of multiple projects, each with a specific purpose:**

### **Core MSBuild SDK**

- **`OpenStrata.MSBuild`** - Core MSBuild tasks and shared functionality

### **Default Solution Projects**

When you create an OpenStrata solution using `dotnet new os-dotnet`, you get these project types by default:

- **`OpenStrata.MSBuild.Solution`** - For Dataverse solution management
- **`OpenStrata.MSBuild.Package`** - For package creation and distribution  
- **`OpenStrata.MSBuild.Stratify`** - For solution layering and stratification
- **`OpenStrata.MSBuild.ConfigData`** - For configuration data management
- **`OpenStrata.MSBuild.Deployment`** - For deployment automation

### **Additional Project Types**

You can add these project types to your solution using individual templates:

- **`OpenStrata.MSBuild.Plugin`** - For Dynamics 365 plugin projects
- **`OpenStrata.MSBuild.PCF`** - For Power Apps Component Framework projects
- **`OpenStrata.MSBuild.PowerPages`** - For Power Pages website projects
- **`OpenStrata.MSBuild.DocumentTemplates`** - For document template projects

### **Development Tools**

- **`OpenStrata.Deployment.Sdk`** - Deployment SDK for advanced scenarios

> **💡 Solution Structure**: An OpenStrata solution orchestrates multiple specialized projects working together to deliver a complete Power Platform solution. Each project type has its own MSBuild SDK with specialized tooling for its specific purpose.

## 🚀 **Quick Start**

### **Prerequisites**

- **.NET SDK 6.0+** or **.NET Framework 4.7.2+**
- **Visual Studio 2022** or **VS Code** with C# extension
- **PowerShell 5.1+** (for development shortcuts)
- **Power Platform CLI** (for solution management)

### **Recommended: Using OpenStrata Templates**

The easiest way to get started is using the OpenStrata .NET templates from [OpenStrata.NET.New](https://github.com/Open-Strata/OpenStrata.NET.New):

1. **Install the OpenStrata templates:**
   ```bash
   dotnet new install OpenStrata.NET.Templates
   ```

2. **Create a new OpenStrata solution:**
   ```bash
   dotnet new os-dotnet -pn "YourPublisherName" -pp "yourprefix"
   ```

3. **Navigate to your solution and build:**
   ```bash
   cd YourPublisherName
   dotnet build
   ```

This creates a complete OpenStrata solution with the appropriate project structure for your chosen project type (Solution, Plugin, PCF, etc.).

### **Manual Installation (Advanced)**

If you need to manually add OpenStrata MSBuild to an existing project:

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

### **Project Types**

When you create an OpenStrata solution, you get **multiple coordinated projects**:

#### **Default Projects** (included automatically):
- **Solution** - Dataverse solution management
- **Package** - Package creation and distribution
- **Stratify** - Solution layering and stratification  
- **ConfigData** - Configuration data management
- **Deployment** - Deployment automation

#### **Additional Projects** (add as needed):
- **Plugin** - `dotnet new os-plugin` - Dynamics 365 plugins
- **PCF** - `dotnet new os-pcf` - Power Apps Component Framework controls
- **PowerPages** - `dotnet new os-powerpages` - Power Pages websites
- **DocumentTemplates** - `dotnet new os-documenttemplates` - Document templates

Each project type automatically includes the appropriate OpenStrata MSBuild SDK.

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