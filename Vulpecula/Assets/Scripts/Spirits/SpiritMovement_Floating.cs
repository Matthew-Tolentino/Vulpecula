using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMovement_Floating : MonoBehaviour
{
    public GameObject pl;
    public Transform player;
    public float radius;
    private float initialD;

    public float verticalFluct;
    public float initialF;
    public float numVertFluct;

    private Vector3 moveTo;
    private Rigidbody rb;
    public string state;

    private Vector3 spawn;
    public float speed;

    public string type;
    private Collider fs;

    private float accel;
    private float timeAway;

    private float forceMag;

    // Dialog Code (Matthew) ---------------------
    [HideInInspector]
    public bool saidDialog = false;
    // -------------------------------------------

    private void Start()
    {
        fs = GetComponent<Collider>();
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        state = "Spawn";
        if (type == "") type = "NULL";
        accel = 1.0f;
        timeAway = 0.0f;

        forceMag = 20000f;
    }

    private void Update()
    {
    	accel = 1.0f;
        var pull = pl.GetComponent<SpiritHandler>();
        if (state == "OnPlayer")
        {
            float angle = pull.rotDegree * Time.fixedDeltaTime;
            Vector3 raiseHeight = player.transform.position;
            //raiseHeight.y += 2;
            moveTo = raiseHeight + new Vector3((radius * Mathf.Cos(angle + initialD)), 0f, (radius * Mathf.Sin(angle + initialD)));
            moveTo.y += Mathf.Cos(angle * numVertFluct + initialF) * verticalFluct;

            float distance = Vector3.Distance(player.position, transform.position);
            if (distance > radius){
            	fs.enabled = true;
            	rb.detectCollisions = true;
            	timeAway += Time.deltaTime;
            	if (timeAway > 1f){
            		fs.enabled = false;
            		rb.detectCollisions = false;
            		timeAway = 0f;
            		accel = 5f;
            	}
            }
            else timeAway = 0f;
        }

    }

    private void LateUpdate()
    {
        if (state != "Spawn" && state != "ReturningToSpawn")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed * accel;
            rb.velocity = direction;
        }
            
    }

    public void ObtainSpiritFloating(int d)
    {   
        state = "OnPlayer";
        initialD = 90 * d;
    }

    public void ReleaseSpiritFloating()
    {
        if (state != "ReturningToSpawn" && state != "Spawn")
            {
            state = "ReturningToSpawn";
            Vector3 direction = (transform.position - player.position).normalized * (forceMag + Random.value*1000);
            direction.x += (Random.value - 1)*forceMag/2;
            direction.z += (Random.value - 1)*forceMag/2;
            direction.y = 0;

            rb.detectCollisions = true;
            rb.AddForce(direction);
            
            Invoke("finishRun", 1.0f);
        }
        
    }

    private void finishRun()
    {
        state = "Spawn";
        rb.velocity = new Vector3(0,0,0);
    }

}
