
public interface ICommand
{
    string[] Parameters { get; }
    string Execute();
}
