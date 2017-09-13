using KingsGambit.Core.Commands;
using KingsGambit.Entities;

public class KillCommand : Command
{
    [Inject]
    private King king;
    [Inject]
    private string name;


    public override void Execute()
    {
       this.king.Kill(this.name);
    }
}

