**OpenStrata.MSBuild.Package** contains build definitions that allow for an OpenStrata Package project to create packages for deploying to environments using the Power Platform Package Deployer.

An OpenStrata Package project depends on the OpenStrata.Deployment.Sdk and Microsoft.CrmSdk.XrmTooling.PackageDeployment to build a package using the Strata aware base class, ImportPackageStrataBase.

The build definitions of this package will manage the contents of the package and maintain the package importconfig file according to the dependencies
identified as project references and Strati package references.

The benefits of using the OpenStrata Package project to create Deployer packages are:

- Packages can be strata aware, allowing for upstream capabilities to be included in a single distribution.
- Strata Aware Packages leverage inherit dependency chain of Nuget to ensure version dependency stays in sync.
- Strata Aware packages can seamlessly integrate the deployment of components produced publishers, whether internal or external.
- Packages can be created to establish entire environments in support of every phase of the ALM.
- Packages can be composed simply by adding desired components from a repository of Strati packages produced internally or by other publishers.

***

Install **[OpenStrata.NET.Templates](https://www.nuget.org/packages/OpenStrata.NET.Templates)** to get started.

After **OpenStrata.NET.Templates** is installed, create a Package project using the `openstrata-package` template.

```
dotnet new openstrata-package --name [preferred-name]
```



***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

