using System;
using System.Collections.Generic;

[Serializable]
public class GameDataModel
{
    public List<InventoryItemDataModel> InventoryItems;
    public int InventoryCapacity;
}

[Serializable]
public class InventoryItemDataModel 
{
    public string FileName;
    public int Stacks;
}


