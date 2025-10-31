# Contributing to OpenStrata.MSBuild

We welcome contributions to the OpenStrata.MSBuild project! This document outlines how to contribute effectively to our open-source MSBuild SDK for the Microsoft Power Platform ecosystem.

## 🎯 **Code of Conduct**

Please read and follow our [Code of Conduct](CODE_OF_CONDUCT.md) to ensure a welcoming environment for all contributors.

## 🚀 **Getting Started**

### **Prerequisites**

- .NET SDK 6.0+ or .NET Framework 4.7.2+
- Visual Studio 2022, VS Code, or Rider
- PowerShell 5.1+
- Git
- Basic understanding of MSBuild and Power Platform

### **Development Environment Setup**

1. **Fork and clone the repository:**

   ```bash
   git clone https://github.com/YOUR_USERNAME/OpenStrata.MSBuild.git
   cd OpenStrata.MSBuild
   ```

2. **Set up development environment:**

   ```powershell
   # Load development shortcuts
   . .\src\shortcuts.ps1
   
   # Restore packages and build
   restore
   build
   ```

3. **Verify your setup:**

   ```powershell
   # Run tests (if available)
   dotnet test
   ```

### **Testing Your Changes**

When contributing to OpenStrata.MSBuild, you'll need to test your MSBuild SDK changes:

1. **Build the MSBuild packages locally:**

   ```powershell
   # Build all packages
   build
   
   # Packages will be available in your local package feed
   # Default: $(TEMP)\OpenStrata\Packages or $env:OPENSTRATA_LOCAL_PACKAGE_FEED
   ```

2. **Create test projects to validate your changes:**

   ```xml
   <!-- Create a test project that references your local MSBuild packages -->
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net472</TargetFramework>
     </PropertyGroup>
     
     <ItemGroup>
       <!-- Reference your locally built packages -->
       <PackageReference Include="OpenStrata.MSBuild.Solution" Version="1.*" />
     </ItemGroup>
   </Project>
   ```

3. **Test your MSBuild targets and tasks:**

   ```powershell
   # Test specific MSBuild targets
   dotnet msbuild YourTestProject.csproj -t:YourNewTarget -v:detailed
   
   # Verify your changes work as expected
   dotnet build YourTestProject.csproj
   ```

## 📋 **How to Contribute**

### **Types of Contributions**

We welcome various types of contributions:

- **🐛 Bug Reports** - Report issues and bugs
- **✨ Feature Requests** - Suggest new features or improvements
- **📝 Documentation** - Improve documentation and examples
- **🔧 Code Contributions** - Submit bug fixes or new features
- **🧪 Testing** - Add or improve tests
- **🎨 Examples** - Provide usage examples and samples

### **Contribution Workflow**

1. **Check existing issues** - Look for existing issues or create a new one
2. **Fork the repository** - Create your own fork
3. **Create a feature branch** - Use descriptive branch names
4. **Make your changes** - Follow our coding standards
5. **Test your changes** - Ensure all tests pass
6. **Submit a pull request** - Provide clear description of changes

## 🌟 **Contribution Guidelines**

### **Branch Naming Convention**

Use descriptive branch names that indicate the type and scope of changes:

```
feature/add-pcf-component-support
bugfix/fix-solution-packaging-issue
docs/update-deployment-guide
refactor/simplify-build-targets
```

### **Commit Message Format**

Follow conventional commit format for clear commit history:

```
<type>(<scope>): <description>

[optional body]

[optional footer(s)]
```

**Types:**
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, etc.)
- `refactor`: Code refactoring
- `test`: Adding or modifying tests
- `chore`: Maintenance tasks

**Examples:**
```
feat(solution): add automatic solution version increment
fix(plugin): resolve assembly loading issue in deployment
docs(readme): add quick start guide for PCF components
```

### **Pull Request Guidelines**

#### **Before Submitting**

- [ ] Code builds successfully
- [ ] All tests pass
- [ ] Documentation is updated if needed
- [ ] Changes are backwards compatible (or breaking changes are documented)
- [ ] Code follows project conventions

#### **Pull Request Description**

Use the following template for pull requests:

```markdown
## Description
Brief description of changes made.

## Type of Change
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] Documentation update

## Testing
- [ ] Unit tests added/updated
- [ ] Integration tests added/updated
- [ ] Manual testing completed

## Checklist
- [ ] Code follows the project's coding standards
- [ ] Self-review completed
- [ ] Documentation updated
- [ ] No breaking changes (or documented if necessary)
```

## 🏗️ **Development Guidelines**

### **Project Structure**

```
src/
├── MSBuild/                    # Core MSBuild SDKs
│   ├── MSBuild/                # Core MSBuild functionality
│   ├── MSBuild.*/              # Specialized MSBuild SDKs
│   └── MSBuild.Shared/         # Shared MSBuild components
├── Deployment/                 # Deployment tools
├── Shared/                     # Shared libraries
└── shortcuts.ps1              # Development utilities
```

### **Understanding OpenStrata Solution Structure**

When contributing to OpenStrata.MSBuild, it's important to understand that an **OpenStrata solution contains multiple coordinated projects**:

#### **Default Projects** (created automatically)

- **Solution projects** - Use `OpenStrata.MSBuild.Solution` for Dataverse solution management
- **Package projects** - Use `OpenStrata.MSBuild.Package` for distribution and packaging
- **Stratify projects** - Use `OpenStrata.MSBuild.Stratify` for solution layering and stratification
- **ConfigData projects** - Use `OpenStrata.MSBuild.ConfigData` for configuration data management
- **Deployment projects** - Use `OpenStrata.MSBuild.Deployment` for deployment automation

#### **Additional Projects** (added as needed)

- **Plugin projects** - Use `OpenStrata.MSBuild.Plugin` for Dynamics 365 plugin development
- **PCF projects** - Use `OpenStrata.MSBuild.PCF` for Power Apps Component Framework controls
- **PowerPages projects** - Use `OpenStrata.MSBuild.PowerPages` for Power Pages website development
- **DocumentTemplate projects** - Use `OpenStrata.MSBuild.DocumentTemplates` for document template management

**For Contributors**: When working on a specific MSBuild SDK (e.g., MSBuild.Solution), understand how it fits within the overall solution structure. Each SDK should focus on its specific capability while supporting coordination with other project types in the same solution.

### **Coding Standards**

#### **C# Code Style**

- Use **C# 8.0+** language features where appropriate
- Follow **Microsoft C# Coding Conventions**
- Use **meaningful variable and method names**
- Add **XML documentation** for public APIs
- Use **async/await** for asynchronous operations

#### **MSBuild Targets & Props**

- Use **clear, descriptive target names**
- Add **comprehensive comments** for complex logic
- Follow **MSBuild best practices** for performance
- Use **conditional execution** where appropriate
- **Validate inputs** and provide clear error messages

#### **File Organization**

- **One class per file** (with exceptions for small helper classes)
- Use **appropriate namespaces** that match folder structure
- Keep **related functionality together**
- Separate **concerns appropriately**

### **Testing Standards**

- **Unit tests** for all public methods
- **Integration tests** for MSBuild targets
- **End-to-end tests** for complete workflows
- Use **descriptive test names** that explain the scenario
- Follow **AAA pattern** (Arrange, Act, Assert)

### **Documentation Standards**

- **Update README.md** for significant changes
- **Add XML documentation** for public APIs
- **Include code examples** for new features
- **Update relevant project documentation**

## 🧪 **Testing Your Changes**

### **Running Tests**

```powershell
# Run all tests
dotnet test

# Run specific test project
dotnet test src/MSBuild/MSBuild.Tests/

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### **Manual Testing**

Create test scenarios for your changes:

1. **Create a test Power Platform project**
2. **Reference your modified MSBuild SDK**
3. **Test the complete build/deploy workflow**
4. **Verify backwards compatibility**

### **Testing MSBuild Targets**

Test MSBuild targets in isolation:

```powershell
# Test specific target
dotnet msbuild YourTestProject.csproj -t:YourTargetName -v:detailed

# Test with different configurations
dotnet msbuild YourTestProject.csproj -p:Configuration=Release
```

## 📚 **Documentation Contributions**

### **Documentation Types**

- **API Documentation** - XML comments and generated docs
- **User Guides** - How-to guides and tutorials
- **Examples** - Sample projects and code snippets
- **Architecture Docs** - Design decisions and architecture

### **Writing Guidelines**

- Use **clear, concise language**
- Include **practical examples**
- **Test all code examples**
- Use **proper markdown formatting**
- Add **screenshots** where helpful

## 🔄 **Release Process**

### **Versioning**

We use [Semantic Versioning](https://semver.org/):

- **MAJOR** - Breaking changes
- **MINOR** - New features (backwards compatible)
- **PATCH** - Bug fixes (backwards compatible)

### **Release Checklist**

- [ ] All tests pass
- [ ] Documentation updated
- [ ] Version numbers incremented
- [ ] Release notes prepared
- [ ] NuGet packages built and tested

## 🆘 **Getting Help**

### **Communication Channels**

- **GitHub Issues** - Bug reports and feature requests
- **GitHub Discussions** - General questions and discussions
- **Pull Request Comments** - Code review and feedback

### **Resources**

- [MSBuild Documentation](https://docs.microsoft.com/en-us/visualstudio/msbuild/)
- [Power Platform Developer Documentation](https://docs.microsoft.com/en-us/power-platform/developer/)
- [.NET API Documentation](https://docs.microsoft.com/en-us/dotnet/api/)

## 🎖️ **Recognition**

Contributors will be recognized in:

- **Release notes** for significant contributions
- **Contributors section** in README
- **Special mentions** for exceptional contributions

## 📄 **License**

By contributing to OpenStrata.MSBuild, you agree that your contributions will be licensed under the [Apache 2.0 License](LICENSE.txt).

---

Thank you for contributing to OpenStrata.MSBuild! Your efforts help make Power Platform development better for everyone. 🚀 
 