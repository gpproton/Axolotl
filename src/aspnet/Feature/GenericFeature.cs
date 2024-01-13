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
using Axolotl.EFCore.Interfaces;
using Axolotl.Interfaces;
using Axolotl.Response;

namespace Axolotl.AspNet.Feature;

public abstract partial class GenericFeature<TFeature, TKey> : IFeature
    where TKey : notnull
    where  TFeature : new() {
    protected IEndpointRouteBuilder? Endpoints { get; set; }

    protected IEndpointRouteBuilder GetEndpoints() => Endpoints!;
    
    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    protected GenericFeature<TFeature, TKey> AddGetAll<TEntity>(RouteState state) where TEntity : class, IAggregateRoot, IResponse =>
        this.AddGetAll<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddGetById<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddGetById<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddGetBySpec<TEntity, TOption>(IEndpointRouteBuilder endpoints,
        RouteState state)
        where TEntity : class, IAggregateRoot, IResponse
        where TOption : class, ISpecFilter =>
        this.AddGetBySpec<TEntity, TEntity, TOption>(endpoints, state);

    protected GenericFeature<TFeature, TKey> AddCreate<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse => this.AddCreate<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddCreateRange<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddCreateRange<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddUpdate<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddUpdate<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddUpdateRange<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddUpdateRange<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddDelete<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddDelete<TEntity, TEntity>(state);

    protected GenericFeature<TFeature, TKey> AddDeleteRange<TEntity>(RouteState state)
        where TEntity : class, IAggregateRoot, IResponse =>
        this.AddDeleteRange<TEntity, TEntity>(state);

    public GenericFeature<TFeature, TKey> AddDeleteBySpec<TEntity, TOption>(IEndpointRouteBuilder endpoints, RouteState state)
        where TEntity : class, IAggregateRoot, IResponse
        where TOption : class =>
        this.AddDeleteBySpec<TEntity, TEntity, TOption>(endpoints, state);
}