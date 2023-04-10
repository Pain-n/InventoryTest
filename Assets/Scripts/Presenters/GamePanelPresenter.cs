using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelPresenter :MonoBehaviour
{
    public Button AddAmmoButton;
    public Button ShootButton;
    public Button DeleteItemButton;
    public Button AddItemButton;

    public Transform InventoryContainer;
    List<InventoryItemPresenter> ItemPresentersList;
    public InventoryItemPresenter ItemPrefab;

    private void Start()
    {
        ItemPresentersList = new List<InventoryItemPresenter>();
        for (int i = 0; i< GameContext.GameConfig.InventoryMaxCapacity; i++)
        {
            InventoryItemPresenter item = Instantiate(ItemPrefab, InventoryContainer);
            ItemPresentersList.Add(item);
            if (i >= GameContext.Inventory.Capacity) item.GetComponent<Image>().color = Color.black;
        }
        UpdateInventoryUI();

        AddAmmoButton.onClick.AddListener(() =>
        {
            for(int i = 0; i< GameContext.GameConfig.Ammo.Count; i++)
            {
                for(int j = 0; j < GameContext.Inventory.Capacity; j++)
                {
                    if (GameContext.Inventory[j] == null)
                    {
                        GameContext.Inventory[j] = (AmmoSO)GameContext.GameConfig.Ammo[i].Clone();
                        break;
                    }
                }
            }
            UpdateInventoryUI();
        });

        AddItemButton.onClick.AddListener(() =>
        {
            for (int j = 0; j < GameContext.Inventory.Capacity; j++)
            {
                if (GameContext.Inventory[j] == null)
                {
                    GameContext.Inventory[j] = (ArmorSO)GameContext.GameConfig.HeadArmor[Random.Range(0, GameContext.GameConfig.HeadArmor.Count)].Clone();
                    break;
                }
            }
            for (int j = 0; j < GameContext.Inventory.Capacity; j++)
            {
                if (GameContext.Inventory[j] == null)
                {
                    GameContext.Inventory[j] = (ArmorSO)GameContext.GameConfig.BodyArmor[Random.Range(0, GameContext.GameConfig.BodyArmor.Count)].Clone();
                    break;
                }
            }
            for (int j = 0; j < GameContext.Inventory.Capacity; j++)
            {
                if (GameContext.Inventory[j] == null)
                {
                    GameContext.Inventory[j] = (WeaponSO)GameContext.GameConfig.Weapons[Random.Range(0, GameContext.GameConfig.Weapons.Count)].Clone();
                    break;
                }
            }
            UpdateInventoryUI();
        });

        DeleteItemButton.onClick.AddListener(() =>
        {
            bool isDeleted = false;
            for(int i = 0;i < GameContext.Inventory.Capacity; i++)
            {
                if(Random.Range(0,10) > 5 && GameContext.Inventory[i] != null)
                {
                    GameContext.Inventory[i] = null;
                    isDeleted = true;
                    break;
                }
            }

            if(GameContext.Inventory.Count > 0 && isDeleted == false)
            {
                for (int i = 0; i < GameContext.Inventory.Capacity; i++)
                {
                    if (GameContext.Inventory[i] != null)
                    {
                        GameContext.Inventory[i] = null;
                        isDeleted = true;
                        break;
                    }
                }
            }

            if (isDeleted == false) Debug.Log("Nothing to delete");
            UpdateInventoryUI();
        });

        ShootButton.onClick.AddListener(() =>
        {
            bool isShooted = false;
            foreach (var item in GameContext.Inventory)
            {
                if (item == null) continue;
                if (item.GetType().ToString() != "WeaponSO") continue;

                if (Random.Range(0, 10) > 5)
                {
                    WeaponSO weapon = (WeaponSO)item;
                    weapon.Shoot();
                    isShooted = true;
                    break;
                }
            }

            if(isShooted == false)
            {
                foreach (var item in GameContext.Inventory)
                {
                    if (item == null) continue;
                    if (item.GetType().ToString() != "WeaponSO") continue;

                    WeaponSO weapon = (WeaponSO)item;
                    weapon.Shoot();
                    isShooted = true;
                    break;
                }
            }

            UpdateInventoryUI();
        });
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i< ItemPresentersList.Count; i++)
        {
            if (i >= GameContext.Inventory.Capacity) break;
            if (GameContext.Inventory[i] != null)
            {
                ItemPresentersList[i].Item = GameContext.Inventory[i];
                ItemPresentersList[i].Image.sprite = GameContext.Inventory[i].Sprite;
                ItemPresentersList[i].Image.enabled = true;
                if(GameContext.Inventory[i].IsStackable == true)
                {
                    ItemPresentersList[i].StacksText.text = GameContext.Inventory[i].Stacks.ToString();
                }
            }
            else
            {
                ItemPresentersList[i].Item = null;
                ItemPresentersList[i].Image.enabled = false;
                ItemPresentersList[i].StacksText.text = "";
            }
        }
    }
}
