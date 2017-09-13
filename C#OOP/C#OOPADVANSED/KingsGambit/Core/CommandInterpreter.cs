using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using KingsGambit.Entities;
using KingsGambit.Interfaces;
public class CommandInterpreter : ICommandInterpreter
{
    private King king;
    private string name;

    public King King
    {
        get { return this.king; }
        set { this.king = value; }
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }
    public IExecutable InterpretCommand(string commandName)
    {
        string completeCommand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(commandName.ToLower()) + "Command";
        Type commandType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == completeCommand);
        if (commandType == null)
        {
            throw new InvalidOperationException("Invalid command!");
        }

        object[] commandParams = {};

        IExecutable currentCommand = (IExecutable)Activator.CreateInstance(commandType, commandParams);

        currentCommand = this.InjectDependences(currentCommand);
        return currentCommand;
    }

    private IExecutable InjectDependences(IExecutable currentCommand)
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

