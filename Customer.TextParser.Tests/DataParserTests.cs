using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Customer.TextParser;

namespace Customer.TextParser.Tests
{
    public class DataParserTests
    {
        static string FirstName = "Summer";
        static string LastName = "Emmerich";
        static string PropertyType = "trailer";
        static string Project = "HVAC";
        static string Date = "2021-07-22";
        static string FormattedDate = "7/22/2021";
        static string Phone = "+43177437725";

        public static IEnumerable<object[]> SingleInput
        {
            get
            {
                return new[]{
                     new object[] { $"{LastName}|{FirstName}|{PropertyType}|{Project}|{Date}|{Phone}", Delimiter.Pipe },
                     new object[] { $"{LastName} {FirstName} {PropertyType} {Project} {Date} {Phone}", Delimiter.Space },
                     new object[] { $"{LastName},{FirstName},{PropertyType},{Project},{Date},{Phone}", Delimiter.Comma }
                 };
            }
        }

        public static IEnumerable<object[]> MultiInput
        {
            get
            {
                return new[]{
                     new object[] { new string[]{$"{LastName}|{FirstName}|{PropertyType}|{Project}|{Date}|{Phone}",$"{LastName}|{FirstName}|{PropertyType}|{Project}|{Date}|{Phone}"}, Delimiter.Pipe },
                     new object[] { new string[]{$"{LastName},{FirstName},{PropertyType},{Project},{Date},{Phone}",$"{LastName},{FirstName},{PropertyType},{Project},{Date},{Phone}"}, Delimiter.Comma },
                     new object[] { new string[]{$"{LastName} {FirstName} {PropertyType} {Project} {Date} {Phone}",$"{LastName} {FirstName} {PropertyType} {Project} {Date} {Phone}"}, Delimiter.Space },
                 };
            }
        }

        [Theory]
        [MemberData(nameof(SingleInput))]
        public void Execute_Should_ReturnCustomerModel(string value, Delimiter value2)
        {
            var cust = DataParser.Execute(value, value2);

            Assert.Equal(cust.FirstName, FirstName);
            Assert.Equal(cust.LastName, LastName);
            Assert.Equal(cust.PropertyType, PropertyType);
            Assert.Equal(cust.Project, Project);
            Assert.Equal(cust.StartDate, FormattedDate);
            Assert.Equal(cust.Phone, Phone);

        }


        [Theory]
        [MemberData(nameof(MultiInput))]
        public void Execute_Should_ReturnListOfCustomerModels(string[] value, Delimiter value2)
        {
            var customers = (DataParser.Execute(value, value2)).ToList();
            Assert.Equal(2, customers.Count);

            foreach (var cust in customers)
            {
                Assert.Equal(cust.FirstName, FirstName);
                Assert.Equal(cust.LastName, LastName);
                Assert.Equal(cust.PropertyType, PropertyType);
                Assert.Equal(cust.Project, Project);
                Assert.Equal(cust.StartDate, FormattedDate);
                Assert.Equal(cust.Phone, Phone);
            }
        }
    }
}
