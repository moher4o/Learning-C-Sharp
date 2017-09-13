
using System.Collections.Generic;

public class InspectCommand : AbstractCommand
{
    private IList<string> arguments;
    private InspectCommand(IManager manager) : base(manager)
    {
    }
    public InspectCommand(IList<string> args,IManager manager) : base(manager)
    {
        this.arguments = new List<string>(args);
    }

    public override string Execute()
    {
        return this.Manager.Inspect(this.arguments);
    }
}
