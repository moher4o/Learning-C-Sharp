
using System.Collections.Generic;

public class ItemCommand : AbstractCommand
{
    private IList<string> arguments;
    protected ItemCommand(IManager manager) : base(manager)
    {
    }

    public ItemCommand(IList<string> args, IManager manager) : this(manager)
    {
        this.arguments = new List<string>(args);
    }

    public override string Execute()
    {
        return this.Manager.AddItemToHero(this.arguments);
    }
}
