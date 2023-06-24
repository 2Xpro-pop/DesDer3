using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal;
using DesDer3.Dal.Models;

namespace DesDer3.Bll;
public interface IRouteService
{
    Task<Post?> GetPostByPathAsync(string path);
    Task<string?> GetPathByRouteAsync(Route route);
    Task<Route?> GetRouteByPathAsync(string path);
    Task<Route?> GetRootAsync();
    Task SaveRouteAsync(Route route);
    Task RemoveRouteAsync(Route route);

}
