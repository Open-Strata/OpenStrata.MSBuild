# OpenStrata.MSBuild

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE.txt)
[![PowerPlatform](https://img.shields.io/badge/Power%20Platform-Compatible-orange.svg)](#)

> **🚀 NuGet-style package management for Microsoft Power Platform**

OpenStrata revolutionizes Power Platform development by bringing **package management and dependency chains** to the Power Platform ecosystem. Create reusable Power Platform capabilities as **Strati packages** that teams can share, discover, and depend on - just like NuGet packages for .NET.

## 🎯 What is OpenStrata?

**The Power Platform Package Revolution**: OpenStrata enables you to bundle complete Power Platform capabilities (solution components, configuration data, deployment tasks) into **referenceable NuGet packages**. Each Strati package can depend on other Strati packages, creating a rich ecosystem of reusable Power Platform components with **automatic dependency sequencing**.

**Before OpenStrata**: Copy-paste solutions, manual deployments, dependency coordination nightmares  
**With OpenStrata**: Package-based development, automated deployments, **zero-effort dependency management**

> **💡 New to OpenStrata?** Start with the [Quick Start](#-quick-start) below, then dive into [How It Works](HOW-IT-WORKS.md) to understand the technical innovation.

## ✨ Key Benefits

- **📦 Strati Packages** - Bundle complete Power Platform capabilities into NuGet packages
- **🔗 Dependency Management** - Reference other Strati packages, enabling true component reuse
- **⚡ Automatic Dependency Sequencing** - Solutions and components deploy in the correct order automatically
- **🚀 Automated Deployments** - Streamlined deployment workflows for Dataverse solutions
- **🏗️ Template-Based Development** - Start projects with proven patterns and structures
- **🔄 CI/CD Ready** - Works with GitHub Actions, Azure DevOps, and other platforms
- **🔐 Enterprise Security** - Automated assembly signing and security best practices

## 🚀 Quick Start

### 1. Install OpenStrata Templates

```bash
dotnet new install OpenStrata.NET.Templates
```

### 2. Create Your Power Platform Solution

```bash
# Create a new OpenStrata solution
dotnet new os-dotnet -pn "YourCompanyName" -pp "yourprefix"

# Navigate and build
cd YourCompanyName
dotnet build
```

This creates a complete solution with multiple coordinated projects designed to create **Strati packages**.

##  Project Types Explained

When you create an OpenStrata solution, you get **multiple coordinated projects** working together:

###  Core Innovation: Strati Packages

**Strati** is what makes OpenStrata revolutionary - it bundles your entire Power Platform capability into a **referenceable NuGet package**:

- **Complete Packaging** - Solution components, configuration data, deployment tasks all in one package
- **Dependency Support** - Your Strati package can reference other Strati packages
- **Version Management** - Semantic versioning and dependency resolution
- **Enterprise Distribution** - Share via internal NuGet feeds or public repositories

### Supporting Project Types

Each solution includes these projects that support Strati package creation:

**Default Projects** (included automatically):

- **Solution** - Manages your Dataverse solution components
- **ConfigData** - Handles configuration data and reference data
- **Deployment** - Automates deployment processes and tasks
- **Package** - Orchestrates the final package creation

**Optional Projects** (add as needed):

- **Plugin** - `dotnet new os-plugin` - For Dynamics 365 plugin development
- **PCF** - `dotnet new os-pcf` - For Power Apps Component Framework controls
- **PowerPages** - `dotnet new os-powerpages` - For Power Pages website development
- **DocumentTemplates** - `dotnet new os-documenttemplates` - For document template management

##  Development Workflow

### Building Your Solution

```bash
# Build your entire solution
dotnet build

# Build for release
dotnet build -c Release
```

### Creating Strati Packages

Your OpenStrata solution automatically creates Strati packages during build. These packages can then be:

- **Published to NuGet feeds** for team consumption
- **Referenced by other OpenStrata solutions** as dependencies
- **Deployed to Power Platform environments** with **automatic dependency sequencing**

**🎯 Automatic Dependency Sequencing**: OpenStrata analyzes your package dependencies and **automatically determines the correct installation order**. No more manual coordination - dependencies are deployed first, ensuring solutions install successfully every time.

##  Package Ecosystem

### Consuming Strati Packages

Reference other teams' Strati packages in your solution:

```xml
<!-- In your project file -->
<PackageReference Include="ContosoCore.Strati" Version="2.1.0" />
<PackageReference Include="ContosoSecurity.Strati" Version="1.5.0" />
```

### Publishing Packages

Share your Strati packages with your organization:

```bash
# Publish to your internal feed
dotnet nuget push YourPackage.Strati.1.0.0.nupkg --source https://your-internal-feed
```

### 🎯 Automatic Dependency Sequencing Example

When you deploy a package with dependencies, OpenStrata automatically determines the correct order:

```
Your Package Dependencies:
MyApp.Strati v1.0 
├── Security.Strati v2.1 
│   └── Core.Strati v1.5
└── Utilities.Strati v3.0
    └── Core.Strati v1.5

Automatic Deployment Order:
1. Core.Strati v1.5      ← Deployed first (foundation)
2. Security.Strati v2.1  ← Deployed second  
3. Utilities.Strati v3.0 ← Deployed third
4. MyApp.Strati v1.0     ← Deployed last (depends on all)
```

**No manual coordination needed** - OpenStrata ensures solutions install in the correct order every time!

> **🔍 Want to understand how this works under the hood?**  
> **See [How It Works](HOW-IT-WORKS.md)** for a detailed technical deep-dive into OpenStrata's architecture, MEF composition, and automatic dependency sequencing implementation.

## 📚 Documentation & Resources

### For Power Platform Developers

- **Templates Documentation** - [OpenStrata.NET.Templates Repository](https://github.com/Open-Strata/OpenStrata.NET.Templates)
- **Getting Started Guide** - See template documentation for detailed walkthroughs
- **Best Practices** - [Community Wiki](https://github.com/Open-Strata/OpenStrata.MSBuild/wiki)

### Technical Deep Dive

- **📋 [How It Works](HOW-IT-WORKS.md)** - **★ MUST READ** - Technical architecture, MEF composition, and dependency sequencing implementation

### For Contributors & Builders

- **[Building Guide](BUILDING.md)** - How to build and contribute to OpenStrata.MSBuild
- **[Contributing Guidelines](CONTRIBUTING.md)** - Contribution process and standards
- **[Security Policy](SECURITY.md)** - Security practices and vulnerability reporting

##  Support & Community

- ** Issues**: [Report bugs or request features](https://github.com/Open-Strata/OpenStrata.MSBuild/issues)
- ** Discussions**: [Ask questions and share ideas](https://github.com/Open-Strata/OpenStrata.MSBuild/discussions)
- ** Documentation**: [Community Wiki](https://github.com/Open-Strata/OpenStrata.MSBuild/wiki)

##  Project Status

OpenStrata is currently a **Proof-of-Concept** initiative. We welcome you to:

-  Evaluate OpenStrata capabilities
-  Provide feedback and suggestions  
-  Participate in community discussions
-  Use with awareness this is active development

##  License

Licensed under the Apache 2.0 License - see [LICENSE.txt](LICENSE.txt) for details.

---

**🌟 Join the Power Platform Package Revolution with OpenStrata**

**Ready to transform your Power Platform development?** Explore the [technical architecture](HOW-IT-WORKS.md) that makes it all possible.
