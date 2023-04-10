using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
public class ArmorSO : Item
{
    public ArmorType ArmorType;
    public int Resist;
}

public enum ArmorType 
{ 
    Head,
    Body
}

