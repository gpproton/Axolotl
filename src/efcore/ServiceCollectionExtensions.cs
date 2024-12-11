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
   Created At: Wed 11 Dec 2024 20:42:55
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 20:42:55
*/

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