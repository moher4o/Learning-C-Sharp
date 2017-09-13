public class SoldierCommand : Command
{

    private ISoldierFactory soldierFactory;

    public SoldierCommand(string[] parameters) : base(parameters)
    {
        this.soldierFactory = new SoldierFactory();
    }

    public override string Execute()
    {
        if (this.Parameters[0] != "Regenerate")
        {
            string soldierType = this.Parameters[0];
            string name = this.Parameters[1];
            int age = int.Parse(this.Parameters[2]);
            double experience = double.Parse(this.Parameters[3]);
            double endurance = double.Parse(this.Parameters[4]);
            ISoldier currentSoldier = this.soldierFactory.CreateSoldier(soldierType, name, age, experience, endurance);
            if (this.werehouse.EquipSoldier(currentSoldier))
            {
                this.army.AddSoldier(currentSoldier);
                return string.Empty;
            }
            return string.Format(OutputMessages.SoldierNotEquiped,soldierType,name);
        }
        else
        {
            string soldierType = this.Parameters[1];
            this.army.RegenerateTeam(soldierType);
            return string.Empty;
        }
    }

}

