# OpenStrata.MSBuild Core

The core MSBuild SDK that provides fundamental tasks and shared functionality for all OpenStrata Power Platform development tools.

## Overview

OpenStrata.MSBuild is the foundation SDK that provides:

- **Core MSBuild Tasks** - Essential build automation tasks
- **Assembly Signing** - Automated strong name key management
- **Base Classes** - Shared functionality for other OpenStrata SDKs
- **Build Utilities** - Common build pipeline components

## Installation

### NuGet Package

```xml
<PackageReference Include="OpenStrata.MSBuild" Version="1.*" />
```

### Manual Installation

```powershell
dotnet add package OpenStrata.MSBuild
```

## Key Features

### 🔐 **Strong Name Key Management**

Automatically generates and manages strong name keys for assembly signing:

```xml
<PropertyGroup>
  <SignAssembly>true</SignAssembly>
  <AssemblyOriginatorKeyFile>$(MSBuildProjectName).snk</AssemblyOriginatorKeyFile>
</PropertyGroup>
```

### 🔧 **Core MSBuild Tasks**

#### CreateStrongNameKeyFile Task

Generates strong name key files automatically during build:

```xml
<Target Name="GenerateStrongNameKey" BeforeTargets="CoreCompile">
  <CreateStrongNameKeyFile Path="$(AssemblyOriginatorKeyFile)" 
                          Condition="!Exists('$(AssemblyOriginatorKeyFile)')" />
</Target>
```

#### FixUpMergeDiffVersionMarkup Task

Handles version markup processing for merge scenarios:

```xml
<Target Name="ProcessVersionMarkup" BeforeTargets="Build">
  <FixUpMergeDiffVersionMarkup InputFile="version.xml" 
                              OutputFile="version.processed.xml" />
</Target>
```

Install **[OpenStrata.NET.Templates](https://www.nuget.org/packages/OpenStrata.NET.Templates)** to get started with project templates.  


***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

