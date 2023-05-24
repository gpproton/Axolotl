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
using Microsoft.Extensions.DependencyInjection;
using Nextended.Core.Extensions;
using Proton.Common.AspNet.Service;
using Proton.Common.EFCore.Interfaces;
using Proton.Common.Enums;
using Proton.Common.Filters;

namespace Proton.Common.AspNet.Feature;

public abstract class GenericFeature : IFeature {
    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    protected virtual IEndpointRouteBuilder SetupGroup<TEntity>(IEndpointRouteBuilder endpoints, List<HttpEndpoint>? types = null, string root = "/api/v1") where TEntity : class, IAggregateRoot {
        var type = typeof(TEntity);
        var name = type.Name.ToLower();
        var url = $"{root}/{name}";
        var group = endpoints.MapGroup(url).WithTags(name.Capitalize());
        
        if (types is null || types!.Contains(HttpEndpoint.GetAll)) {
            group.MapGet("/", async (IGenericService<TEntity> sv, [AsParameters] PageFilter filter) =>
                await sv.GetAllAsync(filter))
                .WithName($"GetAll{name}");
        }
        
        if (types is null || types.Contains(HttpEndpoint.GetById)) {
            group.MapGet("/{id}", async (IGenericService<TEntity> sv, Guid id) =>
                await sv.GetByIdAsync(id))
                .WithName($"Get{name}ById");
        }

        return group;
    }
}