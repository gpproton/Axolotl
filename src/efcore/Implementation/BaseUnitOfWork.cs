// Copyright 2022 - 2024 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.EFCore.Implementation;

public abstract class BaseUnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext  {
    public abstract TContext Context { get; }
    private bool _disposed = false;
    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await Context.SaveChangesAsync(cancellationToken);

    private void Dispose(bool disposing) {
        if (_disposed) return;
        if (disposing) Context.Dispose();
        _disposed = true;
    }

    public virtual void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}