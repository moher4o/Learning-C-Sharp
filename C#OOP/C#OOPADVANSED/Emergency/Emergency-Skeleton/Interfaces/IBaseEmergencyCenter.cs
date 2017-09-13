using System.Collections.Generic;
using Emergency_Skeleton.Models;

namespace Emergency_Skeleton.Interfaces
{
   public interface IBaseEmergencyCenter
    {
        string Name { get; }
        int AmountOfMaximumEmergencies { get; }
        IReadOnlyList<BaseEmergency> AmounthOfEmergenciesProcessed { get; }
        void AddEmergencies(BaseEmergency emergency);
        bool isForRetirement();
    }
}
