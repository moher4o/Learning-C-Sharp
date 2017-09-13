using KingsGambit.Core;
using KingsGambit.Interfaces;
using KingsGambit.IO;

namespace KingsGambit
{
    class Startup
    {
        static void Main(string[] args)
        {
            
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            Engine engin = new Engine(commandInterpreter,reader,writer);
            engin.Run();
        }
    }
}
