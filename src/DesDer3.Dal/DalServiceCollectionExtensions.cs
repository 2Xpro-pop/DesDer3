using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DesDer3.Dal;
public static class DalServiceCollectionExtensions
{
    public static void AddDal(this IServiceCollection services)
    {
        services.AddScoped(serviceProvider =>
        {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            return unitOfWork.UserRepository;
        });
        services.AddScoped(serviceProvider =>
        {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            return unitOfWork.RouteRepository;
        });
        services.AddScoped(serviceProvider =>
        {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            return unitOfWork.PostRepository;
        });
    }
}
