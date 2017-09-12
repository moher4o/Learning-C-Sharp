
public class PressureProvider : Provider
{
    private const double DurabilityModifier = -300d;
    private const double EnergyMultiplier = 2d;
    public PressureProvider(int id, double energyOutput) : base(id, DurabilityModifier, energyOutput * EnergyMultiplier)
    {
    }
}
