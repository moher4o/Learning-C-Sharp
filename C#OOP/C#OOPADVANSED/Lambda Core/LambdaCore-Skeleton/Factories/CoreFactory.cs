using LambdaCore_Skeleton.Interfaces;
using LambdaCore_Skeleton.Models.Cores;

namespace LambdaCore_Skeleton.Factories
{
   public class CoreFactory
   {

       public ICore GetCore(string type, string name, double durability)
       {
           if (type == "System")
           {
               return new SystemCore(name,durability);
           }
           else if (type == "Para")
           {
               return new ParaCore(name, durability);
           }

           return null;
       }
   }
}
