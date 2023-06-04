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
using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.EFCore.Repository;

public abstract partial class GenericBaseRepository<TEntity, TContext> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : class, IAggregateRoot, IHasKey
    where TContext : DbContext {
    private readonly TContext _context;
    
    protected GenericBaseRepository(TContext context) : base(context) => _context = context;
    public IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default) =>
    _context.Set<TEntity>().AsQueryable();
    public IQueryable<TEntity> GetAll(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
    ApplySpecification(specification).AsQueryable();

    public async Task<IEnumerable<TEntity>> GetBySpec(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).AsNoTracking().ToListAsync(cancellationToken);

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().Where(expression);
    
    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification, Expression<Func<TEntity, bool>> expression) =>
        ApplySpecification(specification).Where(expression);

    public async Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default) =>
    await _context.Set<TEntity>().FirstOrDefaultAsync(cancellationToken);

    public IQueryable<TEntity> GetQueryable(CancellationToken cancellationToken = default) =>
        _context.Set<TEntity>().AsQueryable();

    public DbSet<TEntity> GetContext(CancellationToken cancellationToken = default) =>
        _context.Set<TEntity>();
}