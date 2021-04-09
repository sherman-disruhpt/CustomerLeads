using Customer;
using Customer.TextParser;

namespace Customer.App
{
    internal class ConsoleInput
    {
        public string FilePath { get; set; }
        public Delimiter Delimiter { get; set; }
        public ConsoleCommandType Command { get; set; }
        public CustomerPredicate CommandOption { get; set; }
    }
}