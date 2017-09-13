using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class HeroManager : IManager
{
    private Dictionary<string, AbstractHero> heroes;

    public HeroManager()
    {
        this.heroes = new Dictionary<string, AbstractHero>();
    }


    public string AddHero(IList<String> arguments)
    {
        string result = null;

        string heroName = arguments[0];
        string heroType = arguments[1];

        try
        {
            //string path = Assembly.GetExecutingAssembly().GetTypes().GetType().FullName;
            Type clazz = Type.GetType(heroType);
            var constructors = clazz.GetConstructors();
            AbstractHero hero = (AbstractHero) constructors[0].Invoke(new object[] {heroName});

            this.heroes[heroName] = hero;
            //result = string.Format($"Created {heroType} - {hero.GetType().Name}");
            result = string.Format(Constants.HeroCreateMessage, heroType, heroName);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return result;
    }

    public string AddItemToHero(IList<String> arguments)
    {
        string result = null;

        //Ма те много бе!
        string itemName = arguments[0];
        string heroName = arguments[1];
        int strengthBonus = int.Parse(arguments[2]);
        int agilityBonus = int.Parse(arguments[3]);
        int intelligenceBonus = int.Parse(arguments[4]);
        int hitPointsBonus = int.Parse(arguments[5]);
        int damageBonus = int.Parse(arguments[6]);

        CommonItem newItem = new CommonItem(itemName, strengthBonus, agilityBonus, intelligenceBonus, hitPointsBonus,
            damageBonus);

        this.heroes[heroName].AddItem(newItem);
        //тука трябваше да добавя към hero ама промених едно нещо и то много неща се счупиха и реших просто да не добавям

        result = string.Format(Constants.ItemCreateMessage, newItem.Name, heroName);
        return result;
    }

    public string AddRecipeToHero(IList<String> arguments)
    {
        
        string recipeName = arguments[0];
        string heroName = arguments[1];
        int strengthBonus = int.Parse(arguments[2]);
        int agilityBonus = int.Parse(arguments[3]);
        int intelligenceBonus = int.Parse(arguments[4]);
        int hitPointsBonus = int.Parse(arguments[5]);
        int damageBonus = int.Parse(arguments[6]);

        string[] itemsNames = arguments.Skip(6).ToArray();

        RecipeItem recipeItem = new RecipeItem(recipeName, strengthBonus, agilityBonus, intelligenceBonus, hitPointsBonus,
            damageBonus, itemsNames);
        this.heroes[heroName].AddRecipe(recipeItem);

        return string.Format(Constants.RecipeCreatedMessage,recipeName,heroName);
    }

    public string CreateGame()
    {
        StringBuilder result = new StringBuilder();

        foreach (var hero in heroes)
        {
            result.AppendLine(hero.Key);
        }

        return result.ToString();
    }

    public string Inspect(IList<String> arguments)
    {
        string heroName = arguments[0];

        return this.heroes[heroName].ToString();
    }

    public string Quit()
    {
        SortedSet<AbstractHero> sortedHeroes = new SortedSet<AbstractHero>();
        foreach (AbstractHero hero in this.heroes.Values)
        {
            sortedHeroes.Add(hero);
        }
        
        StringBuilder sb = new StringBuilder();
        int counter = 0;
        
        foreach (AbstractHero hero in sortedHeroes.Reverse())
        {
            sb.AppendLine($"{++counter}. {hero.GetType().Name}: {hero.Name}");
            sb.AppendLine($"###HitPoints: {hero.HitPoints}");
            sb.AppendLine($"###Damage: {hero.Damage}");
            sb.AppendLine($"###Strength: {hero.Strength}");
            sb.AppendLine($"###Agility: {hero.Agility}");
            sb.AppendLine($"###Intelligence: {hero.Intelligence}");
            if (hero.Items.Count == 0)
            {
                sb.AppendLine($"###Items: None");
            }
            else
            {
                List<string> items = new List<string>();
                foreach (var item in hero.Items)
                {
                    items.Add(item.Name);
                }
                sb.AppendLine($"###Items: {string.Join(", ", items)}");
            }
            
        }
        return sb.ToString();
    }

}