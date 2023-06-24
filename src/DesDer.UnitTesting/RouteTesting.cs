using DesDer3.Bll;
using DesDer3.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesDer.UnitTesting;

[TestClass]
public class RouteTesting
{
    private TestServer _server = null!;
    private IServiceScope _scope = null!;
    private IServiceProvider _scopedServices = null!;

    [TestInitialize]
    public void Initialize()
    {
        _server = new();
        _scope = _server.Services.CreateScope();
        _scopedServices = _scope.ServiceProvider;

        // Set up routes
        var root = new Route()
        {
            Segment = "main",
            ChildRoutes = new()
            {
                new()
                {
                    Segment = "about"
                }
            }
        };
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

    [TestCleanup]
    public void Exit()
    {
        _server.Dispose();
        _scope.Dispose();
    }
}