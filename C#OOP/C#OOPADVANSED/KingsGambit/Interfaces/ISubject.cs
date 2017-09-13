using KingsGambit.Interfaces;

namespace KingsGambit
{
   public interface ISubject
    {
        void Register(IObserver observer);
        void Kill(string observer);
        void Attack();
    }
}
