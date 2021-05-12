using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMaze : MonoBehaviour
{
	public GameObject target;
	public GameObject player;
	private Renderer ren;
	private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        ren = target.GetComponent<Renderer>();
        mat = ren.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetVector("_Vector3_Position", player.transform.position);
    }
}
