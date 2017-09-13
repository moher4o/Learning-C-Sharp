using LambdaCore_Skeleton.Enums;

namespace LambdaCore_Skeleton.Interfaces
{
   public interface IFragment
    {
        string Name { get; }
        FragmentType Type { get; }
        long PressureAffection { get; }
    }
}
