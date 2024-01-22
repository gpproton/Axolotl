// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Axolotl.AspNet.Feature;
using Axolotl.AspNet.Service;
using Axolotl.Helpers;

namespace Axolotl.AspNet;

public static class ServiceCollectionExtensions {
    private static readonly List<IFeature> RegisteredFeatures = [];
    private static readonly List<IFeature> RegisteredEndpoints = [];

    public static IServiceCollection RegisterFeatures(this IServiceCollection services, Assembly assembly) {
        var modules = FactoryLoader.LoadClassInstances<IFeature>(assembly);
        foreach (var module in modules) {
            if (!RegisteredFeatures.Contains(module).Equals(false))
                continue;

            module.RegisterModule(services);
            RegisteredFeatures.Add(module);
        }

        return services;
    }

    public static WebApplication RegisterFeatureEndpoints(this WebApplication app) {
        foreach (var module in RegisteredFeatures.Where(module => !RegisteredEndpoints.Contains(module))) {
            RegisteredEndpoints.Add(module);
            module.MapEndpoints(app);
        }

        return app;
    }

    public static IServiceCollection RegisterGenericServices(this IServiceCollection services) {
        Action<JsonOptions> jsonOptions = options => {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            var jsonConverters =
                AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(x => !x.IsDynamic)
                    .SelectMany(x => x.ExportedTypes)
                    .Where(x => x.BaseType != null)
                    .Where(x => x.BaseType!.IsGenericType)
                    .Where(x => x.BaseType!.BaseType == typeof(JsonConverter));

            foreach (var jsonConverter in jsonConverters)
                if (Activator.CreateInstance(jsonConverter) is JsonConverter instance)
                    options.SerializerOptions.Converters.Add(instance);
        };
        
        services.Configure(jsonOptions);
        services.ConfigureHttpJsonOptions(jsonOptions);
        
        services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));

        return services;
    }
}