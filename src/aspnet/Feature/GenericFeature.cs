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

namespace Proton.Common.AspNet.Feature;

public abstract partial class GenericFeature<TFeature> : IFeature where  TFeature : new() {
    private IEndpointRouteBuilder? Endpoints { get; set; }
    
    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    protected GenericFeature<TFeature> AddGetAll<TEntity>(RouteState state) where TEntity : IAggregateRoot {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddGetById<TEntity, TId>(RouteState state) where TEntity : IAggregateRoot where TId : notnull {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddCreate<TEntity>(RouteState state) where TEntity : IAggregateRoot {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddCreateRange<TEntity>(RouteState state) where TEntity : IAggregateRoot {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddUpdate<TEntity>(RouteState state) where TEntity : IAggregateRoot {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddUpdateRange<TEntity>(RouteState state) where TEntity : IAggregateRoot {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddDelete<TEntity, TId>(RouteState state) where TEntity : IAggregateRoot where TId : notnull {
        throw new NotImplementedException();
    }

    protected GenericFeature<TFeature> AddDeleteRange<TEntity>(RouteState state) where TEntity : IAggregateRoot {
        throw new NotImplementedException();
    }
}