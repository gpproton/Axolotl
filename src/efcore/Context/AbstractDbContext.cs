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
using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.EFCore.Context;

public abstract class AbstractDbContext : DbContext {
    protected AbstractDbContext() { }
    protected AbstractDbContext(DbContextOptions options) : base(options) { }

    public override int SaveChanges(bool acceptAllChangesOnSuccess) {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default
    ) {
        OnBeforeSaving();
        return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
            cancellationToken));
    }

    private void OnBeforeSaving() {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in entries) {
            if (entry.Entity is IAuditableEntity trackable) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.Property("UpdatedAt").IsModified = false;
                        entry.Property("DeletedAt").IsModified = false;
                        trackable.CreatedAt = utcNow;
                        break;
                    case EntityState.Modified:
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("DeletedAt").IsModified = false;
                        trackable.UpdatedAt = utcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        // ReSharper disable once UnusedVariable
                        bool all = entry.References.All(e => e.IsModified = true);
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("UpdatedAt").IsModified = false;
                        trackable.DeletedAt = utcNow;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    default:
                        break;
                }
            }
        }
    }
}