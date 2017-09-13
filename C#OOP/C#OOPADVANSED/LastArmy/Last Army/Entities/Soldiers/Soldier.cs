using System.Collections.Generic;
using System.Linq;

public abstract class Soldier : ISoldier
{
    private string name;
    private int age;
    private double endurance;
    private double experience;

    protected Soldier(string name, int age, double experience, double endurance)
    {
        this.Name = name;
        this.Age = age;
        this.endurance = endurance;
        this.Experience = experience;
        this.Weapons = new Dictionary<string, IAmmunition>();
        foreach (string weapon in this.WeaponsAllowed)
        {
            this.Weapons[weapon] = null;
        }
    }

    public string Name
    {
        get { return this.name; }
        protected set { this.name = value; }
    }

    public int Age
    {
        get { return this.age; }
        protected set { this.age = value; }
    }

    public double Experience
    {
        get { return this.experience; }
        protected set
        {
            this.experience = value;
            
        }
    }

    public double Endurance
    {
        get { return this.endurance; }
        protected set {
            if (value + this.endurance < 0)
            {
                this.endurance = 0;
            }
            else if (this.endurance + value > 100)
            {
                this.endurance = 100;
            }
            else
            {
                this.endurance = this.endurance + value;
            }
        }
    }

    public abstract double OverallSkill
    {
        get;
    }
    
    public IDictionary<string, IAmmunition> Weapons { get; protected set; }
    
    public abstract IReadOnlyList<string> WeaponsAllowed { get; }

    public abstract void Regenerate();

    public bool ReadyForMission(IMission mission)
    {
        if (this.Endurance < mission.EnduranceRequired)
        {
            return false;
        }

        bool hasAllEquipment = this.Weapons.Values.Count(weapon => weapon == null) == 0;

        if (!hasAllEquipment)
        {
            return false;
        }

        return this.Weapons.Values.Count(weapon => weapon.WearLevel <= 0) == 0;
    }

    public void CompleteMission(IMission mission)
    {
        this.Experience += mission.EnduranceRequired;
        this.Endurance = -mission.EnduranceRequired;
        foreach (var weapon in this.Weapons.Values)
        {
            if (weapon!= null)
            {
                weapon.DecreaseWearLevel(mission.WearLevelDecrement);
            }
        }
        this.AmmunitionRevision();
    }

    private void AmmunitionRevision()
    {
        IEnumerable<string> keys = this.Weapons.Keys.ToList();
        foreach (string weaponName in keys)
        {
            if (this.Weapons[weaponName].WearLevel <= 0)
            {
                this.Weapons[weaponName] = null;
            }
        }
    }

    public override string ToString() => string.Format(OutputMessages.SoldierToString, this.Name, this.OverallSkill);
}