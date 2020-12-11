﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Catalog.Api.DependencyInjection
{
    internal static class SwaggerSetup
    {
        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(setup => 
            { 
                setup.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Catalog API",
                        Version = "v1",
                        Description = "A music catalog API",
                        TermsOfService = new Uri("http://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Developer",
                            Email = "developer@example.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Apache 2.0",
                            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                        }
                    });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
                setup.EnableAnnotations();
            });
        }

        internal static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup => 
            { 
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
                setup.RoutePrefix = string.Empty;
                setup.DefaultModelExpandDepth(2);
                setup.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
                setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                setup.EnableDeepLinking();
            });
            return app;
        }
    }
}
