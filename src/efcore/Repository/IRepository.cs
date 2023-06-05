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
using Axolotl.EFCore.Interfaces;

#nullable enable
namespace Axolotl.EFCore.Repository;

  public interface IRepository<TEntity> : 
    IReadRepository<TEntity>,
    IRepositoryBase<TEntity>
    where TEntity : class, IAggregateRoot, IHasKey
  {
      new Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
      new Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
      new Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
      Task<TEntity?> DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
      new Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
      Task<int> DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default) where TId : notnull;
      Task<IEnumerable<TEntity>> DeleteBySpec(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
      Task ClearAsync(CancellationToken cancellationToken = default);
  }