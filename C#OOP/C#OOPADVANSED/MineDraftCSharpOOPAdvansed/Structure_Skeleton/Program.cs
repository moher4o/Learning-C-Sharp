public class Program
{
    public static void Main(string[] args)
    {
       
        IReader reader = new ConsoleReader();
        IWriter writer = new ConsoleWriter();
        IEnergyRepository enrgyRepository = new EnergyRepository();
        HarvesterController harvesterController = new HarvesterController();
        ProviderController providerController = new ProviderController(enrgyRepository);
        ICommandInterpreter interpreter = new CommandInterpreter(harvesterController,providerController);

        IEngine engine = new Engine(interpreter,reader,writer);

        engine.Run();
    }
}