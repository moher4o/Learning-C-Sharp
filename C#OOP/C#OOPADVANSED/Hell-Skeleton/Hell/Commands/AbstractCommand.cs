using System.Collections.Generic;

public abstract class AbstractCommand : ICommand
{
    protected IManager Manager;

    protected AbstractCommand(IManager manager)
    {
        this.Manager = manager;

    }
    public abstract string Execute();
}

