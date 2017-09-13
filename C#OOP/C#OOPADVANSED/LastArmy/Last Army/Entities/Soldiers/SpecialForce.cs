using System.Collections.Generic;

public class SpecialForce : Soldier
{

    private const double OverallSkillMiltiplier = 3.5;
    private readonly List<string> weaponsAllowed = new List<string>
        {
            "Gun",
            "AutomaticMachine",
            "MachineGun",
            "RPG",
            "Helmet",
            "Knife",
            "NightVision"
        };

    public SpecialForce(string name, int age, double experience, double endurance)
        : base(name, age, experience, endurance)
    {
    }

    public override IReadOnlyList<string> WeaponsAllowed
    {
        get { return (IReadOnlyList<string>)this.weaponsAllowed.AsReadOnly(); }
    }


    public override double OverallSkill
    {
        get { return (double) (this.Age + this.Experience) * OverallSkillMiltiplier; }
    }

    public override void Regenerate()
    {
        this.Endurance = (double)(30 + this.Age);
    }
}
