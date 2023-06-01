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
using Proton.Common.AspNet.Service;
using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.AspNet.Helpers;

public static class GenerateSpec {
    public static Specification<TEntity> Build<TEntity>(Type? spec, params object[] param) where TEntity : IAggregateRoot {
        return spec == null ? 
            new GenericSpec<TEntity>() :
            (Specification<TEntity>)Activator.CreateInstance(spec, param)!;
    }
}