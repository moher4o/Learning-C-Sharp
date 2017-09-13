namespace KingsGambit.Core.Commands
{
   public abstract class Command : IExecutable
    {
        //public Command(string unit)
        //{
        //    this.Unit = unit;
        //}
        //public string Unit { get; set; }
        public abstract void Execute();
    }
}
