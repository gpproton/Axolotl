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
   Created At: Wed 11 Dec 2024 20:48:00
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 20:48:00
*/

using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Axolotl.Http;

public abstract class BaseHttpService(HttpClient http) {
    protected HttpRequestMessage CreateRequest(HttpMethod method, string uri, object? value = null) {
        var request = new HttpRequestMessage(method, uri);
        if (value != null)
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return request;
    }

    protected async Task SendRequest(HttpRequestMessage request) {
        await AddJwtHeader(request);
        try {
            using var response = await http.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                await SignOut();
                return;
            }
            await HandleErrors(response);
        }
        catch (Exception) {
            Console.WriteLine("A HTTP callback error occurred");
        }
    }

    protected async Task<T> SendRequest<T>(HttpRequestMessage request) {
        await AddJwtHeader(request);
        try {
            using var response = await http.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                await SignOut();
                return default!;
            }
            await HandleErrors(response);
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new Converters.StringConverter());

            return (await response.Content.ReadFromJsonAsync<T>(options))!;
        }
        catch (Exception) {
            Console.WriteLine("A HTTP callback error occurred");
        }

        return default!;
    }

    protected abstract Task AddJwtHeader(HttpRequestMessage request);

    protected abstract Task SignOut();

    private async Task HandleErrors(HttpResponseMessage response) {
        if (!response.IsSuccessStatusCode) {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (error != null) throw new Exception(error["message"]);
        }
    }
}