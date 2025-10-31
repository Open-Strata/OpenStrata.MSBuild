# OpenStrata.MSBuild# OpenStrata.MSBuild



[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE.txt)[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE.txt)

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()

[![PowerPlatform](https://img.shields.io/badge/Power%20Platform-Compatible-orange.svg)]()[![PowerPlatform](https://img.shields.io/badge/Power%20Platform-Compatible-orange.svg)]()



> **🚀 NuGet-style package management for Microsoft Power Platform**> **🚀 MSBuild SDKs for the Microsoft Power Platform ecosystem**



OpenStrata revolutionizes Power Platform development by bringing **package management and dependency chains** to the Power Platform ecosystem. Create reusable Power Platform capabilities as **Strati packages** that teams can share, discover, and depend on - just like NuGet packages for .NET.OpenStrata.MSBuild provides a comprehensive suite of MSBuild SDKs that standardize and automate the development workflow for Microsoft Power Platform solutions. These tools enable developers to build, package, deploy, and manage Power Platform components using familiar .NET development practices.



## 🎯 **What is OpenStrata?**## 🎯 **Project Vision**



**The Power Platform Package Revolution**: OpenStrata enables you to bundle complete Power Platform capabilities (solution components, configuration data, deployment tasks) into **referenceable NuGet packages**. Each Strati package can depend on other Strati packages, creating a rich ecosystem of reusable Power Platform components.The OpenStrata Initiative revolutionizes Power Platform development by bringing **NuGet-style package management** to the Power Platform ecosystem. At its core is the **Strati project type** - the foundational innovation that bundles Power Platform solution components, configuration data, deployment tasks, and dependencies into referenceable NuGet packages.



**Before OpenStrata**: Copy-paste solutions, manual deployments, no dependency management  **The Strati Innovation**: Just as NuGet transformed .NET development by enabling code reuse through package dependencies, OpenStrata enables Power Platform teams to create, share, and depend on packaged Power Platform capabilities. Each Strati package can reference other Strati packages, creating a rich ecosystem of reusable Power Platform components.

**With OpenStrata**: Package-based development, automated deployments, dependency resolution

## ✨ **Key Features**

## ✨ **Key Benefits**

- **� Strati Package System** - Bundle complete Power Platform capabilities into referenceable NuGet packages

- **📦 Strati Packages** - Bundle complete Power Platform capabilities into NuGet packages- **� Package Dependencies** - Reference other Strati packages, enabling true component reuse

- **🔗 Dependency Management** - Reference other Strati packages, enabling true component reuse- **🔧 Automated Build Processes** - Standardized MSBuild targets for Power Platform projects

- **🚀 Automated Deployments** - Streamlined deployment workflows for Dataverse solutions- **🚀 Deployment Automation** - Streamlined deployment workflows for Dataverse solutions

- **🏗️ Template-Based Development** - Start projects with proven patterns and structures- **🔐 Security & Signing** - Automated assembly signing and security best practices

- **🔄 CI/CD Ready** - Works with GitHub Actions, Azure DevOps, and other platforms- **🏗️ Project Templates** - Scaffolding for common Power Platform development patterns

- **🔐 Enterprise Security** - Automated assembly signing and security best practices- **🔄 CI/CD Integration** - Ready for GitHub Actions, Azure DevOps, and other CI/CD platforms



## 🚀 **Quick Start**## 🏛️ **Architecture Overview**



### **1. Install OpenStrata Templates**The OpenStrata.MSBuild solution provides specialized MSBuild SDKs for different types of Power Platform projects. **An OpenStrata solution consists of multiple projects, each with a specific purpose:**



```bash### **Core MSBuild SDK**

dotnet new install OpenStrata.NET.Templates

```- **`OpenStrata.MSBuild`** - Core MSBuild tasks and shared functionality



### **2. Create Your Power Platform Solution**### **Default Solution Projects**



```bashWhen you create an OpenStrata solution using `dotnet new os-dotnet`, you get these project types by default:

# Create a new OpenStrata solution

dotnet new os-dotnet -pn "YourCompanyName" -pp "yourprefix"- **`OpenStrata.MSBuild.Solution`** - For Dataverse solution management

- **`OpenStrata.MSBuild.Package`** - For package creation and distribution  

# Navigate and build- **`OpenStrata.MSBuild.Stratify`** - **THE CORE INNOVATION** - Bundles Power Platform capabilities into referenceable NuGet packages with dependency support

cd YourCompanyName- **`OpenStrata.MSBuild.ConfigData`** - For configuration data management

dotnet build- **`OpenStrata.MSBuild.Deployment`** - For deployment automation

```

### **Additional Project Types**

This creates a complete solution with multiple coordinated projects designed to create **Strati packages**.

You can add these project types to your solution using individual templates:

## 🎯 **Project Types Explained**

- **`OpenStrata.MSBuild.Plugin`** - For Dynamics 365 plugin projects

When you create an OpenStrata solution, you get **multiple coordinated projects** working together:- **`OpenStrata.MSBuild.PCF`** - For Power Apps Component Framework projects

- **`OpenStrata.MSBuild.PowerPages`** - For Power Pages website projects

### **🌟 Core Innovation: Strati Packages**- **`OpenStrata.MSBuild.DocumentTemplates`** - For document template projects



**Strati** is what makes OpenStrata revolutionary - it bundles your entire Power Platform capability into a **referenceable NuGet package**:### **Development Tools**



- **Complete Packaging** - Solution components, configuration data, deployment tasks all in one package- **`OpenStrata.Deployment.Sdk`** - Deployment SDK for advanced scenarios

- **Dependency Support** - Your Strati package can reference other Strati packages

- **Version Management** - Semantic versioning and dependency resolution> **💡 Solution Structure**: An OpenStrata solution orchestrates multiple specialized projects working together to deliver a complete Power Platform solution. Each project type has its own MSBuild SDK with specialized tooling for its specific purpose.

- **Enterprise Distribution** - Share via internal NuGet feeds or public repositories

## 🚀 **Quick Start**

### **Supporting Project Types**

### **Prerequisites**

Each solution includes these projects that support Strati package creation:

- **.NET SDK 6.0+** or **.NET Framework 4.7.2+**

**Default Projects** (included automatically):- **Visual Studio 2022** or **VS Code** with C# extension

- **PowerShell 5.1+** (for development shortcuts)

- **Solution** - Manages your Dataverse solution components- **Power Platform CLI** (for solution management)

- **ConfigData** - Handles configuration data and reference data

- **Deployment** - Automates deployment processes and tasks### **Recommended: Using OpenStrata Templates**

- **Package** - Orchestrates the final package creation

The easiest way to get started is using the OpenStrata .NET templates from [OpenStrata.NET.New](https://github.com/Open-Strata/OpenStrata.NET.New):

**Optional Projects** (add as needed):

1. **Install the OpenStrata templates:**

- **Plugin** - `dotnet new os-plugin` - For Dynamics 365 plugin development   ```bash

- **PCF** - `dotnet new os-pcf` - For Power Apps Component Framework controls   dotnet new install OpenStrata.NET.Templates

- **PowerPages** - `dotnet new os-powerpages` - For Power Pages website development   ```

- **DocumentTemplates** - `dotnet new os-documenttemplates` - For document template management

2. **Create a new OpenStrata solution:**

## 🔄 **Development Workflow**   ```bash

   dotnet new os-dotnet -pn "YourPublisherName" -pp "yourprefix"

### **Building Your Solution**   ```



```bash3. **Navigate to your solution and build:**

# Build your entire solution   ```bash

dotnet build   cd YourPublisherName

   dotnet build

# Build for release   ```

dotnet build -c Release

```This creates a complete OpenStrata solution with the appropriate project structure for your chosen project type (Solution, Plugin, PCF, etc.).



### **Creating Strati Packages**### **Project Types**



Your OpenStrata solution automatically creates Strati packages during build. These packages can then be:When you create an OpenStrata solution, you get **multiple coordinated projects** working together to create **reusable Strati packages**:



- **Published to NuGet feeds** for team consumption#### **🎯 Core Innovation: Strati Projects**

- **Referenced by other OpenStrata solutions** as dependencies

- **Deployed to Power Platform environments** with full dependency resolution**Strati** is the foundational project type that makes OpenStrata revolutionary. Each Strati project:

- **Bundles complete Power Platform capabilities** (solution components, configuration data, deployment tasks)

### **Adding Project Types**- **Creates referenceable NuGet packages** that other projects can depend on

- **Enables dependency chains** - your Strati package can reference other Strati packages

Start with the default solution, then add specific project types as needed:- **Brings package management benefits** to Power Platform development



```bash#### **Default Projects** (included automatically)

# Add a plugin project

dotnet new os-plugin -n "MyCustomPlugin"- **Solution** - Dataverse solution management

- **Package** - Package creation and distribution

# Add a PCF control project  - **Stratify** - **THE CORE** - Bundles everything into referenceable NuGet packages

dotnet new os-pcf -n "MyCustomControl"- **ConfigData** - Configuration data management

- **Deployment** - Deployment automation

# Add a Power Pages project

dotnet new os-powerpages -n "MyPortalSite"#### **Additional Projects** (add as needed)

```

- **Plugin** - `dotnet new os-plugin` - Dynamics 365 plugins

## 🏗️ **Example: Creating a Reusable Component**- **PCF** - `dotnet new os-pcf` - Power Apps Component Framework controls

- **PowerPages** - `dotnet new os-powerpages` - Power Pages websites

```bash- **DocumentTemplates** - `dotnet new os-documenttemplates` - Document templates

# 1. Create base solution

dotnet new os-dotnet -pn "ContosoFinance" -pp "cfi"Each project type automatically includes the appropriate OpenStrata MSBuild SDK.



# 2. Add specific capabilities## 📁 **Project Structure**

cd ContosoFinance

dotnet new os-plugin -n "FinanceCalculations"```

dotnet new os-pcf -n "FinanceDashboard"OpenStrata.MSBuild/

├── src/

# 3. Build Strati package│   ├── MSBuild/                    # Core MSBuild SDKs

dotnet build -c Release│   │   ├── MSBuild/                # Core functionality

│   │   ├── MSBuild.Solution/       # Dataverse solutions

# 4. Other teams can now reference your package│   │   ├── MSBuild.Plugin/         # Dynamics 365 plugins

# In their project: <PackageReference Include="ContosoFinance.Strati" Version="1.0.0" />│   │   ├── MSBuild.ConfigData/     # Configuration data

```│   │   ├── MSBuild.PCF/            # Power Apps components

│   │   ├── MSBuild.PowerPages/     # Power Pages sites

## 📦 **Package Ecosystem**│   │   ├── MSBuild.Package/        # Package management

│   │   ├── MSBuild.Deployment/     # Deployment tools

### **Consuming Strati Packages**│   │   └── MSBuild.Stratify/       # Core Innovation - Strati packages

│   ├── Deployment/

Reference other teams' Strati packages in your solution:│   │   └── Deployment.Sdk/         # Deployment SDK

│   ├── Shared/                     # Shared libraries

```xml│   └── shortcuts.ps1               # Development utilities

<!-- In your project file -->├── CONTRIBUTING.md                 # Contributing guidelines

<PackageReference Include="ContosoCore.Strati" Version="2.1.0" />├── CODE_OF_CONDUCT.md             # Code of conduct

<PackageReference Include="ContosoSecurity.Strati" Version="1.5.0" />├── SECURITY.md                    # Security policy

```└── LICENSE.txt                    # Apache 2.0 license

```

### **Publishing Packages**

## 🛠️ **Development**

Share your Strati packages with your organization:

### **Development Environment Setup**

```bash

# Publish to your internal feed1. **Prerequisites:**

dotnet nuget push YourPackage.Strati.1.0.0.nupkg --source https://your-internal-feed   - Visual Studio 2022 or VS Code

```   - .NET SDK 6.0+

   - PowerShell 5.1+

## 📚 **Documentation & Resources**

2. **Clone and setup:**

### **For Power Platform Developers**   ```bash

   git clone https://github.com/Open-Strata/OpenStrata.MSBuild.git

- **Templates Documentation** - [OpenStrata.NET.Templates Repository](https://github.com/Open-Strata/OpenStrata.NET.Templates)   cd OpenStrata.MSBuild

- **Getting Started Guide** - See template documentation for detailed walkthroughs   ```

- **Best Practices** - [Community Wiki](https://github.com/Open-Strata/OpenStrata.MSBuild/wiki)

3. **Load development shortcuts:**

### **For Contributors & Builders**   ```powershell

   . .\src\shortcuts.ps1

- **[Building Guide](BUILDING.md)** - How to build and contribute to OpenStrata.MSBuild   ```

- **[Contributing Guidelines](CONTRIBUTING.md)** - Contribution process and standards

- **[Security Policy](SECURITY.md)** - Security practices and vulnerability reporting### **Available Commands**



## 🆘 **Support & Community**```powershell

# Restore NuGet packages

- **🐛 Issues**: [Report bugs or request features](https://github.com/Open-Strata/OpenStrata.MSBuild/issues)restore

- **💬 Discussions**: [Ask questions and share ideas](https://github.com/Open-Strata/OpenStrata.MSBuild/discussions)

- **📖 Documentation**: [Community Wiki](https://github.com/Open-Strata/OpenStrata.MSBuild/wiki)# Build all projects

build

## ⚠️ **Project Status**

# Publish to NuGet (requires NUGET_API_KEY)

OpenStrata is currently a **Proof-of-Concept** initiative. We welcome you to:push2nuget

```

- ✅ Evaluate OpenStrata capabilities

- ✅ Provide feedback and suggestions  ### **Strong Name Key Management**

- ✅ Participate in community discussions

- ⚠️ Use with awareness this is active developmentFor security reasons, strong name key files (.snk) are not included in the repository. They will be auto-generated during the first build, or you can create them manually:



## 📄 **License**```powershell

# Generate strong name key

Licensed under the Apache 2.0 License - see [LICENSE.txt](LICENSE.txt) for details.sn -k YourAssembly.snk

```

---

See [SECURITY-STRONGNAME.md](SECURITY-STRONGNAME.md) for detailed information.

**🌟 Join the Power Platform Package Revolution with OpenStrata**
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