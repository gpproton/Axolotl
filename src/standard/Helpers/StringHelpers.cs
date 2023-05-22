// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Proton.Common.Helpers {
    public static class StringHelpers {
        public static string GetInitialsText(string?[] value) {
            if (value.Length < 2) {
                return "FL";
            }

            string? f = value[0]?.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            string? l = value[^1]?.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            if (f == null || l == null) {
                return "FL";
            }

            try {
                return string.Concat(f[0], l[0]);
            }
            catch (Exception) {
                // ignore
            }
            return string.Empty;
        }
    }
}