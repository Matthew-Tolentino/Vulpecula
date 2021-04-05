using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items")]
public class Items : ScriptableObject
{
    public bool gateKey;

    public bool checkItem(string item)
    {
        switch (item)
        {
            case "GateKey":
                return gateKey;

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

            default:
                Debug.LogError("No Item with name: " + item);
                return;
        }
    }

    // Return all names of item variables
    public string[] getItems() {
        return new string[] {"GateKey"};
    }
}
