using System;
using System.Linq;
using LambdaCore_Skeleton.Interfaces;

namespace LambdaCore_Skeleton.Core
{
    public class Engine : IEngine
    {
        private const string EndOFGame = "System Shutdown!";
        private ICommandInterpreter commandInterpreter;
        private IReader reader;
        private IWriter writer;

        public Engine()
        {
            this.commandInterpreter = new CommandInterpreter();
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();

        }
        public void Run()
        {
            string input = string.Empty;

            while ((input = this.reader.ReadLine()) != EndOFGame)
            {
                string[] inputTokens = input.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                string commandName = inputTokens[0];
                string[] parameters = null;
                if (inputTokens.Length > 1)
                {
                    parameters = inputTokens[1].Split(new[] {'@'}, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();
                }

                string result = string.Empty;
                result = this.commandInterpreter.CommandToExecute(commandName, parameters).Execute().Trim();

                if (result != string.Empty)
                {
                    this.writer.WriteLine(result);
                }
            }

        }
    }
}
