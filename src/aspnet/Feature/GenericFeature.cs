// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Proton.Common.EFCore.Interfaces;
using Proton.Common.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Feature;

public abstract partial class GenericFeature<TFeature> : IFeature where  TFeature : new() {
    protected IEndpointRouteBuilder? Endpoints { get; set; }

    protected IEndpointRouteBuilder GetEndpoints() => Endpoints!;
    
    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    protected GenericFeature<TFeature> AddGetAll<TEntity>(RouteState state) where TEntity : class, IAggregateRoot, IResponse =>
        this.AddGetAll<TEntity, TEntity>(state);

    protected GenericFeature<TFeature> AddGetById<TEntity, TId>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse where TId : notnull =>
        this.AddGetById<TEntity, TEntity, TId>(state);

    protected GenericFeature<TFeature> AddGetBySpec<TEntity, TOption>(IEndpointRouteBuilder endpoints,
        RouteState state)
        where TEntity : class, IAggregateRoot, IResponse
        where TOption : class, ISpecFilter =>
        this.AddGetBySpec<TEntity, TEntity, TOption>(endpoints, state);

    protected GenericFeature<TFeature> AddCreate<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse => this.AddCreate<TEntity, TEntity>(state);

    protected GenericFeature<TFeature> AddCreateRange<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddCreateRange<TEntity, TEntity>(state);

    protected GenericFeature<TFeature> AddUpdate<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddUpdate<TEntity, TEntity>(state);

    protected GenericFeature<TFeature> AddUpdateRange<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddUpdateRange<TEntity, TEntity>(state);

    protected GenericFeature<TFeature> AddDelete<TEntity, TId>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse where TId : notnull =>
        this.AddDelete<TEntity, TEntity, TId>(state);

    protected GenericFeature<TFeature> AddDeleteRange<TEntity, TId>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse
        where TId : notnull =>
        this.AddDeleteRange<TEntity, TEntity, TId>(state);

    public GenericFeature<TFeature> AddDeleteBySpec<TEntity, TOption>(IEndpointRouteBuilder endpoints, RouteState state)
        where TEntity : class, IAggregateRoot, IResponse
        where TOption : class =>
        this.AddDeleteBySpec<TEntity, TEntity, TOption>(endpoints, state);
}