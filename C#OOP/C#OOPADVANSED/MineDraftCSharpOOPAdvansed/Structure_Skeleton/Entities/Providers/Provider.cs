using System;

public abstract class Provider : IProvider
{
    private const double InitialDurability = 1000d;
    private int id;
    private double durability;
    private double energyOutput;

    protected Provider(int id, double durabilityModifier, double energyOutput)
    {
        this.ID = id;
        this.Durability = InitialDurability+durabilityModifier;
        this.EnergyOutput = energyOutput;
    }

    public int ID {
        get { return this.id; }
        private set { this.id = value; }
    }
    public double Durability {
        get { return this.durability; }
        protected set { this.durability = value; }
    }
    public double EnergyOutput {
        get { return this.energyOutput; }
        protected set { this.energyOutput = value; }
    }

    public double Produce()
    {
        return this.EnergyOutput;
    }

    public void Broke()
    {
        this.Durability -= 100d;
        if (this.Durability < 0d)
        {
           throw new ArgumentException();
        }
        
    }

    
    public void Repair(double val)
    {
        this.Durability += val;
    }
}