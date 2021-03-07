using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public string[] itemsNeeded;

    private bool canInteract;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.KeyDown("ItemInteract") && canInteract)
        {
            // Do whatever interactable needs to do
            // Generalize to all items later
            if (transform.parent.gameObject.name == "Interactable_InnerGate")
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool missingItem = false;
            foreach (string item in itemsNeeded)
            {
                if (!ItemManager.instance.CheckItem(item))
                {
                    missingItem = true;
                    // TODO: Pop up dialog about what items missing
                    Debug.Log("Missing an Item");
                    break;
                }
            }
            if (!missingItem)
                Debug.Log("Can interact");
                canInteract = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canInteract = false;
    }
}
