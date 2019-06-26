using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MockingConcreteClasses.Services;
using System;

[assembly: FunctionsStartup(typeof(MockingConcreteClasses.Startup))]

namespace MockingConcreteClasses
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Repositories
            var storageAccountKey = Environment.GetEnvironmentVariable("StorageAccountConnectionString");
            var storageAccount = CloudStorageAccount.Parse(storageAccountKey);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("TestTable");
            table.CreateIfNotExistsAsync().Wait();

            builder.Services.AddSingleton<CloudTable>(x =>
            {
                return table;
            });

            builder.Services.AddSingleton<IRandomService, RandomService>();
        }
    }
}
