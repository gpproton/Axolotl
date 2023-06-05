// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.AspNetSample.Features.PostModule;
using Axolotl.EFCore.Base;

namespace Axolotl.AspNetSample.Features.CategoryModule;

public class Category : AuditableEntity<Guid> {
    public string Name { get; set; } = null!;
    public virtual ICollection<Post>? Posts { get; set; }
    public override string ToString() => Name;
}