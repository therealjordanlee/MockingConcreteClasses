using FluentAssertions;
using Microsoft.WindowsAzure.Storage.Table;
using MockingConcreteClasses.Entities;
using MockingConcreteClasses.Models;
using MockingConcreteClasses.Services;
using NSubstitute;
using System;
using Xunit;

namespace MockingConcreteClassesTests
{
    public class UnitTests
    {
        [Fact]
        public void RandomService_Converts_TestModel_To_TestEntity()
        {
            // Arrange
            var mockTestModel = new TestModel
            {
                LastName = "John",
                FirstName = "Smith",
                Address = "123 Jump Street"
            };

            var mockCloudTable = Substitute.For<CloudTable>(new Uri("https://fake.url"));

            var expectedTestEntity = new TestEntity
            {
                PartitionKey = mockTestModel.LastName,
                RowKey = mockTestModel.FirstName,
                Address = mockTestModel.Address
            };

            var expectedTableOperation = TableOperation.Insert(expectedTestEntity);
            TableOperation actualOperation = null;

            mockCloudTable.ExecuteAsync(Arg.Do<TableOperation>(tableOperation =>
            {
                actualOperation = tableOperation;
            }));

            var randomService = new RandomService(mockCloudTable);

            // Act
            randomService.AddTestEntity(mockTestModel);

            // Assert
            actualOperation.Should().BeEquivalentTo(expectedTableOperation);
        }
    }
}
