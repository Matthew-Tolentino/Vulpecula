using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject gate;
    public float initPos;
    public float openPos;

    public string state;
    private float time;

    [SerializeField]
    private FMODUnity.StudioEventEmitter gateOpenSound = null;
    [SerializeField]
    private FMODUnity.StudioEventEmitter gateCloseSound = null;
    void start()
    {
    	//initPos = gate.transform.position;
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
    	    gate.transform.eulerAngles = new Vector3(
		        gate.transform.eulerAngles.x,
		        Mathf.Lerp(initPos, openPos, time),
		        gate.transform.eulerAngles.z
		    );
        	time += Time.deltaTime/5f;
        }
        else if (state == "Open-To-Closed"){
        	gate.transform.eulerAngles = new Vector3(
		        gate.transform.eulerAngles.x,
		        Mathf.Lerp(openPos, initPos, 1-time),
		        gate.transform.eulerAngles.z
		    );
        	time -= Time.deltaTime*3.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    	if (state == "Opened" || state == "Closed-To-Open") return;
        else if (other.gameObject.tag == "Spirit_Land"){
        	var pull = other.gameObject.GetComponent<SpriritMovement_Land>();
            if (pull.type == "Sit" && (pull.state == "ForceMovement" || pull.state == "ForcedMovent_Idle"))
            {
                if (gateCloseSound != null && gateOpenSound != null)
                {
                    if (gateCloseSound.IsPlaying())
                        gateCloseSound.Stop();
                    if (!gateOpenSound.IsPlaying())
                    {
                        gateOpenSound.Play();
                    }
                }
                
                state = "Closed-To-Open";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
    	if (state == "Closed" || state == "Open-To-Closed") return;
        else if (other.gameObject.tag == "Spirit_Land"){
        	var pull = other.gameObject.GetComponent<SpriritMovement_Land>();
            if (pull.type == "Sit")
            {
                if (gateOpenSound != null && gateCloseSound != null)
                {
                    if (gateOpenSound.IsPlaying())
                        gateOpenSound.Stop();
                    if (!gateCloseSound.IsPlaying())
                    {
                        gateCloseSound.Play();
                    }
                }
                
                state = "Open-To-Closed";
            }
        }
    }
}
