// Copyright 2023 - 2023 Godwin peter .O (me@godwin.dev)
//
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.Json.Serialization;
using Proton.Common.Interfaces;

namespace Proton.Common.Response;

public class AuditResponse : CoreResponse, IAuditResponse {
    public DateTimeOffset CreatedAt { get; set; }
    [JsonIgnore]
    public DateTimeOffset? UpdatedAt { get; set; }
    [JsonIgnore]
    public DateTimeOffset? DeletedAt { get; set; }
}

public class AuditResponse<TKey> : BaseResponse<TKey>, IAuditResponse<TKey> where TKey : notnull {
    [JsonIgnore]
    public TKey? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    [JsonIgnore]
    public TKey? UpdatedBy { get; set; }
    [JsonIgnore]
    public DateTimeOffset? UpdatedAt { get; set; }
    [JsonIgnore]
    public TKey? DeletedBy { get; set; }
    [JsonIgnore]
    public DateTimeOffset? DeletedAt { get; set; }
}