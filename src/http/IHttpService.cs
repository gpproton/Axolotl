// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
//
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Proton.Common.Http;

public interface IHttpService {
    Task<T> Get<T>(string uri);
    Task<T> Get<T>(string uri, object? query);
    Task Post(string uri, object? value);
    Task<T> Post<T>(string uri, object? value);
    Task<T> Post<T>(string uri);
    Task Put(string uri, object? value);
    Task<T> Put<T>(string uri);
    Task<T> Put<T>(string uri, object? value);
    Task Delete(string uri);
    Task<T> Delete<T>(string uri);
    Task<T> Delete<T>(string uri, ICollection? values);
}