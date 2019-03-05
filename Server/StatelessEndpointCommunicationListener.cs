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
     /*       builder.RegisterType<CreateCarHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<CreateCarLockedStatusHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<CreateCarOnlineStatusHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<CreateCarSpeedHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);*/
     //       builder.RegisterType<CreateCompanyHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
        /*    builder.RegisterType<CreateCompanyNameHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<CreateCompanyAddressHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<DeleteCarHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);*/
    //        builder.RegisterType<DeleteCompanyHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
          /*  builder.RegisterType<UpdateCarLockedStatusHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateCarOnlineStatusHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateCarSpeedHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateCompanyAddressHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
            builder.RegisterType<UpdateCompanyNameHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);*/
            builder.RegisterType<ClearDatabaseHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder); 
            Container = builder.Build();

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
