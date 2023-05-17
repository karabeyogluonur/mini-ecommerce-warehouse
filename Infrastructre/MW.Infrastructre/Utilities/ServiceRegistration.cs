using Microsoft.Extensions.DependencyInjection;
using MW.Application.Interfaces.Repositories.CustomRepository;
using MW.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MW.Application.Interfaces.Services.Media;
using MW.Infrastructre.Services.Media;

namespace MW.Infrastructre.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructreServices(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
        }
    }
}
