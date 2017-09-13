
public class LastArmyMain
{
    static void Main()
    {
        
        IArmy army = new Army();
        IWareHouse wareHouse = new WareHouse();
        IMissionController missioncontroler = new MissionController(army, wareHouse);
        IReader reader = new ConsoleReader();
        IWriter writer = new ConsoleWriter();
        IEngine engine = new Engine(missioncontroler, wareHouse, army, reader, writer);

        engine.Run();
    }
}
