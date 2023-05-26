// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nextended.Core.Extensions;
using Proton.Common.AspNet.Service;
using Proton.Common.EFCore.Interfaces;
using Proton.Common.Enums;
using Proton.Common.Filters;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Feature;

public class TempParam<TId> where TId : notnull {
    public TId Id { get; set; } = default!;
}

public abstract class GenericFeature : IFeature {
    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    protected virtual IEndpointRouteBuilder SetupGroup<TEntity, TId>(IEndpointRouteBuilder endpoints, List<EndpointType>? types = null, string root = "/api/v1")
        where TEntity : class, IAggregateRoot, IResponse
        where TId : notnull {
        var type = typeof(TEntity);
        var name = type.Name.ToLower();
        var url = $"{root}/{name}";
        var group = endpoints.MapGroup(url).WithTags(name.Capitalize());

        var defaults = types is null;
        var active = types ?? new List<EndpointType> { EndpointType.GetAll, EndpointType.GetById };

        if (active.Contains(EndpointType.GetAll)) {
            group.MapGet(String.Empty, async (IGenericService<TEntity> sv, [AsParameters] PageFilter filter) =>
                await sv.GetAllAsync(filter))
                .WithName($"GetAll{name}");
        }

        if (active.Contains(EndpointType.GetById)) {
            group.MapGet("/{id}", async (IGenericService<TEntity> sv, [AsParameters] TempParam<TId> parameters) =>
            await sv.GetByIdAsync(parameters.Id)
            ).WithName($"Get{name}ById");
        }

        if (defaults) return group;

        // Create item
        if (active.Contains(EndpointType.Create)) {
            group.MapPost(String.Empty, async (IGenericService<TEntity> sv, TEntity value) =>
                    await sv.CreateAsync(value))
                .WithName($"Create{name}");
        }

        // Create multiple items
        if (active.Contains(EndpointType.CreateRange)) {
            group.MapPost("/multiple", async (IGenericService<TEntity> sv, IEnumerable<TEntity> values) =>
                    await sv.CreateRangeAsync(values))
                .WithName($"CreateMultiple{name}");
        }

        // Update item
        if (active.Contains(EndpointType.Update)) {
            group.MapPut(String.Empty, async (IGenericService<TEntity> sv, TEntity value) =>
                    await sv.UpdateAsync(value))
                .WithName($"Update{name}");
        }

        // Update multiple items
        if (active.Contains(EndpointType.UpdateRange)) {
            group.MapPut("/multiple", async (IGenericService<TEntity> sv, IEnumerable<TEntity> values) =>
                    await sv.UpdateRangeAsync(values))
                .WithName($"UpdateMultiple{name}");
        }

        // Delete item by id
        if (active.Contains(EndpointType.Delete)) {
            group.MapDelete("/{id}", async (IGenericService<TEntity> sv, [AsParameters] TempParam<TId> parameters) =>
                await sv.GetByIdAsync(parameters.Id))
            .WithName($"Delete{name}");
        }

        // Delete multiple items
        if (active.Contains(EndpointType.DeleteRange)) {
            group.MapDelete("/multiple", async (IGenericService<TEntity> sv, IEnumerable<TEntity> values) =>
                    await sv.DeleteRangeAsync(values))
                .WithName($"DeleteMultiple{name}");
        }

        return group;
    }
}