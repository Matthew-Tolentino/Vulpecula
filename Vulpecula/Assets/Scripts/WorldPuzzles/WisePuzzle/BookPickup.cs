using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPickup : MonoBehaviour
{
    void LateUpdate() {
        if (ItemManager.instance.CheckItem("WiseBook")) {
            Destroy(gameObject);
        }
    }
}
