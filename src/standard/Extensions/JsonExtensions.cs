// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.Json;

namespace Axolotl.Extensions {
    public static class JsonExtensions {
        private static readonly JsonSerializerOptions JsonOptions = new() {
            PropertyNameCaseInsensitive = true
        };

        public static T? FromJson<T>(this string json) {
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public static string ToJson<T>(this T obj) {
            return JsonSerializer.Serialize(obj, JsonOptions);
        }
    }
}