using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.DependencyInjection
{
    public static class ApplicationSetup
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationSetup).Assembly))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
