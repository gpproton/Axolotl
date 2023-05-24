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
using Proton.Common.Filters;
using Proton.Common.Interfaces;

namespace Proton.Common.AspNet.Service;

public sealed class GenericListSpec<TEntity> : Specification<TEntity> {
    public GenericListSpec(IPageFilter? filter) {
        var check = filter ?? new PageFilter();
        var page = check.Page ?? 1;
        var size = check.Size ?? 25;
        // var search = check.Search?.Split(" ").ToList().Select(x => x.ToLower());
        // Query.Take(size).Take(page - 1 * size);
    }
}