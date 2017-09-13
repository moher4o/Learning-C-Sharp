namespace LambdaCore_Skeleton.Commands
{
   public class SelectCoreCommand : Command
    {

        public SelectCoreCommand(string[] parameters) : base(parameters)
        {
        }

        public override string Execute()
        {
            if (this.aec.ContainsKey(this.Parameters[0]))
            {
                this.selectedCore.ChangeName(this.aec[this.Parameters[0]].Name);
                return $"Currently selected Core {this.selectedCore.Name}!";
            }
            else
            {
                return $"Failed to select Core {this.Parameters[0]}!";
            }
            
        }
    }
}
