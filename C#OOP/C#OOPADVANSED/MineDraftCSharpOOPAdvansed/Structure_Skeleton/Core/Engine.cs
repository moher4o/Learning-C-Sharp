using System;
using System.Collections.Generic;
using System.Linq;

public class Engine : IEngine
{

    private ICommandInterpreter interpreter;
    private readonly IReader reader;
    private readonly IWriter writer;

    public Engine(ICommandInterpreter interpreter, IReader reader, IWriter writer)
    {

        this.interpreter = interpreter;
        this.reader = reader;
        this.writer = writer;
    }

    public void Run()
    {
        string input = string.Empty;
        bool quit = false;

        while (!quit)
        {
            input = this.reader.ReadLine();

            IList<string> inputTokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (inputTokens[0] == Constants.EndOfGame)
            {
                quit = true;
            }


            string result = string.Empty;
            result = this.interpreter.ProcessCommand(inputTokens).Trim();

            if (result != string.Empty)
            {
                this.writer.WriteLine(result);
            }
        }
    }
}
