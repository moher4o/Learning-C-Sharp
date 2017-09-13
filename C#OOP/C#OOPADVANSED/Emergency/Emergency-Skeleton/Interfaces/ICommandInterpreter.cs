namespace Emergency_Skeleton.Interfaces
{
    public interface ICommandInterpreter
    {
        string CommandToExecute(string command, string[] data);
        
    }
}
