using System.Linq;
using LambdaCore_Skeleton.Factories;

namespace LambdaCore_Skeleton.Commands
{
    public class CreateCoreCommand : Command
    {
        private CoreFactory coreFactory;
        public CreateCoreCommand(string[] parameters) : base(parameters)
        {
            this.coreFactory = new CoreFactory();
        }

        public override string Execute()
        {
            string type = this.Parameters[0];
            double durability = double.Parse(this.Parameters[1]);
            if (!(((type == "System") || (type == "Para")) && durability >= 0d))
            {
                return "Failed to create Core!";
            }
            string lastCoreName = this.aec.Keys.LastOrDefault();
            char nextName;
            if (lastCoreName == null)
            {
                this.aec["A"] = this.coreFactory.GetCore(type, "A", durability);
                nextName = 'A';
            }
            else
            {
                nextName = (char)(lastCoreName[0]+1);
                this.aec[nextName.ToString()] = this.coreFactory.GetCore(type, nextName.ToString(), durability);
            }
            return $"Successfully created Core {nextName}!";
           
        }
    }
}
