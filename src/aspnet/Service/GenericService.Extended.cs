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
using Axolotl.AspNet.Helpers;
using Axolotl.EFCore.Interfaces;
using Axolotl.EFCore.Repository;
using Axolotl.Filters;
using Axolotl.Interfaces;
using Axolotl.Response;

namespace Axolotl.AspNet.Service;

public class GenericService<TEntity, TResponse> (IRepository<TEntity> repo) :
    IGenericService<TEntity, TResponse> 
    where TEntity : class, IAggregateRoot, IHasKey, IResponse
    where TResponse : class, IResponse {
    
    public async Task<PagedResponse<TResponse>> PageFilter(ISpecification<TEntity> specification, int? checkPage, int? checkSize, CancellationToken cancellationToken = default) {
        int page = 1;
        int size = 25;
        if (checkPage is not null && checkPage != 0) page = (int)checkPage;
        if (checkSize is not null && checkSize != 0) size = (int)checkSize;
        
        var count = await repo.GetQueryable().WithSpecification(specification).CountAsync(cancellationToken);
        var take = (page - 1) * size;
        var result = await repo.GetQueryable().WithSpecification(specification).Take(size).Skip(take).ToListAsync(cancellationToken);
        var output = result.MapTo<List<TResponse>>();

        return new PagedResponse<TResponse>(output, page, size, count);
    }
    
    public async Task<PagedResponse<TResponse>> GetAllAsync(IPageFilter? filter, Type? spec = null, CancellationToken cancellationToken = default) {
        var specification = GenerateSpec.Build<TEntity>(spec, new PageFilter {
            Page = filter!.Page ?? 1,
            Size = filter.Size ?? 25,
            Search = filter.Search ?? string.Empty
        });

        return await PageFilter(specification, filter.Page, filter.Size, cancellationToken);
    }

    public async Task<Response<TResponse?>> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var result = await repo.GetByIdAsync(id, cancellationToken);
        var output = result.MapTo<TResponse?>();
        
        return new Response<TResponse?>(output);
    }

    public async Task<PagedResponse<TResponse>> GetBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class, ISpecFilter {
        var specification = GenerateSpec.Build<TEntity>(spec, option);
        
        return await PageFilter(specification, option.Filter.Page, option.Filter.Size, cancellationToken);
    }

    public async Task<Response<TResponse>> CreateAsync(IResponse value, CancellationToken cancellationToken = default) {
        var result = await repo.AddAsync(value.MapTo<TEntity>(), cancellationToken);
        var output = result.MapTo<TResponse>();
        
        return new Response<TResponse>(output);
    }

    public async Task<PagedResponse<TResponse>> CreateRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default) {
        var converted = values.MapTo<IEnumerable<TEntity>>().ToList();
        var result = await repo.AddRangeAsync(converted, cancellationToken);
        var output = result.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task<Response<TResponse>> UpdateAsync(IResponse value, CancellationToken cancellationToken = default) {
        var result = await repo.UpdateAsync(value.MapTo<TEntity>(), cancellationToken);
        var output = result.MapTo<TResponse>();
        
        return new Response<TResponse>(output);
    }

    public async Task<PagedResponse<TResponse>> UpdateRangeAsync(IEnumerable<IResponse> values, CancellationToken cancellationToken = default) {
        var converted = values.MapTo<IEnumerable<TEntity>>().ToList();
        var result = await repo.UpdateRangeAsync(converted, cancellationToken);
        var output = result.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task<Response<TResponse?>> DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull {
        var result = await repo.DeleteAsync(id, cancellationToken);
        var output = result.MapTo<TResponse?>();
        
        return new Response<TResponse?>(output, "", result != null);
    }

    public async Task<Response<int>> DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default) where TId : notnull {
        var result = await repo.DeleteRangeAsync(ids, cancellationToken);
        
        return new Response<int>(result);
    }

    public async Task<PagedResponse<TResponse>> DeleteBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class {
        var specification = GenerateSpec.Build<TEntity>(spec, option);
        var result = await repo.DeleteBySpec(specification, cancellationToken);
        var output = result.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default) => await repo.ClearAsync(cancellationToken);
}