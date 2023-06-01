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
using Proton.Common.AspNet.Filters;
using Proton.Common.AspNet.Service;
using Proton.Common.EFCore.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Feature;

public abstract partial class GenericFeature<TFeature> where  TFeature : new() {
    protected GenericFeature<TFeature> AddGetAll<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"GetAll{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapGet(state.Path ?? String.Empty,
                    async (IGenericService<TEntity, TResponse> sv, [AsParameters] PagedFilter filter,
                            CancellationToken cancellationToken = default) =>
                        await sv.GetAllAsync(filter, state.Spec, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature> AddGetById<TEntity, TResponse, TId>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse where TId : notnull {
        var name = state.Name ?? $"Get{typeof(TEntity).Name}ById";
        if (Endpoints != null) {
            Endpoints.MapGet(state.Path ?? String.Empty + "/{id}",
                async (IGenericService<TEntity, TResponse> sv, [AsParameters] EndpointParam<TId> parameters,
                        CancellationToken cancellationToken = default) =>
                    await sv.GetByIdAsync(parameters.Id, state.Spec, cancellationToken)
            ).WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature> AddCreate<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"Create{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapPost(state.Path ?? String.Empty,
                    async (IGenericService<TEntity, TResponse> sv, TResponse value,
                            CancellationToken cancellationToken = default) =>
                        await sv.CreateAsync(value, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    public GenericFeature<TFeature> AddCreateRange<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"CreateRange{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapPost(state.Path ?? "/range",
                    async (IGenericService<TEntity, TResponse> sv, IEnumerable<TResponse> values,
                            CancellationToken cancellationToken = default) =>
                        await sv.CreateRangeAsync(values, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature> AddUpdate<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"Update{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapPut(state.Path ?? String.Empty,
                    async (IGenericService<TEntity, TResponse> sv, TResponse value,
                            CancellationToken cancellationToken = default) =>
                        await sv.UpdateAsync(value, state.Spec, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature> AddUpdateRange<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"UpdateRange{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapPut(state.Path ?? "/range",
                    async (IGenericService<TEntity, TResponse> sv, IEnumerable<TResponse> values,
                            CancellationToken cancellationToken = default) =>
                        await sv.UpdateRangeAsync(values, state.Spec, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature> AddDelete<TEntity, TResponse, TId>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse where TId : notnull {
        var name = state.Name ?? $"Delete{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapDelete(state.Path ?? String.Empty + "/{id}",
                    async (IGenericService<TEntity, TResponse> sv, [AsParameters] EndpointParam<TId> parameters,
                            CancellationToken cancellationToken = default) =>
                        await sv.DeleteAsync(parameters.Id, state.Spec, cancellationToken))
                .WithName(name);
        }

        return this;
    }

    protected GenericFeature<TFeature> AddDeleteRange<TEntity, TResponse>(RouteState state) where TEntity : class, IAggregateRoot where TResponse : class, IResponse {
        var name = state.Name ?? $"DeleteRange{typeof(TEntity).Name}";
        if (Endpoints != null) {
            Endpoints.MapDelete(state.Path ?? "/range",
                    async (IGenericService<TEntity, TResponse> sv, IEnumerable<TResponse> values,
                            CancellationToken cancellationToken = default) =>
                        await sv.DeleteRangeAsync(values, state.Spec, cancellationToken))
                .WithName(name);
        }

        return this;
    }
}