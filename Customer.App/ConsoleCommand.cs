using System;
using System.Collections.Generic;

namespace Customer.App
{
     internal enum ConsoleCommandType
    {
        Sort
    }

    internal static class ConsoleCommand
    {
        public static KeyValuePair<ConsoleCommandType, string> Sort = new KeyValuePair<ConsoleCommandType, string>(ConsoleCommandType.Sort, "sort");
    }
}