// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Proton.Common.EFCore.Interfaces;
using Proton.Common.EFCore.Repository;
using Proton.Common.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Service;

public sealed class GenericService<TEntity>(IRepository<TEntity> repo, IGenericService<TEntity, TEntity> root) :
    IGenericService<TEntity> where TEntity : class, IAggregateRoot, IResponse {

    public async Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter, Type? spec, CancellationToken cancellationToken = default) =>
        await root.GetAllAsync(filter, spec, cancellationToken);

    public async Task<Response<TEntity?>> GetByIdAsync<TId>(TId id, Type? spec = null, CancellationToken cancellationToken = default) where TId : notnull =>
        await root.GetByIdAsync(id, spec, cancellationToken);

    public async Task<Response<TEntity>> CreateAsync(TEntity value, CancellationToken cancellationToken = default) =>
        await root.CreateAsync(value, cancellationToken);

    public async Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
        await root.CreateRangeAsync(values, cancellationToken);

    public async Task<Response<TEntity>> UpdateAsync(TEntity value, Type? spec = null, CancellationToken cancellationToken = default) =>
        await root.UpdateAsync(value, spec, cancellationToken);

    public async Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values, Type? spec = null, CancellationToken cancellationToken = default) =>
        await root.UpdateRangeAsync(values, spec, cancellationToken);

    public async Task<Response<TEntity?>> DeleteAsync<TId>(TId id, Type? spec = null, CancellationToken cancellationToken = default) where TId : notnull =>
        await root.DeleteAsync(id, spec, cancellationToken);

    public async Task<PagedResponse<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> values, Type? spec = null, CancellationToken cancellationToken = default) {
        IEnumerable<TEntity> aggregateRoots = values.ToList();
        await repo.DeleteRangeAsync(aggregateRoots, cancellationToken);

        return new PagedResponse<TEntity>(aggregateRoots);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default) => await root.ClearAsync(cancellationToken);
}