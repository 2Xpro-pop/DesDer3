using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesDer3.Dal.Models;
public enum UserRole
{
    /// <summary>
    /// Represents the role of an administrator.
    /// Administrators typically have full access and control over the system.
    /// </summary>
    Admin,

    /// <summary>
    /// Represents the role of a developer.
    /// Developers are involved in software development tasks and have elevated privileges 
    /// for development-related activities.
    /// </summary>
    Developer,

    /// <summary>
    /// Represents the role of an editor.
    /// Editors have permissions to modify and manage content within the system.
    /// </summary>
    Editor,

    /// <summary>
    /// Represents the role of a viewer.
    /// Viewers have permissions for read-only access to view system content without the ability to modify it.
    /// </summary>
    Viewer
}
