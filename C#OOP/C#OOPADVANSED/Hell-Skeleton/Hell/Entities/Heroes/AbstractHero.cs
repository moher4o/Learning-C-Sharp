using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class AbstractHero : IHero, IComparable<AbstractHero>
{
    private IInventory inventory;
    private long strength;
    private long agility;
    private long intelligence;
    private long hitPoints;
    private long damage;

    protected AbstractHero(string name, long strength, long agility, long intelligence, long hitPoints, long damage)
    {
        this.Name = name;
        this.strength = strength;
        this.agility = agility;
        this.intelligence = intelligence;
        this.hitPoints = hitPoints;
        this.damage = damage;
        this.inventory = new HeroInventory();
    }

    public string Name { get; private set; }

    public long Strength
    {
        get { return this.strength + this.inventory.TotalStrengthBonus; }
        set { this.strength = value; }
    }

    public long Agility
    {
        get { return this.agility + this.inventory.TotalAgilityBonus; }
        set { this.agility = value; }
    }

    public long Intelligence
    {
        get { return this.intelligence + this.inventory.TotalIntelligenceBonus; }
        set { this.intelligence = value; }
    }

    public long HitPoints
    {
        get { return this.hitPoints + this.inventory.TotalHitPointsBonus; }
        set { this.hitPoints = value; }
    }

    public long Damage
    {
        get { return this.damage + this.inventory.TotalDamageBonus; }
        set { this.damage = value; }
    }

    public long PrimaryStats
    {
        get { return this.Strength + this.Agility + this.Intelligence; }
    }

    public long SecondaryStats
    {
        get { return this.HitPoints + this.Damage; }
    }

    //REFLECTION
    public ICollection<IItem> Items
    {
        get
        {
            Type invertoryType = this.inventory.GetType();
            FieldInfo fieldItem = invertoryType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(t => t.GetCustomAttribute<ItemAttribute>() != null);
            IDictionary<string, IItem> itemsInInventory = new Dictionary<string, IItem>();
            
            itemsInInventory = (Dictionary<string, IItem>)fieldItem.GetValue(this.inventory);

            return itemsInInventory.Values;
        }
    }

    public void AddItem(IItem item)
    {
        this.inventory.AddCommonItem(item);
    }


    public void AddRecipe(IRecipe recipe)
    {
        this.inventory.AddRecipeItem(recipe);
    }
    
    public int CompareTo(AbstractHero other)
    {
        //if (ReferenceEquals(this, other))
        //{
        //    return 0;
        //}
        //if (ReferenceEquals(null, other))
        //{
        //    return 1;
        //}
        var primary = this.PrimaryStats.CompareTo(other.PrimaryStats);
        if (primary != 0)
        {
            return primary;
        }
        var secondary = this.SecondaryStats.CompareTo(other.SecondaryStats);
        if (secondary != 0)
        {
            return secondary;
        }
        return this.Name.CompareTo(other.Name);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Hero: {this.Name}, Class: {this.GetType().Name}");
        sb.AppendLine($"HitPoints: {this.HitPoints}, Damage: {this.Damage}");
        sb.AppendLine($"Strength: {this.Strength}");
        sb.AppendLine($"Agility: {this.Agility}");
        sb.AppendLine($"Intelligence: {this.Intelligence}");
        if (this.Items.Count != 0)
        {
            sb.AppendLine("Items:");
            foreach (IItem item in this.Items)
            {
                sb.AppendLine($"###Item: {item.Name}");
                sb.AppendLine($"###+{item.StrengthBonus} Strength");
                sb.AppendLine($"###+{item.AgilityBonus} Agility");
                sb.AppendLine($"###+{item.IntelligenceBonus} Intelligence");
                sb.AppendLine($"###+{item.HitPointsBonus} HitPoints");
                sb.AppendLine($"###+{item.DamageBonus} Damage");
            }
        }
        else
        {
            sb.AppendLine("Items: None");
        }
        return sb.ToString().Trim();
    }
}