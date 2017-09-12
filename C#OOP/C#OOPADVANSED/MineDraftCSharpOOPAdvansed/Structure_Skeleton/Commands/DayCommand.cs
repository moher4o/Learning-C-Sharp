using System;
using System.Collections.Generic;
using System.Text;


public class DayCommand : Command
{
    [Inject]
    private HarvesterController harvesterController;
    [Inject]
    private ProviderController providerController;


    public DayCommand(IList<string> arguments)
    {
        this.Arguments = new List<string>(arguments);
    }
   

    public override string Execute()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(this.providerController.Produce());
        if (this.providerController.TotalEnergyProduced >= this.harvesterController.TotalEnergyRequirement)
        {
            sb.AppendLine(this.harvesterController.Produce());
        }
        else
        {
            sb.AppendLine(string.Format(Constants.OreProducedToday, 0d));
        }

        return sb.ToString();
    }
}

