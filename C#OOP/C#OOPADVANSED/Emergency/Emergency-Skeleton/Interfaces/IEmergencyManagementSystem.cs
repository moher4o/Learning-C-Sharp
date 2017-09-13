namespace Emergency_Skeleton.Interfaces
{
   public interface IEmergencyManagementSystem
   {
       string RegisterPropertyEmergency(string[] data);
       string RegisterHealthEmergency(string[] data);
       string RegisterOrderEmergency(string[] data);
       string RegisterFireServiceCenter(string[] data);
       string RegisterMedicalServiceCenter(string[] data);
       string RegisterPoliceServiceCenter(string[] data);
       string ProcessEmergencies(string[] data);
       string EmergencyReport();
   }
}
