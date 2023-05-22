// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.EFCore.Base;

public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity<TKey> {
    public TKey? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public TKey? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public TKey? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}