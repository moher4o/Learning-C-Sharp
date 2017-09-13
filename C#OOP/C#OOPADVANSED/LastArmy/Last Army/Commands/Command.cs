
public abstract class Command : ICommand
{
    [Inject]
    protected IArmy army;

    [Inject]
    protected IWareHouse werehouse;

    [Inject]
    protected IMissionController missionController;

    private string[] parameters;

    protected Command(string[] parameters)
    {
        this.Parameters = parameters;
    }
    public string[] Parameters {
        get { return this.parameters; }
        protected set { this.parameters = value; }
    }

    public abstract string Execute();
}

