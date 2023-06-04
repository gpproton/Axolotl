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
using Proton.Common.EFCore.Interfaces;
using Proton.Common.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Service;

public sealed class GenericService<TEntity>(IGenericService<TEntity, TEntity> root) :
    IGenericService<TEntity> where TEntity : class, IAggregateRoot, IHasKey, IResponse {
    public Task<PagedResponse<TEntity>> PageFilter(ISpecification<TEntity> specification, int? checkPage,
        int? checkSize,
        CancellationToken cancellationToken = default) =>
        root.PageFilter(specification, checkPage, checkSize, cancellationToken);

    public async Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter, Type? spec, CancellationToken cancellationToken = default) =>
        await root.GetAllAsync(filter, spec, cancellationToken);

    public async Task<Response<TEntity?>> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull =>
        await root.GetByIdAsync(id, cancellationToken);

    public async Task<PagedResponse<TEntity>> GetBySpec<TOption>(Type spec, TOption option,
        CancellationToken cancellationToken = default) where TOption : class, ISpecFilter =>
        await root.GetBySpec(spec, option, cancellationToken);

    public async Task<Response<TEntity>> CreateAsync(TEntity value, CancellationToken cancellationToken = default) =>
        await root.CreateAsync(value, cancellationToken);

    public async Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
        await root.CreateRangeAsync(values, cancellationToken);

    public async Task<Response<TEntity>> UpdateAsync(TEntity value, CancellationToken cancellationToken = default) =>
        await root.UpdateAsync(value, cancellationToken);

    public async Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
        await root.UpdateRangeAsync(values, cancellationToken);

    public async Task<Response<TEntity?>> DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull =>
        await root.DeleteAsync(id, cancellationToken);

    public async Task<Response<int>> DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default) where TId : notnull =>
        await root.DeleteRangeAsync(ids, cancellationToken);

    public async Task<PagedResponse<TEntity>> DeleteBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class =>
        await root.DeleteBySpec(spec, option, cancellationToken);

    public async Task ClearAsync(CancellationToken cancellationToken = default) => await root.ClearAsync(cancellationToken);
}