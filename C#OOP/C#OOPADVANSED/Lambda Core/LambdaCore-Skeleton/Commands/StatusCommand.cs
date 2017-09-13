using System.Linq;
using System.Text;
using LambdaCore_Skeleton.Interfaces;

namespace LambdaCore_Skeleton.Commands
{
   public class StatusCommand : Command
    {
        public StatusCommand(string[] parameters) : base(parameters)
        {
        }

        public override string Execute()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Lambda Core Power Plant Status:");
            sb.AppendLine($"Total Durability: {this.aec.Values.Sum(c => c.Durability)}");
            sb.AppendLine($"Total Cores: {this.aec.Count}");
            sb.AppendLine($"Total Fragments: {this.aec.Values.Sum(c => c.Fragments.Count)}");

            foreach (ICore core in this.aec.Values)
            {
                 sb.AppendLine($"Core {core.Name}:");
                var pressureAffection = core.Fragments.Sum(f => f.PressureAffection);

                if (pressureAffection <= 0)
                {
                    sb.AppendLine($"####Durability: {core.Durability:f0}");
                    sb.AppendLine("####Status: NORMAL");
                }
                else
                {
                    sb.AppendLine($"####Durability: {core.Durability:f0}");
                    sb.AppendLine("####Status: CRITICAL");
                }

            }

            return sb.ToString().Trim();
        }
    }
}
