using System.Collections.Generic;

public class QuitCommand : AbstractCommand
{
    private QuitCommand(IManager manager) : base(manager)
    {
    }

    public QuitCommand(IList<string> args,IManager manager) : base(manager)
    {

    }


    public override string Execute()
    {
        return this.Manager.Quit();
    }
}