**OpenStrata.MSBuild.ConfigData** contains build definitions that allow for an OpenStrata ConfigData project to manage ConfigData packages generated using the Power Platform Configuration Migration tool.  As a result, ConfigData will be:

- treated as a configuration item with version control.
- tightly couple Dataverse Solution version control with Config data version control.
- automatically incorporated into the distribution options the OpenStrata framework provides.
- merged into a single import file when multiple files exist. (coming soon.)
- cleaned and verified during MSBuild process. (coming soon.)
- subject to tasks not currently envisioned via extending build definitions.

The output of an OpenStrata ConfigData project is a single, consolidated and properly sequenced ConfigData package compatible with the Power Platform Configuration Migration tool and Package Deployer constructs.

Typically, the output packaged ConfigData will be distributed as part of an OpenStrata Strati package, ultimately being included as part of an Import Package, produced using the OpenStrata Framework, via a PackageReference or direct ProjectReference.  
***
Install **[OpenStrata.NET.Templates](https://www.nuget.org/packages/OpenStrata.NET.Templates)** to get started.

After **OpenStrata.NET.Templates** is installed, create a ConfigData project using the `openstrata-configdata` template.

```
dotnet new openstrata-configdata --name [preferred-name]
```



***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

