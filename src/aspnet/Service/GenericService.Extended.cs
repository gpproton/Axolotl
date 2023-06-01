// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proton.Common.EFCore.Interfaces;
using Proton.Common.EFCore.Repository;
using Proton.Common.Filters;
using Proton.Common.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Service;

public class GenericService<TEntity, TResponse> (IRepository<TEntity> repo) :
    IGenericService<TEntity, TResponse> 
    where TEntity : class, IAggregateRoot
    where TResponse : class, IResponse {
    public async Task<PagedResponse<TResponse>> GetAllAsync(IPageFilter? filter, Type? type, CancellationToken cancellationToken = default) {
        var check = new PageFilter {
            Page = filter!.Page ?? 1,
            Size = filter.Size ?? 25,
            Search = filter.Search ?? string.Empty
        };

        var page = (int)check.Page;
        var size = (int)check.Size;
        var specification = type == null ? 
            new GenericListSpec<TEntity>() : 
            (Specification<TEntity>)Activator.CreateInstance(type, check)!;
        var count = await repo.GetQueryable().WithSpecification(specification).CountAsync(cancellationToken);
        var result = await repo.GetQueryable()
            .WithSpecification(specification)
            .Take(size)
            .Skip(page - 1 * size)
            .ToListAsync(cancellationToken);
        
        var output = result.MapTo<List<TResponse>>();

        return new PagedResponse<TResponse>(output, page, size, count);
    }

    public async Task<Response<TResponse?>> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var result = await repo.GetByIdAsync(id, cancellationToken);
        var output = result.MapTo<TResponse?>();
        
        return new Response<TResponse?>(output);
    }

    public async Task<Response<TResponse>> CreateAsync(IResponse value, CancellationToken cancellationToken = default) {
        var result = await repo.AddAsync(value.MapTo<TEntity>(), cancellationToken);
        var output = result.MapTo<TResponse>();
        
        return new Response<TResponse>(output);
    }

    public async Task<PagedResponse<TResponse>> CreateRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default) {
        var convertedValues = values.MapTo<IEnumerable<TEntity>>();
        var result = await repo.AddRangeAsync(convertedValues, cancellationToken);
        var output = result.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task<Response<TResponse>> UpdateAsync(IResponse value, CancellationToken cancellationToken = default) {
        await repo.UpdateAsync(value.MapTo<TEntity>(), cancellationToken);
        
        return new Response<TResponse>((TResponse?)value);
    }

    public async Task<PagedResponse<TResponse>> UpdateRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default) {
        var valueConverted = values.MapTo<IEnumerable<TEntity>>();
        var valueAggregate = valueConverted.ToList();
        await repo.UpdateRangeAsync(valueAggregate, cancellationToken);
        var output = valueAggregate.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task<Response<TResponse?>> DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var item = await repo.GetByIdAsync(id, cancellationToken);
        if (item is not null) await repo.DeleteAsync(item, cancellationToken);
        var output = item.MapTo<TResponse>();
        
        return new Response<TResponse?>(output, "", item != null);
    }

    public async Task<PagedResponse<TResponse>> DeleteRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default) {
        var valueConverted = values.MapTo<IEnumerable<TEntity>>();
        var valueAggregate = valueConverted.ToList();
        await repo.DeleteRangeAsync(valueAggregate, cancellationToken);
        var output = valueAggregate.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default) => await repo.ClearAsync(cancellationToken);
}