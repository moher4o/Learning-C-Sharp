public class SolarProvider : Provider
   {
       private const double DurabilityModifier = 500d;
        public SolarProvider(int id,  double energyOutput) : base(id, DurabilityModifier, energyOutput)
        {
        }
    }

