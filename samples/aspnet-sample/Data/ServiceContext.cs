// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proton.Common.AspNetSample.Features.CategoryModule;
using Proton.Common.AspNetSample.Features.PostModule;

namespace Proton.Common.AspNetSample.Data;

public class ServiceContext : DbContext {
    public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite") {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTimeOffset)
                     || p.PropertyType == typeof(DateTimeOffset?));
                foreach (var property in properties)
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
            }
        }
    }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;
}