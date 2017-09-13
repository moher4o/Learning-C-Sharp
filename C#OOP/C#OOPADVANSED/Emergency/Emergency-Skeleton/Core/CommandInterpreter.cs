using System.Reflection;
using Emergency_Skeleton.Interfaces;


namespace Emergency_Skeleton.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {

        private IEmergencyManagementSystem ems;

        public CommandInterpreter()
        {
            this.ems = new EmergencyManagementSystem();
        }

        public string CommandToExecute(string command, params string[] data)
        {
            MethodInfo currentMethod = this.ems.GetType().GetMethod(command);
            object result;

            if (data != null)
            {
                result = currentMethod.Invoke(this.ems, new object[] {data});
            }
            else
            {
                result = currentMethod.Invoke(this.ems, new object[] { });
            }
            
            return result.ToString();
        }

    }
}
