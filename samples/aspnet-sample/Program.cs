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
using Proton.Common.AspNet;
using Proton.Common.AspNetSample.Data;
using Proton.Common.EFCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { });

// Proton.Common services
builder.Services.RegisterFeatures(typeof(Program).Assembly);
builder.Services.RegisterGenericServices();

// Sample services
builder.Services.RegisterDataContext();
builder.Services.RegisterGenericRepositories(typeof(GenericRepository<>));
builder.Services.AddHostedService<MigrationService>();
builder.Services.AddCoreAdmin(new CoreAdminOptions {
    Title = "Sample Admin",
    ShowPageSizes = true,
    PageSizes = new Dictionary<int, string>() {
        { 25, "25"},
        { 100, "100"},
        { 0, "All"}
    },
    IgnoreEntityTypes = new List<Type> { }
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

// Proton.Common app DI
app.RegisterFeatureEndpoints();

app.MapGet("/", () => "x").WithName("Default").WithTags("Root");
app.UseCoreAdminCustomUrl("admin");
app.UseCoreAdminCustomAuth((serviceProvider) => Task.FromResult(true));


app.Run();