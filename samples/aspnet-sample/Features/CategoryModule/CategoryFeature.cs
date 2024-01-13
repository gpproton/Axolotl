// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Axolotl.AspNet.Feature;
using Axolotl.Enums;

namespace Axolotl.AspNetSample.Features.CategoryModule;

public class CategoryFeature : GenericFeature<CategoryFeature, Guid> {
    public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        var state = new FeatureState([
            new RouteState(RouteType.GetAll, typeof(CategorySpec)),
            new RouteState(RouteType.GetById),
            new RouteState(RouteType.Create),
            new RouteState(RouteType.CreateRange),
            new RouteState(RouteType.Update),
            new RouteState(RouteType.UpdateRange),
            new RouteState(RouteType.Delete),
            new RouteState(RouteType.DeleteRange)
        ]);

        var group = SetupGroup<CategoryFeature, Category>(endpoints, state);
        var options = new RouteState(RouteType.Any, typeof(CategorySpec));
        
        AddGetBySpec<Category, Category, CategorySpecFilter>(group, options);
        AddDeleteBySpec<Category, Category, CategorySpecObject>(group, options);

        return GetEndpoints();
    }
}