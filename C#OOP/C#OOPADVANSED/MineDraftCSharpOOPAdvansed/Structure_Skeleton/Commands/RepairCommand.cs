using System;
using System.Collections.Generic;


public class RepairCommand : Command
{
    [Inject]
    private ProviderController providerController;


    public RepairCommand(IList<string> arguments)
    {
        this.Arguments = new List<string>(arguments);
    }

    public override string Execute()
    {
        return this.providerController.Repair(double.Parse(this.Arguments[0]));

    }
}

