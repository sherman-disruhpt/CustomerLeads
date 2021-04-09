using System;
using System.Collections.Generic;

namespace Customer.TextParser
{
    public static class DataParser
    {
        private static string[] GetValues(string data, Delimiter delimiter)
        {
            switch (delimiter)
            {
                case Delimiter.Pipe:
                    return data.Split('|');
                case Delimiter.Comma:
                    return data.Split(',');
                case Delimiter.Space:
                    return data.Split(' ');
                default:
                    return null;
            }
        }

        public static CustomerModel Execute(string data, Delimiter delimiter)
        {
            string[] values = GetValues(data, delimiter);
            
            if(values.Length != 6){
                throw new DataParsingException($"{data} has {values.Length} fields and 6 are required");
            }

            return new CustomerModel
            {
                Id = Guid.NewGuid(),
                LastName = values[0].Trim(),
                FirstName = values[1].Trim(),
                PropertyType = values[2].Trim().ToLower(),
                Project = values[3].Trim(),
                StartDate = DateTime.Parse(values[4].Trim()).ToShortDateString(),
                Phone = values[5].Trim(),
            };
        }
        public static List<CustomerModel> Execute(IEnumerable<string> data, Delimiter delimiter)
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (var d in data)
            {
                customers.Add(Execute(d, delimiter));
            }

            return customers;
        }
    }
}
