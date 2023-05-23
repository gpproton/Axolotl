// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Proton.Common.Response;

public class Response<T> {
    public Response() {
        Success = true;
    }

    public Response(string message) {
        Success = true;
        Message = message;
    }

    public Response(string message, bool success) {
        Success = success;
        Message = message;
    }

    public Response(T? data, string message = "", bool success = true) {
        Message = message;
        Data = data;
        Success = success;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public List<string> Errors { get; set; } = null!;
    public T? Data { get; set; }
}