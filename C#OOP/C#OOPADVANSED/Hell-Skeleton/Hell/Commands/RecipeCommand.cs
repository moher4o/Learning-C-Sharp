
using System.Collections.Generic;

public class RecipeCommand : AbstractCommand
{
    private IList<string> arguments;
    protected RecipeCommand(IManager manager) : base(manager)
    {
    }

    public RecipeCommand(IList<string> args,IManager manager) : this(manager)
    {
        this.arguments = new List<string>(args);
    }

    public override string Execute()
    {
        return this.Manager.AddRecipeToHero(this.arguments);
    }
}

