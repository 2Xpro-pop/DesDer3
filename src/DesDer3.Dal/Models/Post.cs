using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesDer3.Dal.Models;
public class Post : Model
{

    /// <summary>
    /// The name must be unique.
    /// </summary>
    public string Name
    {
        get; set;
    } = null!;

    /// <summary>
    /// Content is a dictionary where the key represents the localization 
    /// and the value represents the title
    /// </summary>
    public Dictionary<string, string> Title
    {
        get; set;
    } = new();

    /// <summary>
    /// Content is a dictionary where the key represents the localization 
    /// and the value represents the description
    /// </summary>
    public Dictionary<string, string> Description
    {
        get; set;
    } = new();

    /// <summary>
    /// Content is a dictionary where the key represents the localization 
    /// and the value represents JSON content.
    /// </summary>
    public Dictionary<string, string> Content
    {
        get; set;
    } = new();

    public Guid OwnerId
    {
        get; set;
    }

    public virtual User Owner
    {
        get; set;
    } = null!;

    public Guid RouteId
    {
        get; set;
    }
    public virtual Route Route
    {
        get; set;
    }
}
