
public class Medium : Mission
{
    private const double endurance = 50d;
    private const double levelDecrement = 50d;
    private const string missionName = "Capturing dangerous criminals";
    public Medium(double score) : base(missionName, score, endurance, levelDecrement)
    {
    }
}

