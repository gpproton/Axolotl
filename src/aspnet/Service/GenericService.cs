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
   Created At: Wed 11 Dec 2024 23:34:23
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:34:23
*/

using Ardalis.Specification;
using Axolotl.EFCore.Interfaces;
using Axolotl.Interfaces;
using Axolotl.Response;

namespace Axolotl.AspNet.Service;

public sealed class GenericService<TEntity, TKey>(IGenericService<TEntity, TEntity, TKey> root) :
    IGenericService<TEntity, TKey>
    where TEntity : class, IAggregateRoot, IHasKey<TKey>, IResponse
    where TKey : notnull {
    public Task<PagedResponse<TEntity>> PageFilter(ISpecification<TEntity> specification, int? checkPage,
        int? checkSize,
        CancellationToken cancellationToken = default) =>
        root.PageFilter(specification, checkPage, checkSize, cancellationToken);

    public async Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter, Type? spec, CancellationToken cancellationToken = default) =>
        await root.GetAllAsync(filter, spec, cancellationToken);

    public async Task<Response<TEntity?>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default) =>
        await root.GetByIdAsync(id, cancellationToken);

    public async Task<PagedResponse<TEntity>> GetBySpec<TOption>(Type spec, TOption option,
        CancellationToken cancellationToken = default) where TOption : class, ISpecFilter =>
        await root.GetBySpec(spec, option, cancellationToken);

    public async Task<Response<TEntity>> CreateAsync(TEntity value, CancellationToken cancellationToken = default) =>
        await root.CreateAsync(value, cancellationToken);

    public async Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
        await root.CreateRangeAsync(values, cancellationToken);

    public async Task<Response<TEntity>> UpdateAsync(TEntity value, CancellationToken cancellationToken = default) =>
        await root.UpdateAsync(value, cancellationToken);

    public async Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default) =>
        await root.UpdateRangeAsync(values, cancellationToken);

    public async Task<Response<TEntity?>> DeleteAsync(TKey id, CancellationToken cancellationToken = default) =>
        await root.DeleteAsync(id, cancellationToken);

    public async Task<Response<int>> DeleteRangeAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default) =>
        await root.DeleteRangeAsync(ids, cancellationToken);

    public async Task<PagedResponse<TEntity>> DeleteBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class =>
        await root.DeleteBySpec(spec, option, cancellationToken);
}