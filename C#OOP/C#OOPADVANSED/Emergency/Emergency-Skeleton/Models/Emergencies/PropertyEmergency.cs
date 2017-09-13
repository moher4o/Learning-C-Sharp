using Emergency_Skeleton.Enums;
using Emergency_Skeleton.Interfaces;

namespace Emergency_Skeleton.Models.Emergencies
{
   public class PropertyEmergency : BaseEmergency
    {
        public PropertyEmergency(string description, EmergencyLevel emergencyLevel, IRegistrationTime registrationTime, int propertyDamage) : base(description, emergencyLevel, registrationTime, propertyDamage)
        {
            
        }

    }
}
