// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Proton.Common.Response;

namespace Proton.Common.Http;

public interface IGenericHttpService<TEntity> where TEntity : class {
    Task<PagedResponse<TEntity>> GetAllAsync(object? query = null);
    Task<Response<TEntity>> GetByIdAsync(Guid id);
    Task<Response<TEntity>> CreateAsync(TEntity value);
    Task<Response<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values);
    Task<Response<TEntity>> UpdateAsync(TEntity value);
    Task<Response<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values);
    Task<Response<TEntity>> DeleteAsync(Guid id);
    Task<Response<TEntity>> DeleteRangeAsync(IEnumerable<Guid> items);
}