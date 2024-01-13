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
using Axolotl.AspNet.Filters;
using Axolotl.AspNet.Service;
using Axolotl.EFCore.Interfaces;
using Axolotl.Interfaces;
using Axolotl.Response;

// ReSharper disable MemberCanBePrivate.Global

namespace Axolotl.AspNet.Feature;

public abstract partial class GenericFeature<TFeature, TKey>
    where TKey : notnull
    where TFeature : new() {
    protected GenericFeature<TFeature, TKey> AddGetAll<TEntity, TResponse>(RouteState state)
        where TEntity : class, IAggregateRoot
        where TResponse : class, IResponse {
        var name = state.Name ?? $"GetAll{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapGet(state.Path ?? String.Empty,
                    async (IGenericService<TEntity, TResponse, TKey> sv, [AsParameters] PagedFilter filter,
                            CancellationToken cancellationToken = default) =>
                        await sv.GetAllAsync(filter, state.Spec, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddGetById<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"Get{typeof(TResponse).Name}ById";
        if (Endpoints != null) {
            Endpoints.MapGet(state.Path ?? String.Empty + "/{id}",
                async (IGenericService<TEntity, TResponse, TKey> sv, [AsParameters] EndpointParam<TKey> parameters,
                        CancellationToken cancellationToken = default) =>
                    await sv.GetByIdAsync(parameters.Id, cancellationToken)
            ).WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddGetBySpec<TEntity, TResponse, TOption>(IEndpointRouteBuilder endpoints, RouteState state)
        where TEntity : class, IAggregateRoot
        where TResponse : class, IResponse
        where TOption : class, ISpecFilter {
        var name = state.Name ?? $"Get{typeof(TResponse).Name}BySpec";
        endpoints.MapPost(state.Path ?? "/spec",
                async (IGenericService<TEntity, TResponse, TKey> sv, [FromBody] TOption option,
                        CancellationToken cancellationToken = default) =>
                    await sv.GetBySpec(state.Spec!, option, cancellationToken))
            .WithName(name);

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddCreate<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"Create{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapPost(state.Path ?? String.Empty,
                    async (IGenericService<TEntity, TResponse, TKey> sv, TResponse value,
                            CancellationToken cancellationToken = default) =>
                        await sv.CreateAsync(value, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    public GenericFeature<TFeature, TKey> AddCreateRange<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"CreateRange{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapPost(state.Path ?? "/range",
                    async (IGenericService<TEntity, TResponse, TKey> sv, [FromBody] IEnumerable<TResponse> values,
                            CancellationToken cancellationToken = default) =>
                        await sv.CreateRangeAsync(values, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddUpdate<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"Update{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapPut(state.Path ?? String.Empty,
                    async (IGenericService<TEntity, TResponse, TKey> sv, TResponse value,
                            CancellationToken cancellationToken = default) =>
                        await sv.UpdateAsync(value, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddUpdateRange<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"UpdateRange{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapPut(state.Path ?? "/range",
                    async (IGenericService<TEntity, TResponse, TKey> sv, [FromBody] IEnumerable<TResponse> values,
                            CancellationToken cancellationToken = default) =>
                        await sv.UpdateRangeAsync(values, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddDelete<TEntity, TResponse>(RouteState state)
        where TEntity : class, IAggregateRoot
        where TResponse : class, IResponse {
        var name = state.Name ?? $"Delete{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapDelete(state.Path ?? String.Empty + "/{id}",
                    async (IGenericService<TEntity, TResponse, TKey> sv, [AsParameters] EndpointParam<TKey> parameters,
                            CancellationToken cancellationToken = default) =>
                        await sv.DeleteAsync(parameters.Id, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddDeleteRange<TEntity, TResponse>(RouteState state)
        where TEntity : class, IAggregateRoot
        where TResponse : class, IResponse {
        var name = state.Name ?? $"DeleteRange{typeof(TResponse).Name}";
        if (Endpoints != null) {
            Endpoints.MapDelete(state.Path ?? "/range",
                    async (IGenericService<TEntity, TResponse, TKey> sv, [FromBody] IEnumerable<TKey> ids,
                            CancellationToken cancellationToken = default) =>
                        await sv.DeleteRangeAsync(ids, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature, TKey> AddDeleteBySpec<TEntity, TResponse, TOption>(IEndpointRouteBuilder endpoints, RouteState state)
        where TEntity : class, IAggregateRoot
        where TResponse : class, IResponse
        where TOption : class {
        var name = state.Name ?? $"Delete{typeof(TResponse).Name}BySpec";
        endpoints.MapDelete(state.Path ?? "/spec",
                async (IGenericService<TEntity, TResponse, TKey> sv, [FromBody] TOption option,
                        CancellationToken cancellationToken = default) =>
                    await sv.DeleteBySpec(state.Spec!, option, cancellationToken))
            .WithName(name);

        return this;
    }
}