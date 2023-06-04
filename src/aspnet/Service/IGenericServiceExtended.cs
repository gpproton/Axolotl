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

public interface IGenericService<TEntity, TResponse> 
    where TEntity : class, IAggregateRoot
    where TResponse : class, IResponse {
    Task<PagedResponse<TResponse>> PageFilter(ISpecification<TEntity> specification, int? checkPage, int? checkSize,
        CancellationToken cancellationToken = default);
    Task<PagedResponse<TResponse>> GetAllAsync(IPageFilter? filter, Type? spec = null, CancellationToken cancellationToken = default);
    Task<Response<TResponse?>> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    Task<PagedResponse<TResponse>> GetBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class, ISpecFilter;
    Task<Response<TResponse>> CreateAsync(IResponse value, CancellationToken cancellationToken = default);
    Task<PagedResponse<TResponse>> CreateRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default);
    Task<Response<TResponse>> UpdateAsync(IResponse value, CancellationToken cancellationToken = default);
    Task<PagedResponse<TResponse>> UpdateRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default);
    Task<Response<TResponse?>> DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    Task<PagedResponse<TResponse>> DeleteRangeAsync<TId>(IEnumerable<TId> values, CancellationToken cancellationToken = default) where TId : notnull;
    Task<PagedResponse<TResponse>> DeleteBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class;
    Task ClearAsync(CancellationToken cancellationToken = default);
}