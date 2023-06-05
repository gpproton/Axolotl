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
using Microsoft.EntityFrameworkCore;
using Axolotl.EFCore.Interfaces;

namespace Axolotl.EFCore.Repository;

public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class, IAggregateRoot {
    IQueryable<TEntity> GetQueryable(CancellationToken cancellationToken = default (CancellationToken));

    DbSet<TEntity> GetContext(CancellationToken cancellationToken = default (CancellationToken));
    
    /// <summary>
    /// Returns the all element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The IQueryable result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
    /// </returns>
    IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the all element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The IQueryable result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
    /// </returns>
    IQueryable<TEntity> GetAll(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> GetBySpec(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the all element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="expression">The Linq expression for query.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The sequence result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
    /// </returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T?" />, or <see langword="null"/>.
    /// </returns>
    Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
}