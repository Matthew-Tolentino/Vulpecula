using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Makes GameManager into a singleton
    public static ItemManager instance;

    public Items items;
    public InventoryUI inventoryUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        resetItemBools();
    }

    public bool CheckItem(string item)
    {
        return items.checkItem(item);
    }

    public void UpdateItem(string item, bool pickUp)
    {
        items.updateItem(item, pickUp);
    }

    // level = what level you are grabbing the items from
    public string[] GetItems(int level) {
        return items.getItems(level);
    }

    public void ResetInventory() {
        inventoryUI.ClearInventory();
    }

    public void SetInventory(int level) {
        inventoryUI.SetItems(level);
    }

    public void resetItemBools() {
        UpdateItem("GateKey", false);
        UpdateItem("WiseBook", false);
    }
}
