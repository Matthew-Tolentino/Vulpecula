using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSpiritWorldController : MonoBehaviour
{
	private string state;
	private Collider c;
	private MeshRenderer mesh;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        //state = "Human";
        c = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var pull = target.GetComponent<MoveToSpiritWorld>();
        string tState = pull.state;

        if (tState != state){
            if (state == "Spirit") ToHuman();
            else ToSpirit();
        }
    }

    public void ToSpirit(){
    	c.enabled = false;
    	mesh.enabled = false;
    	state = "Spirit";
    }

    public void ToHuman(){
    	c.enabled = true;
    	mesh.enabled = true;
		state = "Human";
    }
}
