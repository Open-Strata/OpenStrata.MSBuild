# OpenStrata.MSBuild.Plugin

MSBuild SDK for Microsoft Dynamics 365 plugin development, testing, and deployment automation.

## Overview

**OpenStrata.MSBuild.Plugin** provides specialized tooling for Dynamics 365 plugin development that works in conjunction with Microsoft.PowerApps.MSBuild.Plugin package.

Key capabilities:
- **Plugin Development** - Streamlined plugin project templates and build processes
- **Metadata Management** - Automatic generation of plugin metadata and registration info
- **Version Control Integration** - Tightly couple UIDs with corresponding source code in version control
- **Package Management** - Plugin packaging for distribution and deployment
- **Testing Integration** - Unit testing framework integration for plugin development

## Installation

```xml
<PackageReference Include="OpenStrata.MSBuild.Plugin" Version="1.*" />
```

### Future Functionality

Planned enhancements include:
- Automatic generation of plugin metadata ensuring publishers can manage and tightly couple version control UIDs with corresponding source code
- Enhanced plugin registration automation
- Advanced testing and validation tools

Install **[OpenStrata.NET.Templates](https://www.nuget.org/packages/OpenStrata.NET.Templates)** to get started.

After **OpenStrata.NET.Templates** is installed, create a Plugin project using the `openstrata-plugin` template.

```
dotnet new openstrata-plugin --name [preferred-name]
```


***

⚠

*The OpenStrata initiative is currently a Proof-of-Concept effort.  We welcome and invite you to evaluate OpenStrata products, provide feedback, and make suggestions or requests.  Please be mindful this is an active development effort.  Not all features are fully operational...*


***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

