public interface ICommandFactory
{
    ICommand CommandToExecute(string command, string[] data);
}

