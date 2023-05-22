# Proton.Common

 A personal shared library for various types of dotnet project types


### Sub-packages

Proton.Common:

Proton.Common.EFCore:

Proton.Common.Razor:

Proton.Common.Maui:


# Install

The framework is provided as a set of NuGet packages. In many cases you'll only need the base package, but if you need efcore, razor or Maui there are implementation-specific packages available to assist.

To install the minimum requirements:

```
Install-Package Proton.Common
```

To install support for serialization, AutoFixture, EF Core, Model Binding, or Dapper select the lines that apply:

```
Install-Package Proton.Common
Install-Package Proton.Common.EFCore
Install-Package Proton.Common.Razor
Install-Package Proton.Common.Maui
```
