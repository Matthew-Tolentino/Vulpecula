using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemUI;
    public Items items;

    string[] itemNames;

    public Sprite gateKey;
    public Sprite book;

    // Start is called before the first frame update
    void Start()
    {
        SetItems(0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemImages();
    }

    private void UpdateItemImages()
    {
        foreach (string item in itemNames)
        {
            if (ItemManager.instance.CheckItem(item))
            {
                Image itemImg = GameObject.Find(item + "UI").GetComponent<Image>();
                Color tmpColor = itemImg.color;
                tmpColor.a = 1f;
                itemImg.color = tmpColor;
            }
        }
    }

    public void ClearInventory() {
        itemNames = new string[0];

        // clear children
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
            children.Add(child);
        
        foreach (Transform child in children)
            Destroy(child.gameObject);
    }

    public void SetItems(int level) {
        itemNames = ItemManager.instance.GetItems(level);
        foreach (string item in itemNames) {
            GameObject newItem = Instantiate(itemUI, transform);
            if (item == "GateKey") newItem.GetComponent<Image>().sprite = gateKey;
            if (item == "WiseBook") newItem.GetComponent<Image>().sprite = book;
            newItem.name = item + "UI";
            
            TextMeshProUGUI text = newItem.GetComponentInChildren<TextMeshProUGUI>(true);
            text.SetText(item);
        }
    }
}
