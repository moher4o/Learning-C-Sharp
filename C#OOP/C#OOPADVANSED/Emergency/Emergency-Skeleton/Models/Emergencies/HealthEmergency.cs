using Emergency_Skeleton.Enums;
using Emergency_Skeleton.Interfaces;

namespace Emergency_Skeleton.Models.Emergencies
{
   public class HealthEmergency : BaseEmergency
    {

        public HealthEmergency(string description, EmergencyLevel emergencyLevel, IRegistrationTime registrationTime, int casualties) : base(description, emergencyLevel, registrationTime, casualties)
        {
            
        }
    }
}
