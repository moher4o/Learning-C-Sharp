using System;

public abstract class Harvester : IHarvester
{
    private const double InitialDurability = 1000d;

    private int id;
    private double oreOutput;
    private double energyRequirement;
    private double durability;

    protected Harvester(int id, double oreOutput, double energyRequirement, double durabilityModifier)
    {
        this.ID = id;
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
        this.Durability = InitialDurability + durabilityModifier;
    }

    public int ID {
        get { return this.id; }
        private set { this.id = value; }
    }

    public double OreOutput {
        get { return this.oreOutput; }
        protected set { this.oreOutput = value; }
    }

    public double EnergyRequirement {
        get { return this.energyRequirement; }
        protected set { this.energyRequirement = value; }
    }

    public virtual double Durability {
        get { return this.durability; }
        protected set { this.durability = value; }
    }


    public double Produce()
    {
        return this.OreOutput;
    }

    public void Broke()
    {
        this.Durability -= 100;
        if (this.Durability < 0d)
        {
            throw new ArgumentException();
        }
    }
}