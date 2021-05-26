using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSpiritWorld : MonoBehaviour
{
    public static int inSpiritWorld;
	public string state;
	public bool touching;
	public Vector3 origin;
	public float distance;
    public GameObject player;

    public float testValue;

    public float sharedVal;

    public float secStart;
    public float secEnd;

    // Start is called before the first frame update
    void Start()
    {
    	sharedVal = 0;
        state = "Human";
        touching = false;
        inSpiritWorld = 0;
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
            inSpiritWorld -= 1;
            // beginning to exit spirit world
            StartCoroutine(Audio_FadeOutOfSpiritWorld(fadeOutTime));
        }
        if (state == "Spirit" && sharedVal < 1){
        	sharedVal += Time.deltaTime*(1/secStart);
        	if (sharedVal > 1) sharedVal = 1;
        }
        if (state == "Human" && sharedVal > 0){
        	sharedVal -= Time.deltaTime*(1/secEnd);
        	if (sharedVal < 0) sharedVal = 0;
        }

    }

    void lampAbility(){
        var pull = player.GetComponent<SpiritHandler>();
        if (touching && state != "Spirit"){
            state = "Spirit";
            inSpiritWorld += 1;
            // begining to enter spirit world
            StartCoroutine(Audio_FadeIntoSpiritWorld(fadeInTime));
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

    [SerializeField]
    private bool controlSpiritHum = false;

    private float fadeInTime = 1.75f; // in seconds
    private float fadeOutTime = 2; // in seconds
    IEnumerator Audio_FadeIntoSpiritWorld(float duration)
    {
        if (controlSpiritHum)
        {
            foreach (var noodler in NoodlerManager.instance.noodlers)
            {
                noodler.StartPlaying();
            }
        }

        float currentTime = 0f;
        while (currentTime < duration)
        {
            float connection = Mathf.Lerp(0, 1, currentTime / duration);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Spirit World Connection", connection);
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
    IEnumerator Audio_FadeOutOfSpiritWorld(float duration)
    {
        if (controlSpiritHum)
        {
            foreach (var noodler in NoodlerManager.instance.noodlers)
            {
                noodler.StopPlaying();
            }
        }

        float currentTime = 0f;
        while (currentTime < duration)
        {
            float connection = Mathf.Lerp(1, 0, currentTime / duration);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Spirit World Connection", connection);
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
