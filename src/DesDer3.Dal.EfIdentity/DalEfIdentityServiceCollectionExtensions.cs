using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.EfIdentity;
using DesDer3.Dal;
using DesDer3.Dal.EfIdentity;
using DesDer3.Dal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;
public static class DalEfIdentityServiceCollectionExtensions
{
    public static IdentityBuilder AddDalEfIdentity(this IServiceCollection services, Action<DbContextOptionsBuilder>? dbContextOptions = null, Action<IdentityOptions>? identityOptions = null)
    {

        services.AddDbContext<DesDer3DbContext>(dbContextOptions);
        services.AddScoped<IUnitOfWork>(serviceProvider =>
        {
            return ActivatorUtilities.CreateInstance<EdIdentityUnitOfWork>(serviceProvider);
        });
        services.AddDal();

        return services.AddIdentityCore<User>(options =>
        {
            options.Stores.MaxLengthForKeys = 128;
            identityOptions?.Invoke(options);
        }).AddEntityFrameworkStores<DesDer3DbContext>();
    }
}
