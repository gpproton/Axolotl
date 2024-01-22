// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using DotNetEd.CoreAdmin;
using Axolotl.AspNet;
using Axolotl.AspNetSample.Data;
using Axolotl.EFCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { });

// Data services
builder.Services.RegisterDataContext();
builder.Services.RegisterUnitOfWork<ServiceContext>(pooled: false);
builder.Services.RegisterGenericRepositories(typeof(GenericRepository<,>));
// Axolotl AspNet services
builder.Services.RegisterGenericServices();
builder.Services.RegisterFeatures(typeof(Program).Assembly);

builder.Services.AddHostedService<MigrationService>();
builder.Services.AddCoreAdmin(new CoreAdminOptions {
    Title = "Sample Admin",
    ShowPageSizes = true,
    PageSizes = new Dictionary<int, string> {
        { 25, "25"},
        { 100, "100"},
        { 0, "All"}
    },
    IgnoreEntityTypes = new List<Type>()
});

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapDefaultControllerRoute();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

// Axolotl app DI
app.RegisterFeatureEndpoints();

app.MapGet("/", () => "x").WithName("Default").WithTags("Root");
app.UseCoreAdminCustomUrl("admin");
app.UseCoreAdminCustomAuth(_ => Task.FromResult(true));


app.Run();