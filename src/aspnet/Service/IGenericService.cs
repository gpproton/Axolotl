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
using Axolotl.Interfaces;
using Axolotl.Response;

namespace Axolotl.AspNet.Service;

public interface IGenericService<TEntity, in TKey>
    where TEntity : class, IAggregateRoot, IResponse
    where TKey : notnull {
    Task<PagedResponse<TEntity>> PageFilter(ISpecification<TEntity> specification, int? checkPage, int? checkSize,
        CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter, Type? spec = null, CancellationToken cancellationToken = default);
    Task<Response<TEntity?>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> GetBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class, ISpecFilter;
    Task<Response<TEntity>> CreateAsync(TEntity value, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default);
    Task<Response<TEntity>> UpdateAsync(TEntity value, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default);
    Task<Response<TEntity?>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    Task<Response<int>> DeleteRangeAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> DeleteBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class;
    Task ClearAsync(CancellationToken cancellationToken = default);
}