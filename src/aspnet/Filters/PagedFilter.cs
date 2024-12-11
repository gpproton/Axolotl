/*
  Copyright (c) 2024 <Godwin peter. O>
  
  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:
  
  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.
  
  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
  
   Author: Godwin peter. O (me@godwin.dev)
   Created At: Wed 11 Dec 2024 23:33:25
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:33:25
*/

using System.Reflection;
using Microsoft.AspNetCore.Http;
using Axolotl.Filters;
using Axolotl.Interfaces;

namespace Axolotl.AspNet.Filters;

public class PagedFilter : PageFilter, IPageFilter {
    public PagedFilter() { }
    public PagedFilter(int page = 1, int size = 25, string search = "") {
        Page = page;
        Size = size;
        Search = search;
    }

    public static ValueTask<PageFilter?> BindAsync(HttpContext httpContext, ParameterInfo parameter) {
        int.TryParse(httpContext.Request.Query["page"], out var page);
        int.TryParse(httpContext.Request.Query["size"], out var size);
        var search = httpContext.Request.Query["search"].ToString();
        return ValueTask.FromResult<PageFilter?>(
            new PagedFilter(
                page == 0 ? 1 : page,
                size == 0 ? 10 : size,
                search
            )
        );
    }
}