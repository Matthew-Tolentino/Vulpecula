using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMatVisibility : MonoBehaviour
{		
	public GameObject player;
	private float val;
	private float temp;
	
	private Renderer ren;
	private Material mat;

    void Start()
    {		
    	// Get Health
        val = GameObject.Find("PlayerHealthFG").GetComponent<Image>().fillAmount + 0.2f;

        // Get Material
        ren = player.GetComponent<Renderer>();
    	mat = ren.sharedMaterial;

    	mat.SetFloat("Vector1_Visibility", val);
    }

    // Update is called once per frame
    void Update()
    {
        temp = GameObject.Find("PlayerHealthFG").GetComponent<Image>().fillAmount + 0.2f;
        if (val > temp) {
        	val -= Time.deltaTime * 0.25f;
        	if (val < temp) val = temp;
        	mat.SetFloat("Vector1_Visibility", val);
        }
    }
}
