using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
	public Vector3 sendRock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
        	var pull = other.gameObject.GetComponent<SpiritHandler>();
        	pull.nearButton = true;
        	pull.rockgoto = sendRock;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    	if (other.gameObject.tag == "Player"){
        	var pull = other.gameObject.GetComponent<SpiritHandler>();
        	pull.nearButton = false;
        }
    }


    
}
