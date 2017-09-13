namespace LambdaCore_Skeleton.Commands
{
    public class RemoveCoreCommand : Command
    {

        public RemoveCoreCommand(string[] parameters) : base(parameters)
        {
        }

        public override string Execute()
        {
            if (this.aec.Remove(this.Parameters[0]))
            {
                return $"Successfully removed Core {this.Parameters[0]}!";
            }
            else
            {
                return $"Failed to remove Core {this.Parameters[0]}!";
            }
        }
    }
}
