// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Proton.Common.Extensions;
using Proton.Common.Filters;

namespace Proton.Common.Http;

public abstract class AbstractHttpService (HttpClient http) : BaseHttpService(http), IHttpService {
    public async Task<T> Get<T>(string uri) {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }
    
    public async Task<T> Get<T>(string uri, object? query) {
        string queries = query != null ? query.GetQueryString() : (new PageFilter()).GetQueryString();
        var url = uri + queries;
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequest<T>(request);
    }

    public async Task Post(string uri, object? value) {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        await SendRequest(request);
    }
    
    public async Task<T> Post<T>(string uri) {
        var request = CreateRequest(HttpMethod.Post, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T> Post<T>(string uri, object? value) {
        var request = CreateRequest(HttpMethod.Post, uri, value);
        return await SendRequest<T>(request);
    }

    public async Task Put(string uri, object? value) {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        await SendRequest(request);
    }
    
    public async Task<T> Put<T>(string uri) {
        var request = CreateRequest(HttpMethod.Put, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T> Put<T>(string uri, object? value) {
        var request = CreateRequest(HttpMethod.Put, uri, value);
        return await SendRequest<T>(request);
    }

    public async Task Delete(string uri) {
        var request = CreateRequest(HttpMethod.Delete, uri);
        await SendRequest(request);
    }

    public async Task<T> Delete<T>(string uri) {
        var request = CreateRequest(HttpMethod.Delete, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T> Delete<T>(string uri, object? value) {
        var request = CreateRequest(HttpMethod.Delete, uri, value);
        return await SendRequest<T>(request);
    }
}