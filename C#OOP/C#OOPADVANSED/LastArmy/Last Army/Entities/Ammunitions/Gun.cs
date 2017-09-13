public class Gun : Ammunition
{
    private const double CurrentWeight = 1.4;

    public Gun(string name)
        : base(name, CurrentWeight)
    {
    }
}
