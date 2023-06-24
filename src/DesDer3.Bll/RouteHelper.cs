using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesDer3.Dal.Models;

namespace DesDer3.Bll;
public partial class RouteHelper
{
    public static string PreparePath(string path)
    {
        var parts = path.Split('/');
        for (var i = 0; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out _))
            {
                parts[i] = "{id}";
            }
        }
        return string.Join("/", parts);
    }

    public static bool IsEqualSegments(Route route, string segment)
    {
        if (route.HasIdParameter)
        {
            return $"{route.Segment}/{{id}}".Equals(segment, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            return route.Segment.Equals(segment, StringComparison.OrdinalIgnoreCase);
        }
    }
}
