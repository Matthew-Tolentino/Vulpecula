using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTransform : MonoBehaviour
{	
	public GameObject target;
	public float maxDistance;

	private Renderer ren;
	private Material mat;
	public bool notFloor;
	private Collider c;

    void Awake()
    {
    	ren = this.GetComponent<Renderer>();
        mat = ren.material;
        if(notFloor) c = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        var pull = target.GetComponent<MoveToSpiritWorld>();
        float val = pull.sharedVal;

        mat.SetFloat("Vector1_Distance", (maxDistance * val));

        if(notFloor && val==0) c.enabled = true;
        else if (notFloor) c.enabled = false;
    }
}
