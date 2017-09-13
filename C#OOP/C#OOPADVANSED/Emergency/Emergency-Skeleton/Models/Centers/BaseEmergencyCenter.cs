using System.Collections.Generic;
using Emergency_Skeleton.Interfaces;

namespace Emergency_Skeleton.Models
{
    public abstract class BaseEmergencyCenter : IBaseEmergencyCenter
    {
        private string name;

        private int amountOfMaximumEmergencies;

        private List<BaseEmergency> amountOfEmergenciesProcessed;

        protected BaseEmergencyCenter(string name, int amountOfMaximumEmergencies)
        {
            this.Name = name;
            this.AmountOfMaximumEmergencies = amountOfMaximumEmergencies;
            this.amountOfEmergenciesProcessed = new List<BaseEmergency>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            protected set
            {
                this.name = value;
            }
        }

        public int AmountOfMaximumEmergencies
        {
            get
            {
                return this.amountOfMaximumEmergencies;
            }
            protected set
            {
                this.amountOfMaximumEmergencies = value;
            }
        }

        public IReadOnlyList<BaseEmergency> AmounthOfEmergenciesProcessed
        {
            get { return this.amountOfEmergenciesProcessed.AsReadOnly(); }
        }

        public void AddEmergencies(BaseEmergency emergency)
        {
            this.amountOfEmergenciesProcessed.Add(emergency);
        }

        public bool isForRetirement()
        {
            if (this.AmounthOfEmergenciesProcessed.Count == this.AmountOfMaximumEmergencies)
            {
                return true;
            }
            return false;
        }
    }
}
