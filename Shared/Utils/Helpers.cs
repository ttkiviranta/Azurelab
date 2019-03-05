using NServiceBus;
using Shared.Messages.Events;
using System;
using System.IO;

namespace Shared.Utils
{
    public class Helpers
    {
        public const string ApiEndpoint = "api-endpoint";
        public const string ApiPriorityEndpoint = "api-publisher";
        public const string ServerEndpoint = "server-endpoint";
        private const string ServerPriorityEndpoint = "server-subscriber";

        private static EndpointConfiguration CreateEndpointBase(string endpointName, string pathToData)
        {
            Console.WriteLine("Create and start endpoint " + endpointName);
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.SetDiagnosticsPath(Directory.GetParent(pathToData).ToString());
            endpointConfiguration.LicensePath(pathToData + "\\License.xml");
            endpointConfiguration.Conventions().DefiningMessagesAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("Shared.Messages") &&
                    (t.Namespace.EndsWith("Commands")))
                .DefiningEventsAs(t =>
                    t.Namespace != null && t.Namespace.StartsWith("Shared.Messages") &&
                    t.Namespace.EndsWith("Events"));

            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");
            return endpointConfiguration;
        }

        public static EndpointConfiguration CreateEndpoint(string endpointName, string pathToData)
        {
            var endpointConfiguration = CreateEndpointBase(endpointName, pathToData);

            var persistence = endpointConfiguration.UsePersistence<AzureStoragePersistence>()
                             .ConnectionString(GetStorageConnection());

            var transport = endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                                        .ConnectionString(GetStorageConnection());
            return endpointConfiguration;
        }

        public static EndpointConfiguration CreatePriorityEndpoint(string endpointName, string pathToData)
        {
            var endpointConfiguration = CreateEndpointBase(endpointName, pathToData);

            endpointConfiguration.UsePersistence<AzureStoragePersistence, StorageType.Subscriptions>()
               .ConnectionString(GetStorageConnection());
            endpointConfiguration.UsePersistence<AzureStoragePersistence, StorageType.Timeouts>()
           .ConnectionString(GetStorageConnection())
           .CreateSchema(true)
           .TimeoutManagerDataTableName("TimeoutManagerPriority")
           .TimeoutDataTableName("TimeoutDataPriority")
           .CatchUpInterval(3600)
           .PartitionKeyScope("2018052400");
            return endpointConfiguration;
        }

        public static EndpointConfiguration CreatePriorityEndpointSubscriber(string pathToData)
        {
            var endpointConfiguration = CreatePriorityEndpoint(ServerPriorityEndpoint, pathToData);

            var transportPriority = endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                                      .ConnectionString(Helpers.GetStorageConnection());
            var routingPriority = transportPriority.Routing();
          //  routingPriority.RegisterPublisher(typeof(UpdateCarLockedStatus), ApiPriorityEndpoint);
          //  routingPriority.RegisterPublisher(typeof(ClearDatabase), ApiPriorityEndpoint);
            return endpointConfiguration;
        }

        public static EndpointConfiguration CreatePriorityEndpointPublisher(string pathToData)
        {
            var endpointConfiguration = CreatePriorityEndpoint(ApiPriorityEndpoint, pathToData);

            var transportPriority = endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                                      .ConnectionString(GetStorageConnection());
            return endpointConfiguration;
        }

        public static string GetSqlConnection()
        {
            //  return "Server=tcp:sireusdbserver.database.windows.net,1433;Initial Catalog=carnbus;Persist Security Info=False;User ID=sireus;Password=GS1@azure;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //    return @"Server=tcp:DESKTOP-HMTP7MB\MSSQLSERVER01.database.windows.net,1433;Initial Catalog=Homelab;Persist Security Info=False;User ID=OMISTAJA;Password=Timoki10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //   return @"Server=tcp:DESKTOP-HMTP7MB\MSSQLSERVER01;Initial Catalog=Homelab;Persist Security Info=False;User ID=OMISTAJA;Password=Timoki10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return "Server=DESKTOP-HMTP7MB\\MSSQLSERVER01;Initial Catalog=Homelab;Persist Security Info=False;User ID=OMISTAJA;Password=Timoki10;Connection Timeout=30;";
            //  return @"Server=localhost\MSSQLSERVER01;Database=Homelab;Trusted_Connection=True;ConnectRetryCount=0";
        }

        public static string GetAspNetDbConnection()
        {
            //return "Server=tcp:sireusdbserver.database.windows.net,1433;Initial Catalog=aspnetdb;Persist Security Info=False;User ID=sireus;Password=GS1@azure;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            // return @"Server=tcp:DESKTOP-HMTP7MB\MSSQLSERVER01.database.windows.net;Initial Catalog=Homelab;Persist Security Info=False;User ID=OMISTAJA;Password=Timoki10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return @"Server=tcp:DESKTOP-HMTP7MB\MSSQLSERVER01;Initial Catalog=Homelab;Persist Security Info=False;User ID=OMISTAJA;Password=Timoki10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public static string GetStorageConnection()
        {
            // return "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;EndpointSuffix=core.windows.net";
             return "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1";
        }
    }
}
