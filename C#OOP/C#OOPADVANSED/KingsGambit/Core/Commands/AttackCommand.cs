
using KingsGambit.Core.Commands;
using KingsGambit.Entities;

public class AttackCommand : Command
{
    [Inject]
    private King king;

    public override void Execute()
    {
        this.king.Attack();
    }
}

