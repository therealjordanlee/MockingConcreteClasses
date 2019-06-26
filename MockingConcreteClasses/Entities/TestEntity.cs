using Microsoft.WindowsAzure.Storage.Table;

namespace MockingConcreteClasses.Entities
{
    public class TestEntity : TableEntity
    {
        [IgnoreProperty]
        public string LastName => PartitionKey;

        [IgnoreProperty]
        public string FirstName => RowKey;
        public string Address { get; set; }
    }
}
