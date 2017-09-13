namespace LambdaCore_Skeleton.Interfaces
{
   public interface ICommandType
    {
        string[] Parameters { get; }
        string Execute();
    }
}
