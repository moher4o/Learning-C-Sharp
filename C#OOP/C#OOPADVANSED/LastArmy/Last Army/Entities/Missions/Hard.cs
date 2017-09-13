
public class Hard : Mission
{
    private const double endurance = 80d;
    private const double levelDecrement = 70d;
    private const string missionName = "Disposal of terrorists";
    public Hard(double score) : base(missionName, score, endurance, levelDecrement)
    {
    }
}

