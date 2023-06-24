using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Bll;
using DesDer3.Bll.Internal;

namespace Microsoft.Extensions.DependencyInjection;
public static class BllServiceCollectionExtensions
{
    public static void AddBll(this IServiceCollection services)
    {
        services.AddTransient<IRouteService, RouteService>();
    }
}
