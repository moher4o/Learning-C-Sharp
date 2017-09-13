using System;
using System.Linq;
using Emergency_Skeleton.Constants;
using Emergency_Skeleton.Interfaces;

namespace Emergency_Skeleton.Core
{
    public class Engine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private ICommandInterpreter commandInterpreter;

        public Engine()
        {
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();
            this.commandInterpreter = new CommandInterpreter();
        }


        public void Run()
        {
            string input = string.Empty;

            while ((input = this.reader.ReadLine()) != Constant.EndOFGame)
            {
                string[] inputTokens = input.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                string commandName = inputTokens[0];
                string[] parameters = null;
                if (inputTokens.Length > 1)
                {
                    parameters = inputTokens.Skip(1).ToArray();
                }

                string result = string.Empty;
                result = this.commandInterpreter.CommandToExecute(commandName, parameters).Trim();

                if (result != string.Empty)
                {
                    this.writer.WriteLine(result);
                }
            }
        }
    }
}
