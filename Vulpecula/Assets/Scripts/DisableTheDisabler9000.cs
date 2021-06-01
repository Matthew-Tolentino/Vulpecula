using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTheDisabler9000 : MonoBehaviour
{
    public GameObject target;

    public void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            Destroy(target);
        }
    }
}
