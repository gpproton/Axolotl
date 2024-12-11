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
   Created At: Wed 11 Dec 2024 20:47:40
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 20:47:40
*/

using Axolotl.Extensions;
using Axolotl.Filters;

namespace Axolotl.Http;

public abstract class AbstractHttpService(HttpClient http) : BaseHttpService(http), IHttpService {
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