using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.DependencyInjection
{
    public static class ApplicationSetup
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
