using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHintVisual : MonoBehaviour
{
	private bool triggererd;
	public GameObject target;
    void Start()
    {
        triggererd = false;
    }

   	void OnTriggerEnter(Collider col)
    {
    	if (!triggererd && col.gameObject.tag == "Player"){
    		var pull = target.GetComponent<SpiritGlowControl>();
    		if (pull != null) pull.activate = true;
    		triggererd = true;
    	}
    }
}
