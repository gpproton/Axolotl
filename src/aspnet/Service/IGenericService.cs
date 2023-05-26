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

public interface IGenericService<TEntity> where TEntity : class, IAggregateRoot, IResponse {
    Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter, Type? type);
    Task<Response<TEntity?>> GetByIdAsync<TId>(TId id) where TId : notnull;
    Task<Response<TEntity>> CreateAsync(TEntity value);
    Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values);
    Task<Response<TEntity>> UpdateAsync(TEntity value);
    Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values);
    Task<Response<TEntity?>> DeleteAsync<TId>(TId id) where TId : notnull;
    Task<PagedResponse<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> values);
    Task ClearAsync();
}