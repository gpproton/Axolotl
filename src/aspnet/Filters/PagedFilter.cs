// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Reflection;
using Microsoft.AspNetCore.Http;
using Proton.Common.Filters;
using Proton.Common.Interfaces;

namespace Proton.Common.AspNet.Filters;

public class PagedFilter : PageFilter, IPageFilter {
    public PagedFilter() { }
    public PagedFilter(int page = 1, int pageSize = 25, string search = "") {
        Page = page;
        PageSize = pageSize;
        Search = search;
    }
    
    public static ValueTask<PageFilter?> BindAsync(HttpContext httpContext, ParameterInfo parameter) {
        int.TryParse(httpContext.Request.Query["page"], out var page);
        int.TryParse(httpContext.Request.Query["page-size"], out var pageSize);
        var search = httpContext.Request.Query["search"].ToString();
        return ValueTask.FromResult<PageFilter?>(
            new PagedFilter(
                page == 0 ? 1 : page,
                pageSize == 0 ? 10 : pageSize,
                search
            )
        );
    }
}