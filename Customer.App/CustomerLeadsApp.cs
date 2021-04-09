using System;
using System.Collections.Generic;
using Customer;

namespace Customer.App
{
    class CustomerLeadsApp
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<CustomerModel> customers = ConsoleProcessor.Execute(args);
                Print.Customers(customers);
            }
            catch
            {
                Print.Help();
            }
        }
    }
}
