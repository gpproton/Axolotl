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
using Proton.Common.EFCore.Interfaces;
using Proton.Common.EFCore.Repository;
using Proton.Common.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Service;

public class GenericService<TEntity, TResponse> (IRepository<TEntity> repo) :
    IGenericService<TEntity, TResponse> 
    where TEntity : class, IAggregateRoot
    where TResponse : class, IResponse {
    public async Task<PagedResponse<TResponse>> GetAllAsync(IPageFilter? filter) {
        var count = await repo.GetQueryable().CountAsync();
        var result = await repo.GetAll(new GenericListSpec<TEntity>(filter)).ToListAsync();
        var output = result.MapTo<List<TResponse>>();
        var page = filter!.Page ?? 1;
        var size = filter.Size ?? 25;
        
        return new PagedResponse<TResponse>(output, page, size, count);
    }

    public async Task<Response<TResponse?>> GetByIdAsync<TId>(TId id) where TId : notnull {
        var result = await repo.GetByIdAsync(id);
        var output = result.MapTo<TResponse?>();
        
        return new Response<TResponse?>(output);
    }

    public async Task<Response<TResponse>> CreateAsync(IResponse value) {
        var result = await repo.AddAsync(value.MapTo<TEntity>());
        var output = result.MapTo<TResponse>();
        
        return new Response<TResponse>(output);
    }

    public async Task<PagedResponse<TResponse>> CreateRangeAsync(IEnumerable<IResponse> values) {
        var convertedValues = values.MapTo<IEnumerable<TEntity>>();
        var result = await repo.AddRangeAsync(convertedValues);
        var output = result.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task<Response<TResponse>> UpdateAsync(IResponse value) {
        await repo.UpdateAsync(value.MapTo<TEntity>());
        
        return new Response<TResponse>((TResponse?)value);
    }

    public async Task<PagedResponse<TResponse>> UpdateRangeAsync(IEnumerable<IResponse> values) {
        var valueConverted = values.MapTo<IEnumerable<TEntity>>();
        var valueAggregate = valueConverted.ToList();
        await repo.UpdateRangeAsync(valueAggregate);
        var output = valueAggregate.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task<Response<TResponse?>> DeleteAsync<TId>(TId id) where TId : notnull {
        var item = await repo.GetByIdAsync(id);
        if (item is not null) await repo.DeleteAsync(item);
        var output = item.MapTo<TResponse>();
        
        return new Response<TResponse?>(output, "", item != null);
    }

    public async Task<PagedResponse<TResponse>> DeleteRangeAsync(IEnumerable<IResponse> values) {
        var valueConverted = values.MapTo<IEnumerable<TEntity>>();
        var valueAggregate = valueConverted.ToList();
        await repo.DeleteRangeAsync(valueAggregate);
        var output = valueAggregate.MapTo<IEnumerable<TResponse>>();
        
        return new PagedResponse<TResponse>(output);
    }

    public async Task ClearAsync() => await repo.ClearAsync();
}