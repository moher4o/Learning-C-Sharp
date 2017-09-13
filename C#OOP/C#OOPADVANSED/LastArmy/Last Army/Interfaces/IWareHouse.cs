using System.Collections.Generic;

public interface IWareHouse
{
    IDictionary<string,int> WeaponAvailable { get; }

    bool EquipSoldier(ISoldier soldier);

    void AddWeapon(string weaponName, int count);

    void AddAmmunition(string weaponName, int count);

    void EquipArmy(IArmy army);
}

