using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeight : MonoBehaviour
{
	public bool leftSide;
	public GameObject target;
	private Collider fs;

    // Update is called once per frame
    void Update()
    {
        fs = GetComponent<Collider>();
        fs.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
    	if (other.tag == "Player")
    	{
    		if (leftSide) target.GetComponent<SeeSawControl>().leftWeight += 1;
    		else target.GetComponent<SeeSawControl>().rightWeight += 1;
    	}
    	else if(other.tag == "Spirit_Land")
    	{
    		if (other.GetComponent<SpriritMovement_Land>().type == "Rock"){
    			if (leftSide) target.GetComponent<SeeSawControl>().leftWeight += 5;
    			else target.GetComponent<SeeSawControl>().rightWeight += 5;
    		}
    	}
    }

    private void OnTriggerExit(Collider other)
    {
    	if (other.tag == "Player")
    	{
    		if (leftSide) target.GetComponent<SeeSawControl>().leftWeight -= 1;
    		else target.GetComponent<SeeSawControl>().rightWeight -= 1;
    	}
    	else if(other.tag == "Spirit_Land")
    	{
    		if (other.GetComponent<SpriritMovement_Land>().type == "Rock"){
    			if (leftSide) target.GetComponent<SeeSawControl>().leftWeight -= 5;
    			else target.GetComponent<SeeSawControl>().rightWeight -= 5;
    		}
    	}
    }
}
