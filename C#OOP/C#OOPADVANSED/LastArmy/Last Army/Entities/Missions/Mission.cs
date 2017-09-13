
public abstract class Mission : IMission
{
    public Mission(string name, double score, double endurance, double levelDecrement)
    {
        this.Name = name;
        this.ScoreToComplete = score;
        this.EnduranceRequired = endurance;
        this.WearLevelDecrement = levelDecrement;
    }
    public string Name { get; protected set; }
    public double EnduranceRequired { get; protected set; }
    public double ScoreToComplete { get; protected set; }
    public double WearLevelDecrement { get; protected set; }
}

