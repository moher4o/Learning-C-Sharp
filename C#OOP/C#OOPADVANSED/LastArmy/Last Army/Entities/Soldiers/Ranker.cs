
using System.Collections.Generic;

public class Ranker : Soldier
{
    private const double OverallSkillMiltiplier = 1.5;
    private readonly List<string> weaponsAllowed = new List<string>
    {
        "Gun",
        "AutomaticMachine",
        "Helmet"
    };

    public Ranker(string name, int age, double experience, double endurance) : base(name, age, experience, endurance)
    {
        
    }

    public override IReadOnlyList<string> WeaponsAllowed
    {
        get { return (IReadOnlyList<string>)this.weaponsAllowed.AsReadOnly(); }
    }


    public override double OverallSkill
    {
        get { return (double)(this.Age + this.Experience) * OverallSkillMiltiplier; }
    }

    public override void Regenerate()
    {
        this.Endurance = (double)(10 + this.Age);
    }
}

