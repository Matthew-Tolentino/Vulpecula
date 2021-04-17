using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreByTag : MonoBehaviour
{
	private Collider ownCol;
	public float strength;
    // Start is called before the first frame update
    void Start()
    {
        ownCol = GetComponent<Collider>();
        ownCol.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision){
    	if (collision.gameObject.tag == "Box"){
    		var pull = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 direction = new Vector3(1, 0, -5);
			direction = direction.normalized;
			pull.velocity = direction * strength;
    	}
    }
    void OnTriggerStay(Collider collision){
    	if (collision.gameObject.tag == "Box"){
    		var pull = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 direction = new Vector3(1, 0, -5);
			direction = direction.normalized;
			pull.velocity = direction * strength;
    	}
    }
}
