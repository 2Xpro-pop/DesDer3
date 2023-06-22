using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesDer3.Dal.Models;
public class Route : Model
{
    /// <summary>
    /// Content is a dictionary where the key represents the localization 
    /// and the value represents name. This is required for navigation 
    /// purposes
    /// </summary>
    public Dictionary<string, string> Name
    {
        get; set;
    } = new();

    /// <summary>
    /// The segment name should not consist of digits only.
    /// </summary>
    public string Segment
    {
        get; set;
    } = null!;

    /// <summary>
    /// Indicates whether the route has an Id parameter.
    /// </summary>
    public bool HasIdParameter
    {
        get; set;
    } = false;

    /// <summary>
    /// The context to which the route's Id is related.
    /// </summary>
    public string? IdContext
    {
        get; set;
    }

    public Guid? ParentRouteId
    {
        get; set;
    }
    public Route? ParentRoute
    {
        get; set;
    }

    public virtual List<Route> ChildRoutes
    {
        get; set;
    } = new();

    public Guid? PostId
    {
        get; set;
    }
    public Post? Post
    {
        get; set;
    }
}
