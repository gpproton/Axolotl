// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.Response;

namespace Axolotl.Http;

public interface IGenericHttpService<TResponse> where TResponse : class, IResponse {
    Task<PagedResponse<TResponse>> GetAllAsync(object? query = null, string? path = null);
    Task<Response<TResponse?>> GetByIdAsync<TId>(TId id, string? path = null) where TId : notnull;
    Task<PagedResponse<TResponse>> GetBySpecAsync(object? value, string? path = null);
    Task<Response<TResponse>> CreateAsync(TResponse value, string? path = null);
    Task<PagedResponse<TResponse>> CreateRangeAsync(IEnumerable<TResponse> values, string? path = null);
    Task<Response<TResponse>> UpdateAsync(TResponse value, string? path = null);
    Task<PagedResponse<TResponse>> UpdateRangeAsync(IEnumerable<TResponse> values, string? path = null);
    Task<Response<TResponse?>> DeleteAsync<TId>(TId id, string? path = null) where TId : notnull;
    Task<PagedResponse<TResponse>> DeleteRangeAsync<TId>(IEnumerable<TId> ids, string? path = null) where TId : notnull;
    Task<PagedResponse<TResponse>> DeleteSpecAsync(object? values, string? path = null);
}