// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.AspNetSample.Features.CategoryModule;
using Axolotl.AspNetSample.Features.PostModule;
using Axolotl.AspNetSample.Features.TagModule;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.AspNetSample.Data;

public class MigrationService : BackgroundService {
    private readonly ILogger<MigrationService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public MigrationService(ILogger<MigrationService> logger, IServiceScopeFactory factory) {
        _logger = logger;
        _scopeFactory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Starting migration...");
        var sp = _scopeFactory.CreateScope().ServiceProvider;
        
        await using var context = sp.GetService<ServiceContext>();
        if (context is null) return;
        
        if (context.Database.IsRelational())
            await context.Database.MigrateAsync(cancellationToken);

        using (var any = context.Posts.AnyAsync(cancellationToken)) {
            if (!(await any)) {
                _logger.LogInformation("Starting seeding...");
                var tags = new List<Tag> {
                    new() { Id = Guid.NewGuid(), Name = "tag-1" },
                    new() { Id = Guid.NewGuid(), Name = "tag-2" },
                    new() { Id = Guid.NewGuid(), Name = "tag-3" }
                };
        
                var categories = new List<Category> {
                    new() { Name = "category-1" },
                    new() { Name = "category-2" },
                    new() { Name = "category-3" }
                };
        
                var posts = new List<Post> {
                    new() { Title = "post-1", Content = "xx-xx-xx", Tags = new List<Tag> { tags[0], tags[1]} , Category = categories[0] },
                    new() { Title = "post-2", Content = "xx-xx-xx", Tags = new List<Tag> { tags[2], tags[0]}, Category = categories[1] },
                    new() { Title = "post-3", Content = "xx-xx-xx", Tags = new List<Tag> { tags[1], tags[2]}, Category = categories[2] }
                };
                
                await context.Posts.AddRangeAsync(posts, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Completed seeding...");
            }
        }

        _logger.LogInformation("Completed migration...");
    }
}