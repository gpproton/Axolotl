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

public class PagedResponse<T> : Response<T> {
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 25;
    public int Total { get; set; }
    
    public new IEnumerable<T>? Data { get; set; }

    public int Pages => Convert.ToInt32(Math.Ceiling((double)this.Total / this.Size));

    public PagedResponse() { }

    public PagedResponse(IEnumerable<T>? data) => Data = data;

    public PagedResponse(IEnumerable<T>? data, int page, int size, int total, string message = "", bool success = true) {
        Message = message;
        Data = data;
        Page = page;
        Size = size;
        Total = total;
        Success = success;
    }
}