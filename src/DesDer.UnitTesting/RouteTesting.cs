using DesDer3.Bll;
using DesDer3.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.ContentModel;

namespace DesDer.UnitTesting;

[TestClass]
public class RouteTesting
{
    private TestServer _server = null!;
    private IServiceScope _scope = null!;
    private IServiceProvider _scopedServices = null!;
    private IRouteService _routeService = null!;
    private Route _root = null!;

    [TestInitialize]
    public async Task Initialize()
    {
        _server = new();
        _scope = _server.Services.CreateScope();
        _scopedServices = _scope.ServiceProvider;
        _routeService = _scopedServices.GetRequiredService<IRouteService>();

        // Set up routes
        _root = new Route()
        {
            Segment = "main",
            ChildRoutes = new()
            {
                new()
                {
                    Segment = "about",
                    ChildRoutes = new()
                    {
                        new()
                        {
                            Segment = "UCO"
                        }
                    }
                },
                new()
                {
                    Segment = "profile",
                    HasIdParameter = true
                }
            }
        };

        await _routeService.SaveRouteAsync(_root);
    }

    [TestMethod]
    public void TestRouteHelperPreparePath()
    {
        var tests = new Dictionary<string, string>()
        {
            { "main/profile123/{id}", "main/profile123/23" },
            { "main/asd123/33d/{id}", "main/asd123/33d/23" },
            { "main/asd123/{id}/{id}", "main/asd123/33/9" },
        };

        foreach (var test in tests)
        {
            Assert.AreEqual(RouteHelper.PreparePath(test.Value), test.Key);
        }
    }

    [TestMethod]
    public void TestRouteSegmentsEquals()
    {
        var tests = new[]
        {
            new { Route = new Route(){Segment = "hi"}, Segment = "hi", Result = true },
            new { Route = new Route(){Segment = "hi", HasIdParameter = true}, Segment = "hi/{id}", Result = true },
            new { Route = new Route(){Segment = "hi", HasIdParameter = true}, Segment = "hi", Result = false },
        };

        foreach (var test in tests)
        {
            Assert.IsTrue(RouteHelper.IsEqualSegments(test.Route, test.Segment) == test.Result);
        }
    }

    [TestMethod]
    public async Task TestRootEquality()
    {
        var root = await _routeService.GetRootAsync();

        Assert.IsNotNull(root);
        Assert.AreEqual(_root, root);
    }

    [TestMethod]
    public async Task TestRecursiveException()
    {
        var recursive = new Route()
        {
            Segment = "recursive",
            ParentRoute = _root,
            ParentRouteId = _root.Id
        };

        _root.ParentRoute = recursive;
        _root.ParentRouteId = recursive.Id;

        await Assert.ThrowsExceptionAsync<RouteRecursiveException>(async () =>
        {
            await _routeService.SaveRouteAsync(recursive);
        });

        _root.ParentRoute = null;
        _root.ParentRouteId = null;
        _root.ChildRoutes.Remove(recursive);
    }

    [TestMethod]
    public async Task TestRouteAndPath()
    {
        var route = await _routeService.GetRouteByPathAsync("main/profile/1234");

        Assert.AreEqual(_root.ChildRoutes[1], route);

        var path = await _routeService.GetPathByRouteAsync(route!);

        Assert.AreEqual("main/profile/{id}", path);
    }

    [TestCleanup]
    public async Task Exit()
    {
        await _routeService.RemoveRouteAsync(_root);

        _server.Dispose();
        _scope.Dispose();
    }
}