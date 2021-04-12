using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGlowControl : MonoBehaviour
{
	public GameObject glowTarget;
    private Renderer ren;
	private Material mat;
	public bool activate;
	public float intensity;
	public string state;

    void Start()
    {	
    	if (glowTarget != null){
	        ren = glowTarget.GetComponent<Renderer>();
	        mat = ren.sharedMaterial;
	        activate = false;
	        intensity = 0f;
	        state = "Inactive";
	        mat.SetFloat("Vector1_Intensity", intensity);
	    }
    }

    void Update()
    {
    	if (glowTarget != null && activate){
    		if (state == "Inactive") state = "ActiveUp";
    		if (state == "ActiveUp"){
    			intensity += Time.deltaTime * (intensity + 1) * 2;
    			if (intensity >= 2.5) state = "ActiveDown";
    		}
    		if (state == "ActiveDown"){
    			intensity -= Time.deltaTime * 2;
    			if (intensity <= 0){
    				state = "Inactive";
    				activate = false;
    				intensity = 0;
    			}
    		}

        	mat.SetFloat("Vector1_Intensity", intensity);
    	}
    }
}
