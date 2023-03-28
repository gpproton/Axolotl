// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Proton.Common.Entity.Converters;

public class EnumCollectionJsonValueConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum {
    public EnumCollectionJsonValueConverter() : base(
        v => JsonConvert
        .SerializeObject(v.Select(e => e.ToString()).ToList()),
        v => JsonConvert
        .DeserializeObject<ICollection<string>>(v)!
        .Select(e => (T) Enum.Parse(typeof(T), e)).ToList()) { }
}