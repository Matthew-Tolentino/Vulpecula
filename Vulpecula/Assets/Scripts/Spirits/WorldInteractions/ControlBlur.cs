using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBlur : MonoBehaviour
{
	public GameObject target;
	public float maxSize;
    private Renderer ren;
	private Material mat;

    void Start()
    {
        ren = this.GetComponent<Renderer>();
        mat = ren.material;
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var pull = target.GetComponent<MoveToSpiritWorld>();
        float val = pull.sharedVal;

        float x = val * maxSize;
        transform.localScale = new Vector3(x, x, x);

        mat.SetFloat("Vector1_B06761C4", (0.004f * val));
    }
}
