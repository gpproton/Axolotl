// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Axolotl.EFCore.Interfaces;

namespace Axolotl.EFCore.Repository;

public abstract partial class GenericBaseRepository<TEntity, TContext, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot, IHasKey<TKey>
    where TContext : DbContext
    where TKey : notnull {
    private readonly DbSet<TEntity> _dbSet;
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IUnitOfWork<TContext> UnitOfWork;

    protected GenericBaseRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork.Context) {
        UnitOfWork = unitOfWork;
        _dbSet = UnitOfWork.Context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query(CancellationToken cancellationToken = default) =>
        _dbSet.AsQueryable();
    public IQueryable<TEntity> Query(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
    ApplySpecification(specification).AsQueryable();

    public async Task<IEnumerable<TEntity>> GetBySpec(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).AsNoTracking().ToListAsync(cancellationToken);

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression) => _dbSet.Where(expression);
    
    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification, Expression<Func<TEntity, bool>> expression) =>
        ApplySpecification(specification).Where(expression);

    public async Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default) =>
    await _dbSet.FirstOrDefaultAsync(cancellationToken);
}