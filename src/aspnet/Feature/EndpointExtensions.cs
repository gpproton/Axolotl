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
using Microsoft.Extensions.DependencyInjection;
using Proton.Common.Helpers;

namespace Proton.Common.AspNet.Feature;

public static class EndpointExtensions {
    private static readonly List<IModule> RegisteredModules = new ();
    private static readonly List<IModule> RegisteredEndpoints = new ();

    public static IServiceCollection RegisterModules(this IServiceCollection services) {
        var modules = DiscoverModules();
        foreach (var module in modules) {
            if (!RegisteredModules.Contains(module).Equals(false))
                continue;

            module.RegisterModule(services);
            RegisteredModules.Add(module);
        }

        return services;
    }

    public static WebApplication RegisterApiEndpoints(this WebApplication app) {
        foreach (var module in RegisteredModules.Where(module => !RegisteredEndpoints.Contains(module))) {
            RegisteredEndpoints.Add(module);
            module.MapEndpoints(app);
        }

        return app;
    }

    private static IEnumerable<IModule> DiscoverModules()
        => FactoryLoader.LoadClassInstances<IModule>();
}