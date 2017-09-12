public class HammerHarvester : Harvester
{
    private const double EnergyRequirementMultiplier = 2d;
    private const double OreOutputMultiplier = 4d;
    private const double DurabilityLoss = 0d;

    public HammerHarvester(int id, double oreOutput, double energyRequirement) 
        : base(id, oreOutput*OreOutputMultiplier, energyRequirement*EnergyRequirementMultiplier,DurabilityLoss)
    {
    }
}