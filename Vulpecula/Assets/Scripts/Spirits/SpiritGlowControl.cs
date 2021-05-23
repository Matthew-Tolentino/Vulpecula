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

	private int runtime;

	[SerializeField] private bool constant;
	public bool stop;
	public bool isHint;


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
	    stop = false;
	    runtime = 5;
    }

    void Update()
    {
    	if (!stop && constant)
  		{
  			if (!activate) activate = true;
  		}

    	if (glowTarget != null && activate){
    		if (state == "Inactive") state = "ActiveUp";
    		if (state == "ActiveUp"){
    			intensity += Time.deltaTime * (intensity + 1) ;
    			if (intensity >= 2.5) state = "ActiveDown";
    		}
    		if (state == "ActiveDown"){
    			intensity -= Time.deltaTime * 4;
    			if (intensity <= 0){
    				state = "Inactive";

    				if (isHint)
	    				{
	    				--runtime;
	    				if (runtime == 0)
	    				{
	    					activate = false;
	    					runtime = 5;
	    				}
	    				intensity = 0;
    				}
    				else
    				{
    					activate = false;
    					intensity = 0;
    				}
    			}
    		}

        	mat.SetFloat("Vector1_Intensity", intensity);
    	}
    }
}
