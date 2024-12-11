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
   Created At: Wed 11 Dec 2024 23:32:29
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:32:29
*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Axolotl.EFCore.Interfaces;
using Axolotl.Enums;
using Axolotl.Response;

namespace Axolotl.AspNet.Feature;

public abstract partial class GenericFeature<TFeature, TKey>
    where TKey : notnull
    where TFeature : new() {
    protected static IEndpointRouteBuilder SetupGroup<TAFeature, TEntity, TResponse>(IEndpointRouteBuilder endpoints, FeatureState? state = null)
        where TAFeature : GenericFeature<TFeature, TKey>
        where TEntity : class, IAggregateRoot, IResponse
        where TResponse : class, IResponse {
        var options = state ?? new FeatureState(new List<RouteState> {
            new(RouteType.GetAll),
            new(RouteType.GetById)
        });

        var type = typeof(TResponse);
        var root = options.Root;
        var name = options.Name ?? type.Name.ToLower();
        var url = options.Path ?? $"{root}/{name}";
        var instance = new TFeature() as TAFeature;
        var group = endpoints.MapGroup(url).WithTags(name);
        if (instance! == null) throw new Exception("Feature instance not initialized.");
        instance.Endpoints = group;

        foreach (var config in options.State) {
            switch (config.Type) {
                case RouteType.GetAll:
                    instance.AddGetAll<TEntity, TResponse>(config);
                    break;
                case RouteType.GetById:
                    instance.AddGetById<TEntity, TResponse>(config);
                    break;
                case RouteType.Create:
                    instance.AddCreate<TEntity, TResponse>(config);
                    break;
                case RouteType.CreateRange:
                    instance.AddCreateRange<TEntity, TResponse>(config);
                    break;
                case RouteType.Update:
                    instance.AddUpdate<TEntity, TResponse>(config);
                    break;
                case RouteType.UpdateRange:
                    instance.AddUpdateRange<TEntity, TResponse>(config);
                    break;
                case RouteType.Delete:
                    instance.AddDelete<TEntity, TResponse>(config);
                    break;
                case RouteType.DeleteRange:
                    instance.AddDeleteRange<TEntity, TResponse>(config);
                    break;
            }
        }

        return instance.Endpoints;
    }

    protected static IEndpointRouteBuilder SetupGroup<TAFeature, TEntity>(IEndpointRouteBuilder endpoints, FeatureState? state = null)
        where TAFeature : GenericFeature<TFeature, TKey>
        where TEntity : class, IAggregateRoot, IResponse =>
        SetupGroup<TAFeature, TEntity, TEntity>(endpoints, state);
}