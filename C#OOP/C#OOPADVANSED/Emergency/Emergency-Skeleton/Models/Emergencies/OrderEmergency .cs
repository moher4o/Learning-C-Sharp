using Emergency_Skeleton.Enums;
using Emergency_Skeleton.Interfaces;

namespace Emergency_Skeleton.Models.Emergencies
{
   public class OrderEmergency : BaseEmergency
    {

        public OrderEmergency(string description, EmergencyLevel emergencyLevel, IRegistrationTime registrationTime, Status status) : base(description, emergencyLevel, registrationTime, (int)status)
        {

        }

    }
}
