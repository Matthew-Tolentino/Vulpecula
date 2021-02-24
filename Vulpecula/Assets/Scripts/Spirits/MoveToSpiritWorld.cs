using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSpiritWorld : MonoBehaviour
{
	public string state;
	public bool touching;
	public Vector3 origin;
	public float distance;
    public GameObject player;

    public float testValue;

    public float sharedVal;

    // Start is called before the first frame update
    void Start()
    {
    	sharedVal = 0;
        state = "Human";
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Vector3.Distance(origin, player.transform.position);
        testValue = x;
        var pull = player.GetComponent<SpiritHandler>();

        if (pull.triggerLamp) lampAbility();
        if (x >= distance && state == "Spirit"){
        	state = "Human";
        }
        if (state == "Spirit" && sharedVal < 1){
        	sharedVal += Time.deltaTime/3;
        	if (sharedVal > 1) sharedVal = 1;
        }
        if (state == "Human" && sharedVal > 0){
        	sharedVal -= Time.deltaTime/3;
        	if (sharedVal < 0) sharedVal = 0;
        }
    }

    void lampAbility(){
        var pull = player.GetComponent<SpiritHandler>();
        pull.triggerLamp = false;
        if (touching){
            state = "Spirit";
        }
    }

    void OnTriggerStay(Collider col){
    	if (col.gameObject.tag == "Player"){
    		touching = true;
    	}
    }
    void OnTriggerExit(Collider col){
    	if (col.gameObject.tag == "Player"){
    		touching = false;
    	}
    }
}
