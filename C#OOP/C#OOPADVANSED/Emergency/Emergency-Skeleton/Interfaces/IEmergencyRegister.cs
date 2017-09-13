using Emergency_Skeleton.Models;

namespace Emergency_Skeleton.Interfaces
{
   public interface IEmergencyRegister
   {
       void EnqueueEmergency(BaseEmergency emergency);
       BaseEmergency DequeueEmergency();
       BaseEmergency PeekEmergency();
       bool IsEmpty();
   }
}
