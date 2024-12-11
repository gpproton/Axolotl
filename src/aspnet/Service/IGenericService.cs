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
   Created At: Wed 11 Dec 2024 23:34:15
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 23:34:15
*/

using Ardalis.Specification;
using Axolotl.EFCore.Interfaces;
using Axolotl.Interfaces;
using Axolotl.Response;

namespace Axolotl.AspNet.Service;

public interface IGenericService<TEntity, in TKey>
    where TEntity : class, IAggregateRoot, IResponse
    where TKey : notnull {
    Task<PagedResponse<TEntity>> PageFilter(ISpecification<TEntity> specification, int? checkPage, int? checkSize,
        CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> GetAllAsync(IPageFilter? filter, Type? spec = null, CancellationToken cancellationToken = default);
    Task<Response<TEntity?>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> GetBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class, ISpecFilter;
    Task<Response<TEntity>> CreateAsync(TEntity value, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> CreateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default);
    Task<Response<TEntity>> UpdateAsync(TEntity value, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> values, CancellationToken cancellationToken = default);
    Task<Response<TEntity?>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    Task<Response<int>> DeleteRangeAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
    Task<PagedResponse<TEntity>> DeleteBySpec<TOption>(Type spec, TOption option, CancellationToken cancellationToken = default) where TOption : class;
}