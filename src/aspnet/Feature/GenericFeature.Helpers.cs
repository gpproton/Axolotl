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
using Microsoft.AspNetCore.Routing;
using Proton.Common.AspNet.Filters;
using Proton.Common.AspNet.Service;
using Proton.Common.EFCore.Interfaces;
using Proton.Common.Enums;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Feature;

public abstract partial class GenericFeature {
    protected virtual IEndpointRouteBuilder SetupGroup<TEntity, TId>(
        IEndpointRouteBuilder endpoints,
        List<RouteType>? types = null,
        string root = "/api/v1",
        Type? spec = null)
        where TEntity : class, IAggregateRoot, IResponse
        where TId : notnull => SetupGroup<TEntity, TEntity, TId>(endpoints, types, root, spec);
    
    protected virtual IEndpointRouteBuilder SetupGroup<TEntity, TResponse, TId>(
        IEndpointRouteBuilder endpoints,
        List<RouteType>? types = null,
        string root = "/api/v1",
        Type? spec = null)
        where TEntity : class, IAggregateRoot, IResponse
        where TResponse : class, IResponse
        where TId : notnull {
        var type = typeof(TEntity);
        var name = type.Name.ToLower();
        var url = $"{root}/{name}";
        var group = endpoints.MapGroup(url).WithTags(name.Capitalize());

        var defaults = types is null;
        var active = types ?? new List<RouteType> { RouteType.GetAll, RouteType.GetById };

        if (active.Contains(RouteType.GetAll)) {
            group.MapGet(String.Empty, async (IGenericService<TEntity, TResponse> sv, [AsParameters] PagedFilter filter, CancellationToken cancellationToken = default) =>
                await sv.GetAllAsync(filter, spec, cancellationToken))
                .WithName($"GetAll{name}");
        }

        if (active.Contains(RouteType.GetById)) {
            group.MapGet("/{id}", async (IGenericService<TEntity, TResponse> sv, [AsParameters] EndpointParam<TId> parameters, CancellationToken cancellationToken = default) =>
            await sv.GetByIdAsync(parameters.Id, spec, cancellationToken)
            ).WithName($"Get{name}ById");
        }

        if (defaults) return group;

        // Create item
        if (active.Contains(RouteType.Create)) {
            group.MapPost(String.Empty, async (IGenericService<TEntity, TResponse> sv, TEntity value, CancellationToken cancellationToken = default) =>
                    await sv.CreateAsync(value, cancellationToken))
                .WithName($"Create{name}");
        }

        // Create range items
        if (active.Contains(RouteType.CreateRange)) {
            group.MapPost("/range", async (IGenericService<TEntity, TResponse> sv, IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
                    await sv.CreateRangeAsync(values, cancellationToken))
                .WithName($"CreateRange{name}");
        }

        // Update item
        if (active.Contains(RouteType.Update)) {
            group.MapPut(String.Empty, async (IGenericService<TEntity, TResponse> sv, TEntity value, CancellationToken cancellationToken = default) =>
                    await sv.UpdateAsync(value, spec, cancellationToken))
                .WithName($"Update{name}");
        }

        // Update range items
        if (active.Contains(RouteType.UpdateRange)) {
            group.MapPut("/range", async (IGenericService<TEntity, TResponse> sv, IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
                    await sv.UpdateRangeAsync(values, spec, cancellationToken))
                .WithName($"UpdateRange{name}");
        }

        // Delete item by id
        if (active.Contains(RouteType.Delete)) {
            group.MapDelete("/{id}", async (IGenericService<TEntity, TResponse> sv, [AsParameters] EndpointParam<TId> parameters, CancellationToken cancellationToken = default) =>
                await sv.DeleteAsync(parameters.Id, spec, cancellationToken))
            .WithName($"Delete{name}");
        }

        // Delete range items
        if (active.Contains(RouteType.DeleteRange)) {
            group.MapDelete("/range", async (IGenericService<TEntity, TResponse> sv, IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
                    await sv.DeleteRangeAsync(values, spec, cancellationToken))
                .WithName($"DeleteRange{name}");
        }

        return group;
    }
}