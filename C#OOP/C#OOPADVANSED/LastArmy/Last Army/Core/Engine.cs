
using System;
using System.Linq;

public class Engine : IEngine
{
    private IReader reader;
    private IWriter writer;
    private ICommandFactory commandFactory;
    
    public Engine(IMissionController missionControler, IWareHouse warehouse, IArmy army, IReader reader, IWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
        this.commandFactory = new CommandFactory(army,warehouse,missionControler);
    }
    public void Run()
    {
        string input = string.Empty;

        while ((input = this.reader.ReadLine()) != OutputMessages.ProgramEnd)
        {
            string[] inputTokens = input.Split(new[] {' '}, StringSplitOptions.None).ToArray();
            string commandName = inputTokens[0];

            string[] parameters = inputTokens.Skip(1).ToArray();

            string result = string.Empty;
            result = this.commandFactory.CommandToExecute(commandName, parameters).Execute().Trim();

            if (result != string.Empty)
            {
                this.writer.WriteLine(result);
            }
        }

        this.writer.WriteLine(this.commandFactory.CommandToExecute("Quit",null).Execute().Trim());
    }
}

