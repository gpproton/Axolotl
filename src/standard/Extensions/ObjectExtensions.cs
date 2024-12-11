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
   Created At: Wed 11 Dec 2024 23:36:03
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:36:03
*/

using System.Web;

namespace Axolotl.Extensions;

public static class ObjectExtensions {
    private static string? EncodeUrl(System.Reflection.PropertyInfo? obj) {
        object? value = obj!.GetValue(obj, null);
        if (value is null) {
            return string.Empty;
        }

        string? result = HttpUtility.UrlEncode(value!.ToString());

        return result;
    }

    public static string GetQueryString(this object? obj) {
        if (obj == null) {
            return string.Empty;
        }

        IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                         where p.GetValue(obj, null) != null
                                         select p.Name + "=" + EncodeUrl(p);

        return "?" + String.Join("&", properties.ToArray());
    }
}
