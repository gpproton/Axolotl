// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.EFCore.Context;

public static class SoftDeleteQueryExtension {
    public static void AddSoftDeleteQueryFilter(
        this IMutableEntityType entityData) {
        var methodToCall = typeof(SoftDeleteQueryExtension)
            .GetMethod(nameof(GetSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            ?.MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall?.Invoke(null, new object[] { });
        entityData.SetQueryFilter((LambdaExpression)filter!);
        entityData.AddIndex(entityData.
            FindProperty(nameof(IAuditableEntity.DeletedAt))!);
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
        where TEntity : IAuditableEntity {
        Expression<Func<TEntity, bool>> filter = x => x.DeletedAt == null;
        return filter;
    }
}