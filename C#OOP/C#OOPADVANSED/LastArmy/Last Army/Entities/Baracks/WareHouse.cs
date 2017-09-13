using System.Collections.Generic;

public class WareHouse : IWareHouse
{

    private AmmunitionFactory weaponFactory;

    public WareHouse()
    {
        this.WeaponAvailable = new Dictionary<string, int>();
        this.weaponFactory = new AmmunitionFactory();
        foreach (var weapon in OutputMessages.WeaponName)
        {
            this.WeaponAvailable[weapon] = 0;
        }
    }
    public IDictionary<string, int> WeaponAvailable { get; private set; }

    public void AddWeapon(string weaponName, int count)
    {
        this.WeaponAvailable[weaponName] += count;
    }

    public void AddAmmunition(string weaponName, int count)
    {
        this.WeaponAvailable[weaponName] += count;
    }

    public bool EquipSoldier(ISoldier soldier)
    {
        bool isEquiped = true;
        IEnumerable<string> soldierWeapons = new List<string>(soldier.Weapons.Keys);
        foreach (var weaponName in soldierWeapons)
        {
            if (this.WeaponAvailable[weaponName] > 0)
            {
                soldier.Weapons[weaponName] = this.weaponFactory.CreateAmmunition(weaponName);
                this.WeaponAvailable[weaponName] -= 1;
            }
            else
            {
                isEquiped = false;
            }
        }
        return isEquiped;
    }

    public void EquipArmy(IArmy army)
    {
        foreach (ISoldier soldier in army.Soldiers)
        {
            foreach (var weapon in new List<string>(soldier.Weapons.Keys))
            {
                if (soldier.Weapons[weapon]==null && this.WeaponAvailable[weapon] > 0)
                {
                        soldier.Weapons[weapon] = weaponFactory.CreateAmmunition(weapon);
                        this.WeaponAvailable[weapon] -= 1;
                  
                }

            }
        }
    }
}

