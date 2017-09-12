using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;


public class CommandInterpreter : ICommandInterpreter
{
    private HarvesterController harvesterController;
    private ProviderController providerController;

    public CommandInterpreter(HarvesterController harvesterController, ProviderController providerController)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public IHarvesterController HarvesterController { get; }
    public IProviderController ProviderController { get; }

    public string ProcessCommand(IList<string> args)
    {
        string command = args[0];
        object[] parameters = { args.Skip(1).ToList() };
        string completeCommand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command) + "Command";
        Type completeCommandType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == completeCommand);
        ICommand currentCommand = (ICommand)Activator.CreateInstance(completeCommandType, parameters);
        currentCommand = this.InjectDependences(currentCommand);
        return currentCommand.Execute();
        
    }


    private ICommand InjectDependences(ICommand currentCommand)
    {
        FieldInfo[] currentCommandFields = currentCommand.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(f => f.GetCustomAttribute<InjectAttribute>() != null).ToArray();

        FieldInfo[] interpeterFileds = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (FieldInfo currentCommandField in currentCommandFields)
        {
            FieldInfo interpreterField = interpeterFileds.Where(f => f.FieldType == currentCommandField.FieldType)
                .FirstOrDefault();
            object valueToInject = interpreterField.GetValue(this);

            currentCommandField.SetValue(currentCommand, valueToInject);
        }

        return currentCommand;
    }

}

