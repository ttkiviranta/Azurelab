using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using NServiceBus;
using Server.CommandHandlers;
using Server.DAL;
using Server.EventHandlers;
using Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class StatelessEndpointCommunicationListener :
        ICommunicationListener
    {
        IEndpointInstance EndpointInstance;
        IEndpointInstance EndpointInstancePriority;
        IServiceCollection Services;
        IContainer Container;

        public async Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApiContext>();

            dbContextOptionsBuilder.UseSqlServer(Helpers.GetSqlConnection());

            Services = new ServiceCollection();
            Services.AddDbContext<ApiContext>(options =>
                    options.UseSqlServer(Helpers.GetSqlConnection()));

            var builder = new ContainerBuilder();
          
            builder.Populate(Services);
            builder.RegisterType<CreateProductHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateProductHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<DeleteProductHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateProductOnlineHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateProductLockedStatusHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);            
            builder.RegisterType<ClearDatabaseHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder); 
            Container = builder.Build();
        //    Services.AddMvc(); //Tarviiko???
            var endpointName = Helpers.ServerEndpoint;
            var endpointConfiguration = Helpers.CreateEndpoint(endpointName, Server.GetPathToData());
            endpointConfiguration.UseContainer<AutofacBuilder>(
                customizations: customizations =>
                {
                    customizations.ExistingLifetimeScope(Container);
                });

            EndpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            var endpointConfigurationPriority = Helpers.CreatePriorityEndpointSubscriber(Server.GetPathToData());
            endpointConfigurationPriority.UseContainer<AutofacBuilder>(
                customizations: customizations =>
                {
                    customizations.ExistingLifetimeScope(Container);
                });

            EndpointInstancePriority = await Endpoint.Start(endpointConfigurationPriority)
                .ConfigureAwait(false);

            return endpointName;
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            return EndpointInstance.Stop();
        }

        public void Abort()
        {
            CloseAsync(CancellationToken.None);
        }
    }
}
