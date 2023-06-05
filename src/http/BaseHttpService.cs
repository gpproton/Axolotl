// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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