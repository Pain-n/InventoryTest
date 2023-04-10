
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig")]
public class GameConfigSO : ScriptableObject
{
    [Header("Items library")]
    public List<AmmoSO> Ammo;
    public List<WeaponSO> Weapons;
    public List<ArmorSO> HeadArmor;
    public List<ArmorSO> BodyArmor;

    [Header("Inventory settings")]
    public int InventoryMaxCapacity;
    public int InventoryBaseCapacity;
    public int PriceToUnlockSlot;

}
