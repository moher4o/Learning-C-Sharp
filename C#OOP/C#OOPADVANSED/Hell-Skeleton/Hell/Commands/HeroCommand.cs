using System.Collections.Generic;

public class HeroCommand : AbstractCommand
{
    private IList<string> arguments;
    protected HeroCommand(IManager manager) : base(manager)
    {
        
    }

    public HeroCommand(IList<string> args, IManager manager) : this(manager)
    {
        this.arguments = new List<string>(args);
    }

    public override string Execute()
    {
        return this.Manager.AddHero(this.arguments);
    }
}

