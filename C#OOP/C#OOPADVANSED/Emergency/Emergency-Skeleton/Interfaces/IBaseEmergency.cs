using Emergency_Skeleton.Enums;

namespace Emergency_Skeleton.Interfaces
{
   public interface IBaseEmergency
    {
        int Trouble { get; }
        string Description { get; }
        EmergencyLevel EmergencyLevel { get; }
        IRegistrationTime RegistrationTime { get; }
    }
}
