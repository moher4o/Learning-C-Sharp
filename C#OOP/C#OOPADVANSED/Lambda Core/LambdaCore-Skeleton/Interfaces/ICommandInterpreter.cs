namespace LambdaCore_Skeleton.Interfaces
{
    public interface ICommandInterpreter
    {
        ICommandType CommandToExecute(string command, string[] data);
        
    }
}
