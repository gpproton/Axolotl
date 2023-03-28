// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Specialized;
using System.Web;

namespace Proton.Common.Standard.Extensions;

public static class NameValueExtensions {
    public static string ToQueryString(this NameValueCollection? source, bool removeEmptyEntries = false) {
        if (source == null) return string.Empty;

        return "?" + String.Join("&", source.AllKeys
               .Where(key => !removeEmptyEntries || source.GetValues(key)!.Any(value => !String.IsNullOrEmpty(value)))
               .SelectMany(key => source.GetValues(key)!
               .Where(value => !removeEmptyEntries || !String.IsNullOrEmpty(value))
               .Select(value => $"{HttpUtility.UrlEncode(key)}={(HttpUtility.UrlEncode(value))}"))
               .ToArray());
    }
}