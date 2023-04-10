using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public static GameConfigSO GameConfig;

    public static List<Item> Inventory;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        GameConfig = Resources.Load<GameConfigSO>("GameConfig");

        Inventory = SaveLoadSystem.LoadGame();

        if(Inventory == null)
        {
            Inventory = new List<Item>(GameConfig.InventoryBaseCapacity);
            for (int i = 0; i < Inventory.Capacity; i++)
            {
                Inventory.Add(null);
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveLoadSystem.SaveGame();
    }
}
