using LambdaCore_Skeleton.Interfaces;
using LambdaCore_Skeleton.Enums;

namespace LambdaCore_Skeleton.Models.Fragments
{
 
    public class Fragment : IFragment
    {
 
        public Fragment(FragmentType type, string name, int pressureAffection)
        {
            this.Type = type;
            this.Name = name;
            if (type == FragmentType.Cooling)
            {
                this.PressureAffection = pressureAffection * -3;
            }
            else if (type == FragmentType.Nuclear)
            {
                this.PressureAffection = pressureAffection * 2;
            }
            
        }

        public string Name { get; private set; }

        public FragmentType Type { get; private set; }

        public long PressureAffection { get; private set; }
    }
}
