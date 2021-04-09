using System.Collections.Generic;
using System.IO;
using Customer;
using Customer.TextParser;

namespace Customer.App
{
    public static class ConsoleProcessor
    {
        private static void ParseFilePath(string arg, ref ConsoleInput input)
        {
            if (File.Exists(arg))
            {
                input.FilePath = arg;
            }
            else if (arg == ConsoleOptions.Help)
            {
                input.FilePath = null;
            }
        }

        private static void ParseDelimiter(string arg, ref ConsoleInput input)
        {
            switch (arg)
            {
                case ConsoleOptions.Pipe:
                    input.Delimiter = Delimiter.Pipe;
                    break;
                case ConsoleOptions.Space:
                    input.Delimiter = Delimiter.Space;
                    break;
                case ConsoleOptions.Comma:
                    input.Delimiter = Delimiter.Comma;
                    break;
            }
        }

        private static void ParseCommand(string arg, ref ConsoleInput input)
        {
            if (arg == ConsoleCommand.Sort.Value)
            {
                input.Command = ConsoleCommand.Sort.Key;
            }
        }

        private static void ParseCommandOption(string arg, ref ConsoleInput input)
        {
            switch (arg)
            {
                case ConsoleOptions.SortByLastName:
                    input.CommandOption = CustomerPredicate.SortByLastName;
                    break;
                case ConsoleOptions.SortByStartDate:
                    input.CommandOption = CustomerPredicate.SortByStartDate;
                    break;
                case ConsoleOptions.SortByPropertyType:
                    input.CommandOption = CustomerPredicate.SortByPropertyTypeThenProject;
                    break;
            }
        }

        public static IEnumerable<CustomerModel> Execute(string[] args)
        {
            ConsoleInput input = new ConsoleInput();
            ParseFilePath(args[0], ref input);
            IEnumerable<string> lines = null;

            if (input.FilePath != null)
            {
                lines = File.ReadLines(input.FilePath);
                ParseDelimiter(args[1], ref input);
                ParseCommand(args[2], ref input);
                ParseCommandOption(args[3], ref input);
            }
            
            CustomerCollection collection = new CustomerCollection();
            var customers = DataParser.Execute(lines, input.Delimiter);
            collection.AddRange(customers);

            return collection.Sort(input.CommandOption);
        }
    }
}