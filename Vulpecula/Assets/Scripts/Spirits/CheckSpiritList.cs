using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpiritList : MonoBehaviour
{
    public SpiritHandler player;
    public int expectedListSize;
    public GameObject dt;

     public void OnTriggerEnter(Collider obj)
    {
    	if (player.SpiritList.Count == expectedListSize){
    		Destroy(dt);
    		Destroy(gameObject);
    	}
    }
}
