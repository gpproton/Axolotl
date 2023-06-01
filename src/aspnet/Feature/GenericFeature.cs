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

namespace Proton.Common.AspNet.Feature;

internal sealed class EndpointParam<TId> where TId : notnull {
    public TId Id { get; set; } = default!;
}

public abstract partial class GenericFeature : IFeature {
    public virtual IServiceCollection RegisterModule(IServiceCollection services) => services;

    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    
}