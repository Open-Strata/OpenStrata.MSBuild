**OpenStrata.MSBuild.Stratify** contains build definitions that allow for an OpenStrata Srati project to produce a Nuget package containing all applicable components required for a Dataverse solution to be production ready.  Components may include configuration data, import definitions,
Package Deployer code, and any other component not otherwise "Solution-Aware".

Collectively, the solution and its components form Strati, which is packaged and distributed and ready to be consumed and integrated as an upstream component of an OpenStrata Package project or another Strati project.  

Strati components are designed to be published for one or more of the following purposes:

- **Public Distribution** via Nuget.org or some other public software package repository.  This is a good choice for accelerators or open-source projects.

- **Private Distribution** via a Publisher managed software package repository such as those available from dev.azure.com or github.com. This might be practical when the publisher is providing Strati packages for customers as part of a licensing or services agreement.

- **Internal Use** via a Publisher managed software package repository with access limited to internal use only.  This type of repository is appropriate for Strati packages that are consumed internally or strati being staged for a phase of an application life cycle.

- **Development/Debugging** via a shared network resource, Azure storage, or on an App Maker's local file system.  This type of repository is appropriate for Strati packages that are consumed by an individual or team of app makers as part of the Making phase of an Application lifecycle.

***

Install **[OpenStrata.NET.Templates](https://www.nuget.org/packages/OpenStrata.NET.Templates)** to get started.

After **OpenStrata.NET.Templates** is installed, create a Strati project using the `openstrata-strati` template.

```
dotnet new openstrata-strati--name [preferred-name]
```



***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

