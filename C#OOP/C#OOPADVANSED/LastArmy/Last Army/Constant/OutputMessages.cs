
using System.Collections.Generic;

public class OutputMessages
{
    public const string SoldierToString = "{0} - {1}";
    public const string MissionDeclined = "Mission declined - {0}";
    public const string MissionSuccessful = "Mission completed - {0}";
    public const string MissionOnHold = "Mission on hold - {0}";
    public const string ProgramEnd = "Enough! Pull back!";
    public const string SoldierNotEquiped = "There is no weapon for {0} {1}!";
    public const string MissionSuccessfullyCompleted = "Successful missions - {0}";
    public const string MissionFailedOnExit = "Failed missions - {0}";
    public static List<string> WeaponName = new List<string>()
    {
        "Gun",
        "AutomaticMachine",
        "MachineGun",
        "RPG",
        "Helmet",
        "Knife",
        "NightVision"
    };

}

