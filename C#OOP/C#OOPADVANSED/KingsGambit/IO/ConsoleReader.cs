using System;
using KingsGambit.Interfaces;

namespace KingsGambit.IO
{
   public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
