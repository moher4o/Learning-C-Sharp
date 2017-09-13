using System.Collections.Generic;

namespace LambdaCore_Skeleton.Interfaces
{
   public interface ICore
    {
        double Durability { get; }
        string Name { get; }
        IReadOnlyList<IFragment> Fragments { get; }
        bool AddFragment(IFragment fragment);
        string RemoveFragment();
        void ChangeName(string name);
    }
}
