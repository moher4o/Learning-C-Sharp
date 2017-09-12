public class StandartHarvester : Harvester
{
    private const double DurabilityLoss = 0d;
    public StandartHarvester(int id, double oreOutput, double energyRequirement) : base(id, oreOutput,
        energyRequirement, DurabilityLoss)
    {
    }
}