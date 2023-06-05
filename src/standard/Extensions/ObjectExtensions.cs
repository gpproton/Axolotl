// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Web;

namespace Axolotl.Extensions {
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
}