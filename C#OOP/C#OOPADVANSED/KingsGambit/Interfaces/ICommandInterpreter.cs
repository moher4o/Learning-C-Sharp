using KingsGambit.Entities;

namespace KingsGambit.Interfaces
{
   public interface ICommandInterpreter
   {
       King King { get; set; }
        string Name { get; set; }
       IExecutable InterpretCommand(string commandName);
    }
}
