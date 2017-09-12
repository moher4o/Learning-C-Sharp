public class SonicHarvester : Harvester
{
    private const double EnergyRequirementDivider = 2d;
    private const double DurabilityLoss = -300d;

    public SonicHarvester(int id, double oreOutput, double energyRequirement) 
        : base(id, oreOutput, energyRequirement/EnergyRequirementDivider,DurabilityLoss)
    {
    }
}