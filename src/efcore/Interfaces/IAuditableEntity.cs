// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the Reciprocal Public License (RPL-1.5) and Trace License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Axolotl.EFCore.Interfaces;

public interface IAuditableEntity {
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}

public interface IAuditableEntity<TKey> : IAuditableEntity {
    public TKey? CreatedBy { get; set; }
    public new DateTimeOffset CreatedAt { get; set; }
    public TKey? UpdatedBy { get; set; }
    public new DateTimeOffset? UpdatedAt { get; set; }
    public TKey? DeletedBy { get; set; }
    public new DateTimeOffset? DeletedAt { get; set; }
}