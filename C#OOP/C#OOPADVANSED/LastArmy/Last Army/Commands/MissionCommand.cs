
public class MissionCommand : Command
{
    private IMissionFactory missionFactory;
    public MissionCommand(string[] parameters) : base(parameters)
    {
        this.missionFactory = new MissionFactory();
    }

    public override string Execute()
    {
        string missionName = this.Parameters[0];
        double missionScore = double.Parse(this.Parameters[1]);
        IMission mission = this.missionFactory.CreateMission(missionName, missionScore);
        return this.missionController.PerformMission(mission);
    }
}

