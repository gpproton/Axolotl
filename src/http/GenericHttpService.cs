/*
  Copyright (c) 2024 <Godwin peter. O>
  
  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:
  
  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.
  
  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
  
   Author: Godwin peter. O (me@godwin.dev)
   Created At: Wed 11 Dec 2024 20:48:18
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 20:48:18
*/

using Axolotl.Response;

namespace Axolotl.Http;

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