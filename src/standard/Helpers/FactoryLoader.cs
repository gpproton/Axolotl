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

namespace Proton.Common.Helpers {
    public static class FactoryLoader {
        public static IEnumerable<T> LoadClassInstances<T>(Assembly? assembly = null) =>
            ResolveAssembly<T>(assembly)
                .Select(Activator.CreateInstance)
                .Cast<T>();

        public static IEnumerable<string> LoadClassNames<T>(Assembly? assembly = null) =>
            ResolveAssembly<T>(assembly).Select(t => t.Name);

        private static IEnumerable<Type> ResolveAssembly<T>(Assembly? assembly = null) {
            var type = typeof(T);
            var value = assembly ?? type.Assembly;

            return value
                .GetTypes()
                .Where(p => p is { IsClass: true, IsAbstract: false } && p.IsAssignableTo(type));
        }
    }
}