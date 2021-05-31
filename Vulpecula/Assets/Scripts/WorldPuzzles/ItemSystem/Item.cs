using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    private bool canPickup = false;

    [SerializeField]
    private List<FMODUnity.StudioEventEmitter> pickupSounds;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.KeyDown("PickUp") && canPickup)
        {
            PickupItem();
        }
    }

    private void PickupItem()
    {
        Destroy(transform.parent.gameObject);
        // Update inventory
        ItemManager.instance.UpdateItem(itemName, true);

        foreach (var emitter in pickupSounds)
        {
            emitter.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PickupItem();
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //         canPickup = false;
    // }
}
