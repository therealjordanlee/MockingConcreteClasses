using Microsoft.WindowsAzure.Storage.Table;
using MockingConcreteClasses.Entities;
using MockingConcreteClasses.Models;
using System.Threading.Tasks;

namespace MockingConcreteClasses.Services
{
    public class RandomService : IRandomService
    {
        private CloudTable _table;
        public RandomService(CloudTable table)
        {
            _table = table;
        }

        public async Task AddTestEntity(TestModel model)
        {
            var entity = new TestEntity
            {
                PartitionKey = model.LastName,
                RowKey = model.FirstName,
                Address = model.Address
            };

            TableOperation insertOperation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
