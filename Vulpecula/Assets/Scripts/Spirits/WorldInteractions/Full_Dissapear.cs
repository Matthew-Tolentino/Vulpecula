using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Full_Dissapear : MonoBehaviour
{
    public GameObject target;

	private Renderer ren;
	private Material mat;
	private Collider c;

    public bool isInverted;
 	
    void Awake()
    {
    	ren = this.GetComponent<Renderer>();
        mat = ren.material;
        c = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        var pull = target.GetComponent<MoveToSpiritWorld>();
        float val;
        if (!isInverted) val = pull.sharedVal;
        else val = 1- pull.sharedVal;
        if (val == 0) c.enabled = true;
        else c.enabled = false;

        mat.SetFloat("Vector1_5F5F4195", val);
    }
}
