using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal;
using DesDer3.Dal.Models;
using DesDer3.Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesDer3.Bll.Internal;
internal class RouteService : IRouteService
{
    private static readonly Dictionary<Route, string?> _memoPath = new();
    private static readonly Dictionary<string, Route> _memoRoute = new(StringComparer.InvariantCultureIgnoreCase);

    private readonly IRouteRepository _routes;

    public RouteService(IRouteRepository routes)
    {
        _routes = routes;
    }
    public async Task<string?> GetPathByRouteAsync(Route route)
    {
        return GetPathTo(await GetRequiredRoot(), route);
    }
    public async Task<Post?> GetPostByPathAsync(string path)
    {
        var route = await GetRouteByPathAsync(path);

        return route?.Post;
    }
    public async Task<Route?> GetRootAsync()
    {
        var root = await _routes.Where(r => r.ParentRouteId == null).FirstOrDefaultAsync();

        return root;
    }
    public async Task<Route?> GetRouteByPathAsync(string path)
    {
        path = RouteHelper.PreparePath(path);

        if (_memoRoute.TryGetValue(path, out var value))
        {
            return value;
        }

        var segments = path.Split('/');

        return GetRouteTo(await GetRequiredRoot(), segments, 0);
    }
    public async Task RemoveRouteAsync(Route route)
    {
        await RemoveRouteAndDescendantsAsync(route);
        await _routes.DeleteAsync(route);
        await _routes.SaveAsync();

        // Required for path re-finding
        _memoPath.Clear();
        _memoRoute.Clear();
    }
    public async Task SaveRouteAsync(Route route)
    {

        if (HasRecursive(route))
        {
            throw new RouteRecursiveException(route, "Recursive route detected.");
        }

        var entity = await _routes.FindByIdAsync(route.Id);

        if (entity == null)
        {
            await _routes.AddAsync(route);
        }
        else
        {
            await _routes.UpdateAsync(route);
        }

        // Required for path re-finding
        _memoPath.Clear();
        _memoRoute.Clear();

        await _routes.SaveAsync();

    }

    private async Task<Route> GetRequiredRoot()
    {
        var root = await GetRootAsync();

        if (root == null)
        {
            throw new RouteException(null, "Does not have a root route. Please create root route(route withot parrent)");
        }

        return root;
    }
    private string? GetPathTo(Route root, Route target)
    {
        if (_memoPath.TryGetValue(root, out var value))
        {
            return value;
        }

        if (root == target)
        {
            return root.HasIdParameter ? $"{root.Segment}/{{id}}" : root.Segment;
        }

        foreach (var childRoute in root.ChildRoutes)
        {

            var path = GetPathTo(childRoute, target);

            if (path != null)
            {
                var fullPath = root.Segment + "/" + path;
                _memoPath[root] = fullPath;
                return fullPath;
            }
        }

        _memoPath[root] = null;
        return null;
    }
    private Route? GetRouteTo(Route root, string[] segments, int currentSegment)
    {
        if (segments.Length - currentSegment == 1 &&
            RouteHelper.IsEqualSegments(root, segments[currentSegment]))
        {
            var path = string.Join("/", segments);
            _memoRoute.Add(path, root);

            return root;
        }
        if (segments.Length - currentSegment == 2 && root.HasIdParameter)
        {
            var segment = $"{segments[currentSegment]}/{segments[currentSegment + 1]}";

            if (RouteHelper.IsEqualSegments(root, segment))
            {
                return root;
            }
        }

        foreach (var childRoute in root.ChildRoutes)
        {
            var foundRoute = GetRouteTo(childRoute, segments, currentSegment + 1);
            if (foundRoute != null)
            {
                return foundRoute;
            }
        }

        return null;
    }
    private async Task RemoveRouteAndDescendantsAsync(Route route)
    {

        foreach (var childRoute in route.ChildRoutes)
        {
            await RemoveRouteAndDescendantsAsync(childRoute);
            await _routes.DeleteAsync(childRoute);
        }
    }
    private static bool HasRecursive(Route route)
    {
        return HasUpRecursive(route) || HasDownRecursive(route, new());
    }

    private static bool HasUpRecursive(Route route)
    {
        var visited = new HashSet<Route>();
        var currentRoute = route;
        while (currentRoute.ParentRoute != null)
        {

            if (visited.Contains(currentRoute))
            {
                return true;
            }

            visited.Add(currentRoute);

            currentRoute = currentRoute.ParentRoute;

            if (currentRoute == null)
            {
                return false;
            }
        }

        return false;
    }
    private static bool HasDownRecursive(Route currentNode, HashSet<Route> visited)
    {
        if (visited.Contains(currentNode))
        {
            return true;
        }

        visited.Add(currentNode);

        foreach (var childRoute in currentNode.ChildRoutes)
        {
            if (HasDownRecursive(childRoute, visited))
            {
                return true;
            }
        }

        visited.Remove(currentNode);

        return false;
    }
}
