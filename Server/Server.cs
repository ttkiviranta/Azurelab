using System.Collections.Generic;
using System.Fabric;
using System.IO;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Server
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class Server : StatelessService
    {
        public static string GetPathToData()
        {
            return pathToData;
        }

        private static string pathToData;

        public Server(StatelessServiceContext context)
            : base(context)
        {
            pathToData = context.CodePackageActivationContext.GetDataPackageObject("Data").ToString();
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>  new StatelessEndpointCommunicationListener())
            };
        }
    }
}
