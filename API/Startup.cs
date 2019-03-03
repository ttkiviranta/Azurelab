using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Shared.Utils;
using Swashbuckle.AspNetCore.Swagger;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        Autofac.IContainer Container { get; set; }
        IConfiguration Configuration { get; set; }
        IEndpointInstance EndpointInstance;
        IEndpointInstance EndpointInstancePriority;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var endpointConfiguration = Helpers.CreateEndpoint(Helpers.ApiEndpoint, API.GetPathToData());
            EndpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            var endpointConfigurationPriority = Helpers.CreatePriorityEndpointPublisher(API.GetPathToData());
            EndpointInstancePriority = Endpoint.Start(endpointConfigurationPriority).GetAwaiter().GetResult();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            Container = containerBuilder.Build();
            services.AddSingleton(EndpointInstance);
            services.AddSingleton(EndpointInstancePriority);
            services.AddSingleton(Configuration);
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AzurelabTypeAPI", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzurelabTypeApi V1");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            app.UseCors("AllowAllOrigins");
        }
    }
}

