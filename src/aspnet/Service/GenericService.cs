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

public sealed class GenericService<TEntity>(IRepository<TEntity> repo) :
    IGenericService<TEntity> where TEntity : class, IAggregateRoot {

    public async Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter) {
        var count = await repo.GetQueryable().CountAsync();
        var result = await repo.GetAll(new GenericListSpec<TEntity>(filter)).ToListAsync();
        var page = filter!.Page ?? 1;
        var size = filter!.Size ?? 25;
        return new PagedResponse<TEntity>(result, page, size, count);
    }

    public async Task<Response<TEntity?>> GetByIdAsync<TId>(TId id) where TId : notnull {
        var result = await repo.GetByIdAsync(id);
        return new Response<TEntity?>(result);
    }

    public async Task<Response<TEntity>> CreateAsync(TEntity value) {
        var result = await repo.AddAsync(value);
        return new Response<TEntity>(result);
    }

    public async Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values) {
        var result = await repo.AddRangeAsync(values);
        return new PagedResponse<TEntity>(result);
    }

    public async Task<Response<TEntity?>> UpdateAsync(TEntity value) {
        await repo.UpdateAsync(value);
        return new Response<TEntity?>(value);
    }

    public async Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values) {
        IEnumerable<TEntity> aggregateRoots = values.ToList();
        await repo.UpdateRangeAsync(aggregateRoots);

        return new PagedResponse<TEntity>(aggregateRoots);
    }

    public async Task<Response<TEntity?>> DeleteAsync<TId>(TId id) where TId : notnull {
        var item = await repo.GetByIdAsync(id);
        if (item is not null) await repo.DeleteAsync(item);

        return new Response<TEntity?>(item, "", item != null);
    }

    public async Task<PagedResponse<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> values) {
        IEnumerable<TEntity> aggregateRoots = values.ToList();
        await repo.DeleteRangeAsync(aggregateRoots);

        return new PagedResponse<TEntity>(aggregateRoots);
    }

    public async Task ClearAsync() {
        await repo.ClearAsync();
    }
}