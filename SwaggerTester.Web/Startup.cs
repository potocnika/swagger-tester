using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using SwaggerTester.Web.Swagger;
using System;

namespace SwaggerTester.Web
{
    public class Startup
    {
        public Startup()
        {
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters();
            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger(typeof(Startup).Assembly, new SwaggerSettings
            {
                GenerateAbstractProperties = false,
                FlattenInheritanceHierarchy = true,
                DefaultEnumHandling = NJsonSchema.EnumHandling.String,
                DocumentProcessors = { new DocumentProcessor() },
                OperationProcessors = { new OperationProcessor() }
            });
            app.UseSwaggerUi3(typeof(Startup).Assembly, new SwaggerUi3Settings
            {
                SwaggerUiRoute = "/docs",
                GenerateAbstractProperties = false,
                FlattenInheritanceHierarchy = true,
                DefaultEnumHandling = NJsonSchema.EnumHandling.String
            });
            app.UseMvcWithDefaultRoute();
        }
    }
}
