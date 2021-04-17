using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float strength;
    private void OnTriggerStay(Collider collision){
    	if (collision.gameObject.tag == "Box"){
    		if (true){
    			Vector3 direction = collision.gameObject.transform.position - transform.position;
    			direction = direction.normalized;
    			direction.y = 0f;
    			var pull = collision.gameObject.GetComponent<Rigidbody>();
    			pull.velocity = direction * strength;
    		}
    	}
    }
        
}
