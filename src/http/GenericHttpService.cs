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

public class GenericHttpService<TResponse> : IGenericHttpService<TResponse> where TResponse : class, IResponse {
    private string _path = String.Empty;
    private readonly IHttpService _http;

    protected GenericHttpService(IHttpService http) {
        _http = http;
        var type = typeof(TResponse);
        this.SetPath($"api/v1/{type.Name.ToLower()}");
    }

    protected void SetPath(string path) {
        _path = path;
    }
    
    public async Task<PagedResponse<TResponse>> GetAllAsync(object? query = null, string? path = null) =>
        await _http.Get<PagedResponse<TResponse>>(path ?? _path, query);

    public async Task<Response<TResponse?>> GetByIdAsync<TId>(TId id, string? path = null) where TId : notnull =>
        await _http.Get<Response<TResponse?>>(path ?? $"{_path}/{id}");

    public async Task<PagedResponse<TResponse>> GetBySpecAsync(object? value, string? path = null) =>
        await _http.Post<PagedResponse<TResponse>>(path ?? $"{_path}/spec", value);

    public async Task<Response<TResponse>> CreateAsync(TResponse value, string? path = null) =>
        await _http.Post<Response<TResponse>>(path ?? _path, value);

    public async Task<PagedResponse<TResponse>> CreateRangeAsync(IEnumerable<TResponse> values, string? path = null) =>
        await _http.Post<PagedResponse<TResponse>>(path ?? $"{_path}/range", values);

    public async Task<Response<TResponse>> UpdateAsync(TResponse value, string? path = null) =>
        await _http.Put<Response<TResponse>>(path ?? _path, value);

    public async Task<PagedResponse<TResponse>> UpdateRangeAsync(IEnumerable<TResponse> values, string? path = null) =>
        await _http.Put<PagedResponse<TResponse>>(path ?? $"{_path}/range", values);

    public async Task<Response<TResponse?>> DeleteAsync<TId>(TId id, string? path = null) where TId : notnull =>
        await _http.Delete<Response<TResponse?>>(path ?? $"{_path}/{id}");

    public async Task<PagedResponse<TResponse>> DeleteRangeAsync<TId>(IEnumerable<TId> ids, string? path = null) where TId : notnull =>
        await _http.Delete<PagedResponse<TResponse>>(path ?? $"{_path}/range", ids);
    
    public async Task<PagedResponse<TResponse>> DeleteSpecAsync(object? items, string? path = null) =>
        await _http.Delete<PagedResponse<TResponse>>(path ?? $"{_path}/spec", items);
}