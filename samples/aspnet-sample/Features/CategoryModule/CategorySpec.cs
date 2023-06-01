// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Proton.Common.Interfaces;

namespace Proton.Common.AspNetSample.Features.CategoryModule;

public sealed class CategorySpec : Specification<Category> {
    public CategorySpec(IPageFilter filter) {
        var search = filter.Search ?? string.Empty;
        var text = search.ToLower().Split(" ").ToList().Select(x => x);
        
        Query.Where(x =>  x.Name != String.Empty && x.Name.Length > 3 && text.Any(p => EF.Functions.Like(x.Name.ToLower(), $"%" + p + "%")))
            .AsNoTracking()
            .OrderBy(b => b.Name);
    }
}