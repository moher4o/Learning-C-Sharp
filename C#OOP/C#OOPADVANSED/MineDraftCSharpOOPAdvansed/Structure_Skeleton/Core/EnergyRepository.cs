
public class EnergyRepository : IEnergyRepository
{
    private double energyStored;

    public EnergyRepository()
    {
        this.EnergyStored = 0d;
    }

    public double EnergyStored
    {
        get { return this.energyStored; }
        private set { this.energyStored = value; }
    }

    public bool TakeEnergy(double energyNeeded)
    {
        if (this.energyStored >= energyNeeded)
        {
            this.energyStored -= energyNeeded;
            return true;
        }
        return false;
    }

    public void StoreEnergy(double energy)
    {
        this.energyStored += energy;
    }
}

