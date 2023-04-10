using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class WeaponSO : Item
{
    public WeaponType WeaponType;
    public int Damage;
    public AmmoSO AmmoInUse;

    public void Shoot()
    {
        if(!GameContext.Inventory.Contains(AmmoInUse))  AmmoInUse = null;

        if (AmmoInUse == null)
        {
            foreach(var item in GameContext.Inventory)
            {
                if (item == null) continue;
                if (item.GetType().ToString() != "AmmoSO") continue;
                AmmoSO ammo = (AmmoSO)item;

                if (ammo.WeaponType == WeaponType)
                {
                    AmmoInUse = ammo;
                    break;
                }
            }
            if (AmmoInUse == null)
            {
                Debug.Log("No ammo");
                return;
            }
        }
        AmmoInUse.Stacks--;
        if (AmmoInUse.Stacks <= 0)
        {
            GameContext.Inventory[GameContext.Inventory.IndexOf(AmmoInUse)] = null;
            AmmoInUse = null;
        }
        Debug.Log("Shooted from " + Name + ", dealted " + Damage + " damage");
    }
}

public enum WeaponType
{
    Pistol,
    Autorifle
}
