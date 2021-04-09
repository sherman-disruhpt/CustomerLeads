using System;
using Xunit;
using Customer.App;
using System.Linq;
using System.Collections.Generic;

namespace Customer.App.Tests
{
    public class ConsoleProcessorTests
    {
        static string FirstName = "Summer";
        static string LastName = "Emmerich";
        static string PropertyType = "trailer";
        static string Project = "HVAC";
        static string Date = "7/22/2021";
        static string Phone = "+43177437725";

        public static IEnumerable<object[]> MultiInput
        {
            get
            {
                return new[]{
                     new object[] { new string[]{"./pipe-sample.txt", "--pipe", "sort" , "--last-name" }},
                      new object[] { new string[]{"./comma-sample.txt", "--comma", "sort" , "--start-date" }},
                      new object[] { new string[]{"./space-sample.txt", "--space", "sort" , "--property-type" }},
                 };
            }
        }
         [Theory]
        [MemberData(nameof(MultiInput))]
        public void Execute_Should_ReturnListOfCustomerModels(string[] value)
        {
            var customers = (ConsoleProcessor.Execute(value)).ToList();
            Assert.Single(customers);

            foreach (var cust in customers)
            {
                Assert.Equal(cust.FirstName, FirstName);
                Assert.Equal(cust.LastName, LastName);
                Assert.Equal(cust.PropertyType, PropertyType);
                Assert.Equal(cust.Project, Project);
                Assert.Equal(cust.StartDate, Date);
                Assert.Equal(cust.Phone, Phone);
            }
        }
    }
}
