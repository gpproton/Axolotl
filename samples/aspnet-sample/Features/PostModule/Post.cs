// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.ComponentModel.DataAnnotations;
using Proton.Common.AspNetSample.Features.CategoryModule;
using Proton.Common.AspNetSample.Features.TagModule;
using Proton.Common.EFCore.Base;

namespace Proton.Common.AspNetSample.Features.PostModule;

public sealed class Post : AuditableEntity<Guid> {
    public string Title { get; set; } = null!;
    [DataType("Markdown")]
    public string Content { get; set; } = string.Empty;
    public Category? Category { get; set; }
    [Display(AutoGenerateField = false)]
    public Guid? CategoryId { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public override string ToString() => Title;
}