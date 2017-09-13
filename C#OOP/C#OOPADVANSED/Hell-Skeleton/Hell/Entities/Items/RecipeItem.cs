using System.Collections.Generic;

public class RecipeItem : AbstractItem, IRecipe
{
    private IList<string> requiredItems;

    public RecipeItem(string name, int strengthBonus, int agilityBonus, int intelligenceBonus, int hitPointsBonus, int damageBonus, params string[] requiredItems) : base(name, strengthBonus, agilityBonus, intelligenceBonus, hitPointsBonus, damageBonus)
    {
        this.RequiredItems = new List<string>(requiredItems);
    }

    public IList<string> RequiredItems {
        get { return this.requiredItems; }
        set { this.requiredItems = value; }
    }


}
