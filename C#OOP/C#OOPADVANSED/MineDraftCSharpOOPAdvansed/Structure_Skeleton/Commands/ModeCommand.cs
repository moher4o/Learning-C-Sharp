using System;
using System.Collections.Generic;


public class ModeCommand : Command
{
    [Inject]
    private HarvesterController harvesterController;

    public ModeCommand(IList<string> arguments)
    {
        this.Arguments = new List<string>(arguments);
    }

    public override string Execute()
    {
        return this.harvesterController.ChangeMode(this.Arguments[0]);
    }
}

