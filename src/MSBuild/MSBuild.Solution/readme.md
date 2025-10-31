# OpenStrata.MSBuild.Solution

MSBuild SDK for Dataverse solution development, packaging, and deployment automation.

## Overview

**OpenStrata.MSBuild.Solution** provides comprehensive tooling for Microsoft Dataverse solution development that works in conjunction with Microsoft.PowerApps.MSBuild.Solution to build Dataverse Solution projects and incorporate them into AppSource and/or Strati distribution packages.

Key capabilities:
- **Solution Management** - Create, modify, and validate Dataverse solutions
- **Automated Packaging** - Build solution packages (.zip files) from source  
- **Publisher Management** - Configure and validate solution publishers
- **AppSource Integration** - Prepare solutions for AppSource distribution
- **Strati Packaging** - Integration with OpenStrata distribution system

## Installation

```xml
<PackageReference Include="OpenStrata.MSBuild.Solution" Version="1.*" />
```

Install **[OpenStrata.NET.Templates](https://www.nuget.org/packages/OpenStrata.NET.Templates)** to get started with solution project templates.

After **OpenStrata.NET.Templates** is installed, create a Dataverse Solution project using the `openstrata-solution` template.

```
dotnet new openstrata-solution --name [preferred-name]
```


***

⚠

*The OpenStrata initiative is currently a Proof-of-Concept effort.  We welcome and invite you to evaluate OpenStrata products, provide feedback, and make suggestions or requests.  Please be mindful this is an active development effort.  Not all features are fully operational...*


***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

