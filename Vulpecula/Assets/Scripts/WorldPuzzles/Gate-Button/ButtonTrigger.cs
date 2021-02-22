using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject gate;
    public Vector3 initPos;
    public Vector3 openPos;

    public string state;
    private float time;
    void start()
    {
    	initPos = gate.transform.position;
    	state = "Closed";
    	time = 0;
    }

    void Update()
    {	
    	if (state == "Closed-To-Open" && time >= 1) {
    		state = "Opened";
    		time = 1;
    	}
    	else if (state == "Open-To-Closed" && time <= 0) {
    		state = "Closed";
    		time = 0;
    	}

        else if (state == "Closed-To-Open"){
        	gate.transform.position = Vector3.Lerp(initPos, openPos, time);
        	time += Time.deltaTime/5f;
        }
        else if (state == "Open-To-Closed"){
        	gate.transform.position = Vector3.Lerp(openPos, initPos, 1-time);
        	time -= Time.deltaTime*3.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    	if (state == "Opened") return;
        else if (other.gameObject.tag == "Spirit_Land"){
        	var pull = other.gameObject.GetComponent<SpriritMovement_Land>();
        	if (pull.type == "Sit") state = "Closed-To-Open";
        }
    }

    private void OnTriggerExit(Collider other)
    {
    	if (state == "Closed") return;
        else if (other.gameObject.tag == "Spirit_Land"){
        	var pull = other.gameObject.GetComponent<SpriritMovement_Land>();
        	if (pull.type == "Sit") state = "Open-To-Closed";
        }
    }
}
