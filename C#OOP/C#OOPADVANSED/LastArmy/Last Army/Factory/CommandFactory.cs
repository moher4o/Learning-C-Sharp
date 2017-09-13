using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

public class CommandFactory : ICommandFactory
{
    private IArmy army;
    private IWareHouse warehouse;
    private IMissionController missionController;
    public CommandFactory(IArmy army, IWareHouse warehouse, IMissionController missionController)
    {
        this.army = army;
        this.warehouse = warehouse;
        this.missionController = missionController;
    }

    public ICommand CommandToExecute(string command, string[] data)
    {
        string completeCommand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command) + "Command";
        Type completeCommandType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == completeCommand);

        object[] parameters = { data };

        ICommand currentCommand = (ICommand) Activator.CreateInstance(completeCommandType, parameters);
        currentCommand = this.InjectDependences(currentCommand);

        return currentCommand;

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
