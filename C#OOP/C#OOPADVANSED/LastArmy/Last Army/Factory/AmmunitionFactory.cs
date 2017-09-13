
using System;
using System.Linq;
using System.Reflection;

public class AmmunitionFactory
{
    public IAmmunition CreateAmmunition(string name)
    {
        
        Type amunitionType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == name);
        object[] data = {name};
        return (IAmmunition) Activator.CreateInstance(amunitionType, data);
    }

}
