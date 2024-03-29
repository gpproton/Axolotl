// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.EntityFrameworkCore;
namespace Axolotl.AspNetSample.Data;

public static class DatabaseExtensions {
    public static IServiceCollection RegisterDataContext(this IServiceCollection services) {
        var sp = services.BuildServiceProvider();
        var config = sp.GetService<IConfiguration>();
        
        services.AddDbContext<ServiceContext>(options =>
            options.UseSqlite(config!.GetConnectionString("DefaultConnection"),
                b => b
                    .MigrationsAssembly(typeof(ServiceContext).Assembly.FullName)
                    .UseRelationalNulls()
            ).EnableSensitiveDataLogging()
        );

        return services;
    }
}