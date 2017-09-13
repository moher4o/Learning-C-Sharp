
public abstract class Ammunition : IAmmunition
{
    protected Ammunition(string name, double weight)
    {
        this.Name = name;
        this.Weight = weight;
        this.WearLevel = this.Weight*100;
    }
    public string Name { get; protected set; }
    public double Weight { get; protected set; }
    public double WearLevel { get; protected set; }
    public void DecreaseWearLevel(double wearAmount)
    {
        this.WearLevel -= wearAmount;
    }
}

 