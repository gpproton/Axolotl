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

namespace Proton.Common.EFCore;

public abstract class GenericBaseRepository<T, TContext> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    where TContext : DbContext {
    private readonly TContext _context;
    protected GenericBaseRepository(TContext context) : base(context) => _context = context;
    public IQueryable<T> GetAll(CancellationToken cancellationToken = default) =>
    _context.Set<T>().AsQueryable();
    public IQueryable<T> GetAll(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
    ApplySpecification(specification).AsQueryable();
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression);
    public async Task<T?> FirstOrDefaultAsync(CancellationToken cancellationToken = default) =>
    await _context.Set<T>().FirstOrDefaultAsync(cancellationToken);
}