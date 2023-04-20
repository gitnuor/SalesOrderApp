using CallBookSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SalesOrder.Application.Interfaces;
using SalesOrder.Infrastructure.Repositories;

namespace SalesOrder.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
 
            services.AddTransient<ISalesOrderRepository ,SalesOrderRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
