
using System.Linq;
using System.Text;

public class QuitCommand : Command
{
    public QuitCommand(string[] parameters) : base(parameters)
    {
    }

    public override string Execute()
    {
        StringBuilder sb = new StringBuilder();
        this.missionController.FailMissionsOnHold();
        sb.AppendLine("Results:");
        sb.AppendLine(string.Format(OutputMessages.MissionSuccessfullyCompleted,
            this.missionController.SuccessMissionCounter));
        sb.AppendLine(string.Format(OutputMessages.MissionFailedOnExit,
            this.missionController.FailedMissionCounter));
        sb.AppendLine("Soldiers:");
        foreach (ISoldier soldier in this.army.Soldiers.OrderByDescending(s => s.OverallSkill))
        {
            sb.AppendLine(soldier.ToString());
        }
        return sb.ToString().Trim();
    }
}
