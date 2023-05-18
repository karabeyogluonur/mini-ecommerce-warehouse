using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MW.Application.Interfaces.Services.Media;
using MW.Application.Interfaces.Services.Messages;
using MW.Application.Validations.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //FluentValidation
            services.AddFluentValidation(x =>
            {
                x.DisableDataAnnotationsValidation = true;
                x.RegisterValidatorsFromAssemblyContaining<UserAddModelValidator>();
            });
        }
    }
}
