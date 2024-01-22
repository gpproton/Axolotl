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