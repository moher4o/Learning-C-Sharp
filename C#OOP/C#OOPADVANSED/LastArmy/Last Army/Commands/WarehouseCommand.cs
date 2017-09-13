
public class WarehouseCommand : Command
{
    public WarehouseCommand(string[] parameters) : base(parameters)
    {
    }

    public override string Execute()
    {
        string weaponName = this.Parameters[0];
        int count = int.Parse(this.Parameters[1]);
        this.werehouse.AddWeapon(weaponName,count);
        return string.Empty;
    }
}

