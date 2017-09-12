using System.Collections.Generic;
using System.Linq;
using System.Text;


public class InspectCommand : Command
{

    [Inject]
    private HarvesterController harvesterController;
    [Inject]
    private ProviderController providerController;


    public InspectCommand(IList<string> arguments)
    {
        this.Arguments = new List<string>(arguments);
    }
 
    public override string Execute()
    {
        StringBuilder sb = new StringBuilder();
        int id = int.Parse(this.Arguments[0]);
        if (this.providerController.Entities.FirstOrDefault(en => en.ID == id) != null)
        {

           sb.AppendLine(this.providerController.Entities.FirstOrDefault(en => en.ID == id).GetType().Name);
            sb.AppendLine(
                $"Durability: {this.providerController.Entities.FirstOrDefault(en => en.ID == id).Durability}");
            return sb.ToString();
        }
        else if (this.harvesterController.Entities.FirstOrDefault(en => en.ID == id) != null)
        {
            sb.AppendLine(this.harvesterController.Entities.FirstOrDefault(en => en.ID == id).GetType().Name);
            sb.AppendLine(
                $"Durability: {this.harvesterController.Entities.FirstOrDefault(en => en.ID == id).Durability}");
            return sb.ToString();
        }
        sb.AppendLine($"No entity found with id - {id}");
        return sb.ToString();

    }
}
