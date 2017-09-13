using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emergency_Skeleton.Collection;
using Emergency_Skeleton.Enums;
using Emergency_Skeleton.Interfaces;
using Emergency_Skeleton.Models;
using Emergency_Skeleton.Models.Centers;
using Emergency_Skeleton.Models.Emergencies;
using Emergency_Skeleton.Utils;

namespace Emergency_Skeleton.Core
{
    public class EmergencyManagementSystem : IEmergencyManagementSystem
    {
        private readonly EmergencyRegister emergencyRegister;
        private readonly Queue<BaseEmergencyCenter> centerRegister;
        private List<IBaseEmergency> emergencyProcessedList;

        public EmergencyManagementSystem()
        {
            this.emergencyRegister = new EmergencyRegister();
            this.centerRegister = new Queue<BaseEmergencyCenter>();
            this.emergencyProcessedList = new List<IBaseEmergency>();
        }

        public string RegisterPropertyEmergency(string[] data)
        {
            string description = data[0];
            if (!Enum.TryParse(data[1], true, out EmergencyLevel level))
            {
                Console.WriteLine("Wrong LEVEL!");
            }
            IRegistrationTime registrationTime = new RegistrationTime(data[2]);
            int propertyDamage = int.Parse(data[3]);
            this.emergencyRegister.EnqueueEmergency(new PropertyEmergency(description, level, registrationTime, propertyDamage));

            return $"Registered Public Property Emergency of level {level} at {registrationTime}.";
        }

        public string RegisterHealthEmergency(string[] data)
        {
            string description = data[0];
            if (!Enum.TryParse(data[1], true, out EmergencyLevel level))
            {
                Console.WriteLine("Wrong LEVEL!");
            }
            IRegistrationTime registrationTime = new RegistrationTime(data[2]);
            int casualties = int.Parse(data[3]);
            this.emergencyRegister.EnqueueEmergency(new HealthEmergency(description, level, registrationTime, casualties));

            return $"Registered Public Health Emergency of level {level} at {registrationTime}.";
        }

        public string RegisterOrderEmergency(string[] data)
        {
            string description = data[0];
            if (!Enum.TryParse(data[1], true, out EmergencyLevel level))
            {
                Console.WriteLine("Wrong LEVEL!");
            }
            IRegistrationTime registrationTime = new RegistrationTime(data[2]);
            if (data[3] == "Non-Special")
            {
                data[3].Replace("-", "");
            }
            if (!Enum.TryParse(data[3], true, out Status status))
            {
                Console.WriteLine("Wrong STATUS!");
            }
            this.emergencyRegister.EnqueueEmergency(new OrderEmergency(description, level, registrationTime, status));

            return $"Registered Public Order Emergency of level {level} at {registrationTime}.";
        }

        public string RegisterFireServiceCenter(string[] data)
        {
            string name = data[0];
            int amountOfEmergencies = int.Parse(data[1]);

            this.centerRegister.Enqueue(new FiremanCenter(name, amountOfEmergencies));

            return $"Registered Fire Service Emergency Center - {name}.";
        }

        public string RegisterMedicalServiceCenter(string[] data)
        {
            string name = data[0];
            int amountOfEmergencies = int.Parse(data[1]);

            this.centerRegister.Enqueue(new MedicalCenter(name, amountOfEmergencies));

            return $"Registered Medical Service Emergency Center - {name}.";

        }

        public string RegisterPoliceServiceCenter(string[] data)
        {
            string name = data[0];
            int amountOfEmergencies = int.Parse(data[1]);

            this.centerRegister.Enqueue(new PoliceCenter(name, amountOfEmergencies));

            return $"Registered Police Service Emergency Center - {name}.";
        }

        public string ProcessEmergencies(string[] data)
        {
            Type typeOfEmergencyCenter = null;
            Type typeOfEmergency = null;
            BaseEmergencyCenter currentcenter = null;
            switch (data[0])
            {
                case "Property":
                    typeOfEmergencyCenter = typeof(FiremanCenter);
                    typeOfEmergency = typeof(PropertyEmergency);
                    break;
                case "Health":
                    typeOfEmergencyCenter = typeof(MedicalCenter);
                    typeOfEmergency = typeof(HealthEmergency);
                    break;
                case "Order":
                    typeOfEmergencyCenter = typeof(PoliceCenter);
                    typeOfEmergency = typeof(OrderEmergency);
                    break;
            }

            bool isCenterAvailable = true;
            for (int i = 0; i < this.emergencyRegister.Count(); i++)
            {
                BaseEmergency currentEmergency = this.emergencyRegister.DequeueEmergency();
                if (currentEmergency.GetType() == typeOfEmergency)
                {
                    isCenterAvailable = false;
                    for (int j = 0; j < this.centerRegister.Count; j++)
                    {
                        if ((currentcenter = this.centerRegister.Dequeue()).GetType() != typeOfEmergencyCenter)
                        {
                            this.centerRegister.Enqueue(currentcenter);
                        }
                        else
                        {
                            currentcenter.AddEmergencies(currentEmergency);
                            if (!currentcenter.isForRetirement())
                            {
                                this.centerRegister.Enqueue(currentcenter);
                            }
                            this.emergencyProcessedList.Add(currentEmergency);
                            isCenterAvailable = true;
                            break;
                        }
                    }
                    if (!isCenterAvailable)
                    {
                        this.emergencyRegister.EnqueueEmergency(currentEmergency);
                        break;
                    }
                }
                else
                {
                    this.emergencyRegister.EnqueueEmergency(currentEmergency);
                }

            }
            if (isCenterAvailable)
            {
                return $"Successfully responded to all {data[0]} emergencies.";
            }
            else
            {
                int amountOfEmergenciesLeft = this.emergencyRegister.GetEmergencyOfType(typeOfEmergency);
                  return $"{data[0]} Emergencies left to process: {amountOfEmergenciesLeft}.";
            }
            
        }

        public string EmergencyReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("PRRM Services Live Statistics");
            sb.AppendLine($"Fire Service Centers: {this.centerRegister.Count(c => c.GetType()==typeof(FiremanCenter))}");
            sb.AppendLine($"Medical Service Centers: {this.centerRegister.Count(c => c.GetType() == typeof(MedicalCenter))}");
            sb.AppendLine($"Police Service Centers: {this.centerRegister.Count(c => c.GetType() == typeof(PoliceCenter))}");
            sb.AppendLine($"Total Processed Emergencies: {this.emergencyProcessedList.Count}");
            sb.AppendLine($"Currently Registered Emergencies: {this.emergencyRegister.Count()}");
            sb.AppendLine($"Total Property Damage Fixed: {this.emergencyProcessedList.Where(c => c.GetType() == typeof(PropertyEmergency)).Sum(c => c.Trouble)}");
            sb.AppendLine($"Total Health Casualties Saved: {this.emergencyProcessedList.Where(c => c.GetType() == typeof(HealthEmergency)).Sum(c => c.Trouble)}");
            sb.AppendLine($"Total Special Cases Processed: {this.emergencyProcessedList.Where(c => c.GetType() == typeof(OrderEmergency)).Sum(c => c.Trouble)}");
            return sb.ToString();
        }
    }
}
