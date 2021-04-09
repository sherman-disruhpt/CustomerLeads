using System;
using System.Linq;
using Xunit;
using Customer;

namespace Customer.Tests
{
    public class CustomerCollectionTests
    {
        [Fact]
        public void Add_Customer()
        {
            CustomerModel model = new CustomerModel();
            CustomerCollection collection = new CustomerCollection();
            collection.Add(model);

            Assert.Equal(1, collection.Count);
        }

        [Fact]
        public void AddRange_Customer()
        {
            CustomerModel model = new CustomerModel();
            CustomerCollection collection = new CustomerCollection();
            CustomerModel[] list = { model, model };
            collection.AddRange(list);

            Assert.Equal(2, collection.Count);
        }

        [Theory]
        [InlineData("A", "B")]
        public void SortByProject_Customer(string value1, string value2)
        {
            CustomerModel model1 = new CustomerModel
            {
                Project = value2,
            };
            CustomerModel model2 = new CustomerModel
            {
                Project = value1,
            };

            CustomerCollection collection = new CustomerCollection();
            collection.Add(model1);
            collection.Add(model2);
            var customers = (collection.Sort(CustomerPredicate.SortByProject)).ToList();

            Assert.Equal(value1, customers[0].Project);
            Assert.Equal(value2, customers[1].Project);
        }

        [Theory]
        [InlineData("A", "B")]
        public void SortByLastName_Descending_Customer(string value1, string value2)
        {
            CustomerModel model1 = new CustomerModel
            {
                LastName = value1,
            };
            CustomerModel model2 = new CustomerModel
            {
                LastName = value2,
            };

            CustomerCollection collection = new CustomerCollection();
            collection.Add(model1);
            collection.Add(model2);
            var customers = (collection.Sort(CustomerPredicate.SortByLastName)).ToList();

            Assert.Equal(value2, customers[0].LastName);
            Assert.Equal(value1, customers[1].LastName);
        }

        [Theory]
        [InlineData("A", "B")]
        public void SortByPropertyType_Customer(string value1, string value2)
        {
            CustomerModel model1 = new CustomerModel
            {
                PropertyType = value2,
            };
            CustomerModel model2 = new CustomerModel
            {
                PropertyType = value1,
            };

            CustomerCollection collection = new CustomerCollection();
            collection.Add(model1);
            collection.Add(model2);
            var customers = (collection.Sort(CustomerPredicate.SortByPropertyType)).ToList();

            Assert.Equal(value1, customers[0].PropertyType);
            Assert.Equal(value2, customers[1].PropertyType);
        }

        [Theory]
        [InlineData("A", "B")]
        public void SortByPropertyTypeThenProject_Customer(string value1, string value2)
        {
            CustomerModel model1 = new CustomerModel
            {
                PropertyType = value2,
                Project = value2,
            };
            CustomerModel model2 = new CustomerModel
            {
                PropertyType = value2,
                Project = value1,
            };

            CustomerModel model3 = new CustomerModel
            {
                PropertyType = value1,
                Project = value1,
            };

            CustomerCollection collection = new CustomerCollection();
            collection.Add(model1);
            collection.Add(model2);
            collection.Add(model3);
            var customers = (collection.Sort(CustomerPredicate.SortByPropertyTypeThenProject)).ToList();

            Assert.Equal(value1, customers[0].Project);
            Assert.Equal(value1, customers[0].PropertyType);
            Assert.Equal(value1, customers[1].Project);
            Assert.Equal(value2, customers[1].PropertyType);
            Assert.Equal(value2, customers[2].Project);
            Assert.Equal(value2, customers[2].PropertyType);
        }

        [Theory]
        [InlineData("8/6/1988", "2/9/1990")]
        public void SortByStartDate_Customer(string value1, string value2)
        {
            CustomerModel model1 = new CustomerModel
            {
                PropertyType = value2,
            };
            CustomerModel model2 = new CustomerModel
            {
                PropertyType = value1,
            };

            CustomerCollection collection = new CustomerCollection();
            collection.Add(model1);
            collection.Add(model2);
            var customers = (collection.Sort(CustomerPredicate.SortByStartDate)).ToList();

            Assert.Equal(value2, customers[0].PropertyType);
            Assert.Equal(value1, customers[1].PropertyType);
        }
    }
}
