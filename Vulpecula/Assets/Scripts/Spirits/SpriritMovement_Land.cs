using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriritMovement_Land : MonoBehaviour
{
	public GameObject pl;
    public Transform player;

    private Vector3 spawn;
    public float speed;
    private Rigidbody rb;

    public string state;
    private Vector3 moveTo;
    public float timer;
    private Collider fs;

    public string type;
    private float accel;

    private Vector3 forceMove;
    private GameObject follow;

    private float forceMag;

    public Vector3 velo;
    // Dialog Code (Matthew) ---------------------
    [HideInInspector]
    public bool saidDialog = false;
    // -------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        state = "Spawn";
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        timer = 5f;
        fs = GetComponent<Collider>();
        fs.enabled = true;

        if (type == "") type = "NULL";
        accel = 1f;

        forceMove = new Vector3();
        follow = null;

        forceMag = 20000f;

        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine action based on state

        // Folow Player
        if (state == "OnPlayer")
        {
            moveTo = player.position;
            rb.isKinematic = false;
            
            // Stop moving if come too close to player
            if (Vector3.Distance(transform.position, player.position) < 2f) {
                state = "OnPlayer_Idle";
                accel = 1f;
                timer = 5f;
            }
            timer -= Time.deltaTime;
            if (timer <= 0 ) {
                rb.detectCollisions = false;
                timer = 5f;
            }
        }

        // Stay away from player
        else if (state == "OnPlayer_Idle")
        {   
            rb.detectCollisions = true;
            moveTo = transform.position;
            rb.useGravity = true;
            fs.enabled = true;
            rb.isKinematic = true;

            // Come back to Player if too far
            if (Vector3.Distance(transform.position, player.position) > 3f) {
                state = "OnPlayer";
                timer = 5f;
            }
        }
        
        // Force movement to desired position
        else if (state == "ForceMovement")
        {   
            if (follow != null) 
            {
                forceMove = follow.transform.position;
            }
        	rb.isKinematic = false;
            moveTo = forceMove;

            // Reach desired Position
            if (Mathf.Round(transform.position.x) == Mathf.Round(forceMove.x) && Mathf.Round(transform.position.z) == Mathf.Round(forceMove.z))
            {
                state = "ForcedMovent_Idle";
                //rb.isKinematic = true;
                accel = 1f;
                rb.detectCollisions = true;
                fs.enabled = true;
                rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            }
        }

        transform.rotation = Quaternion.LookRotation((player.position - transform.position).normalized);
    }

    private void LateUpdate()
    {
        // Do action based on state
        if (state != "ForceMovement") follow = null;
        if (state != "ForcedMovent_Idle" && state != "Spawn") rb.constraints = RigidbodyConstraints.None;

        // Move to set Position
        if (state != "Spawn" && state != "ForcedMovent_Idle" && state != "OnPlayer_Idle" && state != "ReturningToSpawn")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed * accel;
            rb.velocity = direction;
            velo = rb.velocity;
        }
    }

    // Player Get spirit
    public void ObtainSpiritLand()
    {
    	Physics.IgnoreCollision(pl.GetComponent<Collider>(), GetComponent<Collider>(), true);
        state = "OnPlayer";
        accel = 5f;
        rb.constraints = RigidbodyConstraints.None;
    }

    // Player Lose spirit
    public void ReleaseSpiritLand()
    {
    	Physics.IgnoreCollision(pl.GetComponent<Collider>(), GetComponent<Collider>(), false);
        if (state != "ReturningToSpawn" && state != "Spawn")
            {
            state = "ReturningToSpawn";
            rb.isKinematic = false;

            Vector3 direction = (transform.position - player.position).normalized * (forceMag + Random.value*1000);
            direction.x += (Random.value - 1)*forceMag/2;
            direction.z += (Random.value - 1)*forceMag/2;
            direction.y = forceMag/5;

            rb.detectCollisions = true;
            rb.AddForce(direction);
            
            Invoke("finishRun", 1.0f);
        }
    }

    // Set spirit to lost
    private void finishRun()
    {
        state = "Spawn";
        rb.velocity = new Vector3(0,0,0);
    }

    // Rock force move
    public void abilityMove(Vector3 pos, GameObject x = null)
    {
        state = "ForceMovement";
        if (x != null)
        {
            forceMove = x.transform.position;
            follow = x;
        }
        else forceMove = pos;
        accel = 5f;
        fs.enabled = false;
        rb.detectCollisions = false;
    }


}
