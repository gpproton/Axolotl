/*
  Copyright (c) 2024 <Godwin peter. O>
  
  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:
  
  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.
  
  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
  
   Author: Godwin peter. O (me@godwin.dev)
   Created At: Wed 11 Dec 2024 23:32:18
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:32:18
*/

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Axolotl.EFCore.Interfaces;
using Axolotl.Interfaces;
using Axolotl.Response;

namespace Axolotl.AspNet.Feature;

public abstract partial class GenericFeature<TFeature, TKey> : IFeature
    where TKey : notnull
    where TFeature : new() {
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