using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DesDer3.Dal.Models;
public class User : IdentityUser
{
    public string FullName { get; set; } = null!;
    public UserRole Role { get; set; } =  UserRole.Editor;

    /// <summary>
    /// The posts created by the user.
    /// </summary>
    public List<Post> Posts { get; set; } = new();
}