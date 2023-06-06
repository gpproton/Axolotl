// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.AspNetSample.Features.CategoryModule;
using Axolotl.AspNetSample.Features.PostModule;
using Axolotl.AspNetSample.Features.TagModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Axolotl.EFCore.Base;
using Axolotl.EFCore.Context;

namespace Axolotl.AspNetSample.Data;

public class ServiceContext : AbstractDbContext {
    public ServiceContext() { }
    public ServiceContext(DbContextOptions<ServiceContext> options) : base(ChangeOptionsType<ServiceContext>(options)) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        var entitiesAssembly = typeof (ServiceContext).Assembly;
        modelBuilder.RegisterAllEntities<BaseEntity<Guid>>(entitiesAssembly);
        modelBuilder.RegisterAllEntities<AuditableEntity<Guid>>(entitiesAssembly);
        modelBuilder.RegisterSoftDeleteFilter();
        
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            modelBuilder.DateTimeOffsetToBinary();
    }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    
}