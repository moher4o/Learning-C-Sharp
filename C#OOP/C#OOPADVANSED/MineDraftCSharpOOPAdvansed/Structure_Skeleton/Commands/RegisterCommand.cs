
using System.Collections.Generic;
using System.Linq;

public class RegisterCommand : Command
{
    [Inject]
    private HarvesterController harvesterController;
    [Inject]
    private ProviderController providerController;


    private IList<string> arguments;

    public RegisterCommand(IList<string> arguments)
    {
        this.Arguments = new List<string>(arguments);
    }

 public override string Execute()
    {
        string result = string.Empty;
        if (this.Arguments[0] == "Harvester")
        {
            result = this.harvesterController.Register(this.Arguments.Skip(1).ToList());
        }
        else
        {
            result = this.providerController.Register(this.Arguments.Skip(1).ToList());

        }
        return result;
    }
}

