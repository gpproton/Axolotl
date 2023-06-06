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
using Axolotl.EFCore.Converters;
using Microsoft.EntityFrameworkCore;
using Axolotl.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Axolotl.EFCore.Context;

public static class ModelBuilderExtensions {
    public static void RegisterAllEntities<TEntity>(this ModelBuilder modelBuilder, params Assembly[] assemblies) 
    where TEntity : class, IAggregateRoot
    {
        IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true } &&
            typeof (TEntity).IsAssignableFrom(c));
        foreach(Type type in types)
            modelBuilder.Entity(type);
    }

    public static void RegisterSoftDeleteFilter(this ModelBuilder modelBuilder) {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                entityType.AddSoftDeleteQueryFilter();
    }

    public static void RegisterEnumConverters<TEnum>(this ModelBuilder modelBuilder, Type type)
    where TEnum : Enum
    {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == type);
                foreach (var property in properties)
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new EnumCollectionJsonValueConverter<TEnum>())
                        .Metadata.SetValueComparer(new CollectionValueComparer<TEnum>());
            }
    }
    
    public static void RegisterObjectConverters<TObject>(this ModelBuilder modelBuilder, Type type)
        where TObject : Enum
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == type);
            foreach (var property in properties)
                modelBuilder
                    .Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(new JsonValueConverter<TObject>())
                    .Metadata.SetValueComparer(new CollectionValueComparer<TObject>());
        }
    }

    public static void DateTimeOffsetToBinary(this ModelBuilder modelBuilder) {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(DateTimeOffset)
                            || p.PropertyType == typeof(DateTimeOffset?));
            foreach (var property in properties)
                modelBuilder
                    .Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(new DateTimeOffsetToBinaryConverter());
        }
    }
}