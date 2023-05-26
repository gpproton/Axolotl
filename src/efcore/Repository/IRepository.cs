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
using Microsoft.EntityFrameworkCore;
using Proton.Common.EFCore.Interfaces;

namespace Proton.Common.EFCore.Repository;

public interface IRepository<TEntity> : IReadRepository<TEntity>, IRepositoryBase<TEntity> where TEntity : class, IAggregateRoot {
    IQueryable<TEntity> GetQueryable(CancellationToken cancellationToken = default);
    DbSet<TEntity> GetContext(CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken = default);
}