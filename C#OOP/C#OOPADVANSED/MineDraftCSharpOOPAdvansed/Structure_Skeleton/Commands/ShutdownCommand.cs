using System;
using System.Collections.Generic;
using System.Text;


public class ShutdownCommand : Command
{
    [Inject]
    private HarvesterController harvesterController;
    [Inject]
    private ProviderController providerController;

    public ShutdownCommand(IList<string> arguments)
    {
        this.Arguments = new List<string>(arguments);
    }

    public override string Execute()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("System Shutdown");
        sb.AppendLine(string.Format(Constants.TotalEnergyProduced, this.providerController.TotalEnergyProduced));
        sb.AppendLine(string.Format(Constants.TotalOreProduced, this.harvesterController.OreProduced));

        return sb.ToString();

    }
}

