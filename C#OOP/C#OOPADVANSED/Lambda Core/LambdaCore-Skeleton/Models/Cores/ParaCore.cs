namespace LambdaCore_Skeleton.Models.Cores
{
    
   public class ParaCore : BaseCore
   {
       private const double DurabilityDivider = 3d;

        public ParaCore(string name, double durability) : base(name, durability/DurabilityDivider)
        {

        }
    }
}
