using KingsGambit.Entities;

namespace KingsGambit.Interfaces
{
    public interface IObserver
    {
        string Name { get; }
        int NumberOfWounds { get;}
        event WoundChangeEventHandler WoundChange;
        void Update();
        void HitObserver();
    }
}
