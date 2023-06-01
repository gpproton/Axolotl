# Proton.Common

 A personal shared library for various types of dotnet project types


### Sub-packages

Proton.Common:

Proton.Common.Http:

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
Install-Package Proton.Common.Http
Install-Package Proton.Common.EFCore
Install-Package Proton.Common.AspNet
Install-Package Proton.Common.Razor
Install-Package Proton.Common.Maui
```

## Asp.Net Core Samples

### Added required packages

```powershell
Install-Package Proton.Common
Install-Package Proton.Common.EFCore
Install-Package Proton.Common.AspNet
```

### Add sample entity

```csharp
public sealed class Post : AuditableEntity<Guid> {
    public string Title { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = null!;
}
```

### Add to context

```csharp
public class ServiceContext : DbContext {
    public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    
    public virtual DbSet<Post> Posts { get; set; } = null!;
    
}
```

### Create optional filter specification

```csharp
public sealed class CategorySpec : Specification<Post> {
    public CategorySpec(IPageFilter filter) {
        var search = filter.Search ?? string.Empty;
        var text = search.ToLower().Split(" ").ToList().Select(x => x);
        Query.Where(x =>  x.Title != String.Empty && x.Title.Length > 3 && text.Any(p => EF.Functions.Like(x.Title.ToLower(), $"%" + p + "%")))
            .AsNoTracking()
            .OrderBy(b => b.Title);
    }
}
```

### Create feature/endpoint

```csharp
public class CategoryFeature : GenericFeature<CategoryFeature> {
    public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        var state = new FeatureState(new List<RouteState> {
            new (RouteType.GetAll, typeof(CategorySpec)),
            new (RouteType.GetById),
            new (RouteType.Create)
        });

        return SetupGroup<CategoryFeature, Category, Guid>(endpoints, state);
    }
}
```
