using System;

public class InfinityHarvester : Harvester
{
    private const double OreOutputDivider = 10d;

    private const double DurabilityLoss = 0d;

    private const double ExactDurability = 1000d;

    private double infiniteDurability;

    public InfinityHarvester(int id, double oreOutput, double energyRequirement) : base(id, oreOutput/OreOutputDivider, energyRequirement,DurabilityLoss)
    {
        this.Durability = ExactDurability;
    }

    public override double Durability
    {
        get => this.infiniteDurability;
        protected set => this.infiniteDurability = Math.Max(1000, value);
    }
}