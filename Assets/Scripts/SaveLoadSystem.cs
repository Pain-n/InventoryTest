using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem
{
    public static void SaveGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saves/GameData.sv");
        GameDataModel data = new();

        data.InventoryItems = new List<InventoryItemDataModel>();
        foreach (var item in GameContext.Inventory)
        {
            InventoryItemDataModel saveItem = new();
            if (item != null)
            {
                saveItem.FileName = item.name;
                saveItem.Stacks = item.Stacks;
                data.InventoryItems.Add(saveItem);
            }
            else
            {
                saveItem.FileName = "null";
                saveItem.Stacks = 0;
                data.InventoryItems.Add(saveItem);
            }
        }
        data.InventoryCapacity = GameContext.Inventory.Capacity;

        bf.Serialize(file, data);
        file.Close();
    }

    public static List<Item> LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/saves/GameData.sv")) return null;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/saves/GameData.sv", FileMode.Open);
        if (file.Length == 0) return null;
        GameDataModel data = (GameDataModel)bf.Deserialize(file);
        file.Close();

        List<Item> items = Resources.LoadAll<Item>("Items").ToList();
        List<Item> loadedInventory = new List<Item>(data.InventoryCapacity);
        for (int i = 0; i < loadedInventory.Capacity; i++)
        {
            loadedInventory.Add(null);
        }

        for (int i = 0; i < data.InventoryItems.Count; i++)
        {
            if (data.InventoryItems[i].FileName != "null")
            {
                foreach(var item in items)
                {
                    if(item.name == data.InventoryItems[i].FileName)
                    {
                        Item loadedItem = (Item)item.Clone();
                        loadedItem.Stacks = data.InventoryItems[i].Stacks;
                        loadedInventory[i] = loadedItem;
                        break;
                    }
                }
            }
        }

        return loadedInventory;
    }
}
