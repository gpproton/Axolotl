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
   Created At: Wed 11 Dec 2024 19:50:56
   Modified By: Godwin peter. O (me@godwin.dev)
   Modified At: Wed 11 Dec 2024 19:50:56
*/

using Axolotl.AspNet;
using Axolotl.AspNetSample.Data;
using Axolotl.EFCore;
using DotNetEd.CoreAdmin;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { });
builder.Services.RegisterDataContext();
builder.Services.RegisterUnitOfWork<ServiceContext>(pooled: false);
builder.Services.RegisterGenericRepositories(typeof(GenericRepository<,>));
builder.Services.RegisterGenericServices();
builder.Services.RegisterFeatures(typeof(Program).Assembly);
builder.Services.AddHostedService<MigrationService>();
builder.Services.AddCoreAdmin(
    new CoreAdminOptions {
        Title = "Sample Admin",
        ShowPageSizes = true,
        PageSizes = new Dictionary<int, string>
        {
            { 25, "25" },
            { 100, "100" },
            { 0, "All" },
        },
        IgnoreEntityTypes = new List<Type>(),
    }
);

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
app.RegisterFeatureEndpoints();
app.MapGet("/", () => "x").WithName("Default").WithTags("Root");
app.UseCoreAdminCustomUrl("admin");
app.UseCoreAdminCustomAuth(_ => Task.FromResult(true));

app.Run();