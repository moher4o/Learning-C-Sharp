
using System;
using System.Linq;
using System.Reflection;

public class SoldierFactory : ISoldierFactory
{

    public ISoldier CreateSoldier(string soldierTypeName, string name, int age, double experience, double endurance)
    {
        Type soldierType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(t => t.Name == soldierTypeName);

        object[] parameters = { name, age, experience, endurance };

        return (ISoldier)Activator.CreateInstance(soldierType, parameters);

    }
}
