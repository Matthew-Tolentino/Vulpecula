using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemUI;
    public Items items;

    string[] itemNames;

    // Start is called before the first frame update
    void Start()
    {
        itemNames = ItemManager.instance.GetItems();
        foreach (string item in itemNames) {
            GameObject newItem = Instantiate(itemUI, transform);
            newItem.name = item + "UI";
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string item in itemNames) {
            if (ItemManager.instance.CheckItem(item)) {
                Image itemImg = GameObject.Find(item + "UI").GetComponent<Image>();
                Color tmpColor = itemImg.color;
                tmpColor.a = 1f;
                itemImg.color = tmpColor;
            }
        }
    }
}
