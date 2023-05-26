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

public class GenericHttpService<TEntity> : IGenericHttpService<TEntity> where TEntity : class {
    private string _path = String.Empty;
    private readonly IHttpService _http;

    protected GenericHttpService(IHttpService http) {
        _http = http;
        var type = typeof(TEntity);
        this.SetPath($"api/v1/{type.Name.ToLower()}");
    }

    protected void SetPath(string path) {
        _path = path;
    }
    
    public async Task<PagedResponse<TEntity>> GetAllAsync(object? query = null) =>
        await _http.Get<PagedResponse<TEntity>>(_path, query);

    public async Task<Response<TEntity?>> GetByIdAsync<TId>(TId id) where TId : notnull =>
        await _http.Get<Response<TEntity?>>($"{_path}/{id}");

    public async Task<Response<TEntity>> CreateAsync(TEntity value) =>
        await _http.Post<Response<TEntity>>(_path, value);

    public async Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values) =>
        await _http.Post<PagedResponse<TEntity>>($"{_path}/multiple", values);

    public async Task<Response<TEntity>> UpdateAsync(TEntity value) =>
        await _http.Put<Response<TEntity>>(_path, value);

    public async Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values) =>
        await _http.Put<PagedResponse<TEntity>>($"{_path}/multiple", values);

    public async Task<Response<TEntity?>> DeleteAsync<TId>(TId id) where TId : notnull =>
        await _http.Delete<Response<TEntity?>>($"{_path}/{id}");

    public async Task<PagedResponse<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> items) =>
        await _http.Delete<PagedResponse<TEntity>>($"{_path}/multiple", items);

    public async Task<PagedResponse<TEntity>> DeleteRangeAsync<TId>(IEnumerable<TId> ids) where TId : notnull =>
        await _http.Delete<PagedResponse<TEntity>>($"{_path}/multiple", ids);
}