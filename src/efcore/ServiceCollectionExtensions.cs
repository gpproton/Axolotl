// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.EFCore.Implementation;
using Axolotl.EFCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Axolotl.EFCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.EFCore;

public static class ServiceCollectionExtensions {
    public static IServiceCollection RegisterGenericRepositories(this IServiceCollection services, Type repositoryType) {
        services.AddScoped(typeof(IRepository<,>), repositoryType);
        
        return services;
    }
    
    public static IServiceCollection RegisterUnitOfWork<TContext>(this IServiceCollection services, bool pooled = false) where TContext : DbContext {
        services.AddScoped(typeof(IUnitOfWork<>), pooled ? typeof(PooledUnitOfWork<>) : typeof(UnitOfWork<>));
        services.AddScoped(typeof(IUnitOfWork<TContext>), pooled ? typeof(PooledUnitOfWork<TContext>) : typeof(UnitOfWork<TContext>));

        return services;
    }
}