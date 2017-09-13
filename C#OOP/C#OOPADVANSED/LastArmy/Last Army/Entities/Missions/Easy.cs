
public class Easy : Mission
{
    private const double endurance = 20d;
    private const double levelDecrement = 30d;
    private const string missionName = "Suppression of civil rebellion";
    public Easy(double score) : base(missionName, score, endurance, levelDecrement)
    {
    }
}

