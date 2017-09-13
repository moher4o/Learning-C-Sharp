using System;
using KingsGambit.Interfaces;

namespace KingsGambit.IO
{
   public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
