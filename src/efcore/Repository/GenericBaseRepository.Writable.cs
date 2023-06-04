// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.EFCore.Repository;

public abstract partial class GenericBaseRepository<TEntity, TContext> where TEntity : class, IAggregateRoot, IHasKey
    where TContext : DbContext {
    public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _context.Set<TEntity>().Update(entity);
        await SaveChangesAsync(cancellationToken);

        return entity;
    }
    
    public override async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        IEnumerable<TEntity> updateRangeAsync = entities.ToList();
        _context.Set<TEntity>().UpdateRange(updateRangeAsync);
        await SaveChangesAsync(cancellationToken);

        return updateRangeAsync;
    }

    public override async Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _context.Set<TEntity>().Remove(entity);
        await SaveChangesAsync(cancellationToken);

        return entity;
    }

   

    public async Task<TEntity?> DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var item = await this.GetByIdAsync(id, cancellationToken);
        if (item is not null) return await this.UpdateAsync(item, cancellationToken);

        return item;
    }
    
    public override async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        IEnumerable<TEntity> deleteRangeAsync = entities.ToList();
        _context.Set<TEntity>().RemoveRange(deleteRangeAsync);
        await SaveChangesAsync(cancellationToken);

        return deleteRangeAsync;
    }

    public async Task<IEnumerable<TEntity>> DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default) where TId : notnull {
        var items = await _context.Set<TEntity>().Where(x => ids.Contains((TId)x.Id)).ToListAsync(cancellationToken);
        _context.Set<TEntity>().RemoveRange(items);
        await SaveChangesAsync(cancellationToken);
        
        return items;
    }

    public async Task<IEnumerable<TEntity>> DeleteBySpec(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) {
        var items = await _context.Set<TEntity>().WithSpecification(specification).ToListAsync(cancellationToken);
        _context.Set<TEntity>().RemoveRange(items);
        await SaveChangesAsync(cancellationToken);
        
        return items;
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default) {
        await _context.Database.EnsureDeletedAsync(cancellationToken);
        await _context.Database.EnsureDeletedAsync(cancellationToken);
    }
}