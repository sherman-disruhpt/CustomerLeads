using System;
using System.Collections.Generic;

namespace Customer.App
{
    internal static class Print
    {
        public static void Help()
        {
            Console.WriteLine("Usage: CustomerLeadsApp [path-to-file] [parser-options] [command] [command-option]");
            Console.WriteLine();
            Console.WriteLine("path-to-file:");
            Console.WriteLine("\t The path to the file to be parsed");
            Console.WriteLine("parser-options:");
            Console.WriteLine("\t --pipe \t Text delimiter \"|\"");
            Console.WriteLine("\t --comma \t Text delimiter \",\"");
            Console.WriteLine("\t --space \t Text delimiter \" \"");
            Console.WriteLine("commands:");
            Console.WriteLine("\t sort \t sorts the customer collection");
            Console.WriteLine("command-options:");
            Console.WriteLine("\t --property-type");
            Console.WriteLine("\t --start-date");
            Console.WriteLine("\t --last-name");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
        }

        public static void Customers(IEnumerable<CustomerModel> customers)
        {
            Console.WriteLine(string.Format("| {0,-15} | {1,-15} | {2,-15} | {3,-19} | {4,-15} | {5,-17} |", "LastName", "FirstName", "PropertyType", "Project", "StartDate", "Phone"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            foreach (var c in customers)
            {
                Console.WriteLine(string.Format("| {0,-15} | {1,-15} | {2,-15} | {3,-19} | {4,-15} | {5,-17} |", c.LastName, c.FirstName, c.PropertyType, c.Project, c.StartDate, c.Phone));
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

        }
    }
}