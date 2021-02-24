using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTransform : MonoBehaviour
{	
	public GameObject target;
	public float maxDistance;

	private Renderer ren;
	private Material mat;
 	
    void Awake()
    {
    	ren = this.GetComponent<Renderer>();
        mat = ren.material;
    }

    // Update is called once per frame
    void Update()
    {
        var pull = target.GetComponent<MoveToSpiritWorld>();
        float val = pull.sharedVal;

        mat.SetFloat("Vector1_Distance", (maxDistance * val));
    }
}
