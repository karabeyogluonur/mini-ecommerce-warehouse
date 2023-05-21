using Microsoft.Extensions.DependencyInjection;
using MW.Application.Interfaces.Repositories;
using MW.Application.Interfaces.Repositories.CustomRepositories;
using MW.Application.Interfaces.Repositories.CustomRepository;
using MW.Persistence.Contexts;
using MW.Persistence.Repositories;
using MW.Persistence.Repositories.CustomRepositories;

namespace MW.Persistence.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<MWDbContext>();
            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<IProductRepository,ProductRepository>();
            services.AddTransient<IStockHistoryRepository,StockHistoryRepository>();
            services.AddTransient<IUnitOfWork,UnitOfWork>();
        }
    }
}
