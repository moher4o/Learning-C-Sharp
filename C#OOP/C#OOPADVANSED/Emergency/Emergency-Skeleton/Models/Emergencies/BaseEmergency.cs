using Emergency_Skeleton.Interfaces;

namespace Emergency_Skeleton.Models
{
    using Emergency_Skeleton.Enums;

    public abstract class BaseEmergency : IBaseEmergency
    {
        private string description;

        private EmergencyLevel emergencyLevel;

        private IRegistrationTime registrationTime;

        private int trouble;

        protected BaseEmergency(string description, EmergencyLevel emergencyLevel, IRegistrationTime registrationTime, int trouble)
        {
            this.Description = description;
            this.EmergencyLevel = emergencyLevel;
            this.RegistrationTime = registrationTime;
            this.Trouble = trouble;
        }

        public int Trouble
        {
            get { return this.trouble; }
            protected set { this.trouble = value; }
        }


        public string Description
        {
            get
            {
                return this.description;
            }
            protected set
            {
                this.description = value;
            }
        }

        public EmergencyLevel EmergencyLevel
        {
            get
            {
                return this.emergencyLevel;
            }
            protected set
            {
                this.emergencyLevel = value;
            }
        }

        public IRegistrationTime RegistrationTime
        {
            get
            {
                return this.registrationTime;
            }
            protected set
            {
                this.registrationTime = value;
            }
        }
    }
}
