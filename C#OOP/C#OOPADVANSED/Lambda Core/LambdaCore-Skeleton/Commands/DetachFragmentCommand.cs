namespace LambdaCore_Skeleton.Commands
{
    public class DetachFragmentCommand : Command
    {

        public DetachFragmentCommand(string[] parameters) : base(parameters)
        {
        }

        public override string Execute()
        {
            string fragmentName = string.Empty;
            if (this.selectedCore.Name == "No")
            {
                return "Failed to attach Fragment!";
            }

            if ((fragmentName = this.aec[this.selectedCore.Name].RemoveFragment()) == null)
            {
                return "Failed to attach Fragment!";
            }
            else
            {
                return $"Successfully detached Fragment {fragmentName} from Core {this.selectedCore.Name}!";
            }

            
        }
    }
}
