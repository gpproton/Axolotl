// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Proton.Common.EFCore.Interfaces;
using Proton.Common.Response;

namespace Proton.Common.AspNet.Feature;

public partial interface IGenericEndpoint {
    IGenericEndpoint AddGetAll<TEntity>() where TEntity : IAggregateRoot;
    IGenericEndpoint AddGetById<TEntity, TId>() where TEntity : IAggregateRoot where TId : notnull;
    IGenericEndpoint AddCreate<TEntity>() where TEntity : IAggregateRoot;
    IGenericEndpoint AddCreateRange<TEntity>() where TEntity : IAggregateRoot;
    IGenericEndpoint AddUpdate<TEntity>() where TEntity : IAggregateRoot;
    IGenericEndpoint AddUpdateRange<TEntity>() where TEntity : IAggregateRoot;
    IGenericEndpoint AddDelete<TEntity, TId>() where TEntity : IAggregateRoot where TId : notnull;
    IGenericEndpoint AddDeleteRange<TEntity>() where TEntity : IAggregateRoot;
}