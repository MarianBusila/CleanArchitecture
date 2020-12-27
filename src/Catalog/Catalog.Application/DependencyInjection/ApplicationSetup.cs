using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.DependencyInjection
{
    public static class ApplicationSetup
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
