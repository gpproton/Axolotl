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
using Axolotl.EFCore.Interfaces;

namespace Axolotl.EFCore.Repository;

public abstract partial class GenericBaseRepository<TEntity, TContext, TKey> where TEntity : class, IAggregateRoot, IHasKey<TKey>
    where TContext : DbContext
    where TKey : notnull{
    public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _dbSet.Update(entity);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
    
    public override async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        var updateRangeAsync = entities.ToList();
        _dbSet.UpdateRange(updateRangeAsync);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return updateRangeAsync;
    }

    public override async Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {
        _dbSet.Remove(entity);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity?> DeleteAsync(TKey id, CancellationToken cancellationToken = default) {
        var item = await this.GetByIdAsync(id, cancellationToken);
        if (item is null) return item;
        
        _dbSet.Remove(item);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return item;
    }
    
    public override async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) {
        var deleteRangeAsync = entities.ToList();
        _dbSet.RemoveRange(deleteRangeAsync);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return deleteRangeAsync;
    }

    public async Task<int> DeleteRangeAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default) {
        var items = await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        _dbSet.RemoveRange(items);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return items.Count;
    }

    public async Task<IEnumerable<TEntity>> DeleteBySpec(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) {
        var items = await _dbSet.WithSpecification(specification).ToListAsync(cancellationToken);
        _dbSet.RemoveRange(items);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return items;
    }
}