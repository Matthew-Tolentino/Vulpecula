using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class Items : ScriptableObject
{
    public bool gateKey, wiseBook;

    public bool checkItem(string item)
    {
        switch (item)
        {
            case "GateKey":
                return gateKey;
            case "WiseBook":
                return wiseBook;

            default:
                Debug.LogError("No Item with name: " + item);
                return false;
        }
    }

    public void updateItem(string item, bool pickUp)
    {
        switch (item)
        {
            case "GateKey":
                gateKey = pickUp;
                return;
            case "WiseBook":
                wiseBook = pickUp;
                return;

            default:
                Debug.LogError("No Item with name: " + item);
                return;
        }
    }

    // Return all names of item variables
    public string[] getItems(int level) {
        if (level == 0)
            return new string[] {"GateKey"};
        if (level == 1)
            return new string[] {"WiseBook"};
            
        Debug.LogError("No level with index: " + level);
        return null;
    }
}
