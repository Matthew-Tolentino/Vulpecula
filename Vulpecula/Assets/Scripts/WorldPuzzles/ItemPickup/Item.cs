using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    private bool canPickup = false;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.KeyDown("PickUp") && canPickup)
        {
            Destroy(transform.parent.gameObject);
            // Update inventory
            ItemManager.instance.UpdateItem(itemName, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canPickup = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canPickup = false;
    }
}
