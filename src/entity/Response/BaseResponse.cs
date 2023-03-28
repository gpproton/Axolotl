// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Proton.Common.Entity.Response;

public abstract class BaseResponse<T> {
    protected BaseResponse() => Success = true;

    protected BaseResponse(string message) {
        Success = true;
        Message = message;
    }

    protected BaseResponse(string message, bool success) {
        Success = success;
        Message = message;
    }

    protected BaseResponse(T? data, string message = "") {
        Success = true;
        Message = message;
        Data = data;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public List<string> Errors { get; set; } = null!;
    public T? Data { get; set; }
}