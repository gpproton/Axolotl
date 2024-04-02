---
_layout: landing
---

A personal shared library for various types of dotnet project types.

## packages

- Axolotl
- Axolotl.Http
- Axolotl.EFCore
- Axolotl.Razor

# Install

The framework is provided as a set of NuGet packages. In many cases you'll only need the base package, but if you need efcore or razor there are implementation-specific packages available to assist.

To install the run any of the following required pakage:

```ps1
Install-Package Axolotl
Install-Package Axolotl.EFCore
Install-Package Axolotl.AspNet
```

```csharp
public sealed class Post : AuditableEntity<Guid> {
    public string Title { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = null!;
}
```

### Create your DB context

```csharp
public class ServiceContext : DbContext {
    public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    
    public virtual DbSet<Post> Posts { get; set; } = null!;
    
}
```
