using System.Reflection.Emit;
using DesDer3.Dal;
using DesDer3.Dal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesDer3.Dal.EfIdentity;
public class DesDer3DbContext: IdentityDbContext<User>
{
    public DbSet<Post> Posts
    {
        get; set;
    } = null!;
    public DbSet<Route> Routes
    {
        get; set;
    } = null!;


    public DesDer3DbContext(DbContextOptions<DesDer3DbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>()
            .HasOne(p => p.Route)
            .WithOne(r => r.Post)
            .HasForeignKey<Route>(r => r.PostId);

    }
}
