using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausedMenu : MonoBehaviour
{
    public GameObject spriteUI;
    
    private List<string> spiritsInLevel;
    private GameObject spiritUIHolder;

    void Start()
    {
        // Holds names of spirits
        // spiritsInLevel = new List<string>();
        spiritUIHolder = GameObject.Find("SpiritUIHolder");

        // SetUpDisplaySpiritsOnUI();
    }

    public void ResetDisplaySpiritsOnUI() {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in spiritUIHolder.transform) {
            children.Add(child);
        }

        foreach (Transform child in children) {
            Destroy(child.gameObject);
        }

        Invoke(nameof(SetUpDisplaySpiritsOnUI), .1f);
        // SetUpDisplaySpiritsOnUI();
    }

    public void SetUpDisplaySpiritsOnUI() {

        // Remove all children of SpiritUIHolder

        for (int i = 0; i < spiritsInLevel.Count; i++) {
            Instantiate(spriteUI, spiritUIHolder.transform);
        }

        int index = 0;
        foreach (Transform child in spiritUIHolder.transform)
        {
            child.name = spiritsInLevel[index++] + "UI";
            // Assign Image to spirit
            child.GetComponent<Image>().sprite = GameManager.instance.findSpiritImage(child.name);
        }

        UpdateSpiritsUI();
    }

    // Checks to see what spirits have been found and updates UI
    public void displaySpiritsOnUI()
    {
        spiritsInLevel = new List<string>();
        GetSpiritsInLevel();
        ResetDisplaySpiritsOnUI();
    }

    private void UpdateSpiritsUI() {
        List <GameObject> foundSpirits = FindObjectOfType<SpiritHandler>().SpiritList;
        
        foreach (string spirit in spiritsInLevel)
        {
            GameObject tempSpiritUI = GameObject.Find(spirit + "UI");
            if (tempSpiritUI != null) {
                Image img = tempSpiritUI.GetComponent<Image>();
                Color tmpColor = img.color;
                for (int i = 0; i < foundSpirits.Count; i++)
                {
                    if (foundSpirits[i] != null)
                    {
                        if (foundSpirits[i].name == spirit)
                        {
                            tmpColor.a = 1f;
                            break;
                        }
                    }
                    tmpColor.a = 0.3f;
                }
                img.color = tmpColor;
            }
        }
    }

    private void GetSpiritsInLevel()
    {
        GameObject spiritHolder = GameObject.Find("Spirits");
        foreach (Transform child in spiritHolder.transform)
        {
            spiritsInLevel.Add(child.name);
        }
    }

    // public Sprite AssignImage(string name) {
    //     switch (name) {
    //         case "Floaty_SpiritUI":
    //             return floatyImg;
    //         case "Rock_SpiritUI":
    //             return rockImg;
    //         case "Lamp_SpiritUI":
    //             return lampImg;
    //         default:
    //             Debug.LogError("No sprite with name: " + name);
    //             return null;
    //     }
    // }

    public void ReturnToMainMenu()
    {
        transform.parent.gameObject.SetActive(false);

        SpiritHandler sh = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<SpiritHandler>();
        while(sh.SpiritList.Count > 0){
            sh.loseSpirit();
        }

        SceneManager.LoadScene("MainMenu");
    }
}
