using System;

namespace Emergency_Skeleton.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
   public class InjectAttribute : Attribute
    {
    }
}
