using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LambdaCore_Skeleton.Attributes;
using LambdaCore_Skeleton.Interfaces;
using LambdaCore_Skeleton.Models.Cores;

namespace LambdaCore_Skeleton.Core
{
   public class CommandInterpreter : ICommandInterpreter
   {
       private ICore selectedCore;
       private IDictionary<string, ICore> aec;

       public CommandInterpreter()
       {
           this.selectedCore = new ParaCore("No",2);
           this.aec = new Dictionary<string, ICore>();
       }

        public ICommandType CommandToExecute(string command, params string[] data)
        {
            string completeCommand = command + "Command";
            Type completeCommandType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == completeCommand);

            object[] parameters = { data };

            ICommandType currentCommand = (ICommandType)Activator.CreateInstance(completeCommandType, parameters);
            currentCommand = this.InjectDependences(currentCommand);

            return currentCommand;

        }

        private ICommandType InjectDependences(ICommandType currentCommand)
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
}
