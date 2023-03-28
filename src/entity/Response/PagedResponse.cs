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

public class PagedResponse<T> : BaseResponse<T> {
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }

    public int TotalPages {
        get {
            var total = ((double)this.Total / this.PerPage);
            return Convert.ToInt32(Math.Ceiling(total));
        }
    }
}