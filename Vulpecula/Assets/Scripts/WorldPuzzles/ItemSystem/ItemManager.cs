using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Makes GameManager into a singleton
    public static ItemManager instance;

    public Items items;

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
    }

    public bool CheckItem(string item)
    {
        return items.checkItem(item);
    }

    public void UpdateItem(string item, bool pickUp)
    {
        items.updateItem(item, pickUp);
    }
}
