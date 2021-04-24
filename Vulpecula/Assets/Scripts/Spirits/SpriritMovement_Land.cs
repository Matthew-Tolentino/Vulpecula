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

    }

    // Update is called once per frame
    void Update()
    {
        if (state == "OnPlayer")
        {
            moveTo = player.position;
            rb.isKinematic = false;
            //moveTo.y = transform.position.y;
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
        else if (state == "OnPlayer_Idle")
        {   
            rb.detectCollisions = true;
            moveTo = transform.position;
            rb.useGravity = true;
            fs.enabled = true;
            rb.isKinematic = true;
            if (Vector3.Distance(transform.position, player.position) > 3f) {
                state = "OnPlayer";
                timer = 5f;
            }
        }
        else if (state == "ReturnToSpawn")
        {
            moveTo = spawn;
            rb.isKinematic = false;
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                rb.detectCollisions = false;
                rb.useGravity = false;
                fs.enabled = false;
                accel = 5f;

            }
            if (Mathf.Round(transform.position.x) == Mathf.Round(spawn.x) && Mathf.Round(transform.position.z) == Mathf.Round(spawn.z))
            {
                state = "Spawn";
                timer = 5f;
                rb.useGravity = true;
                fs.enabled = true;
                rb.detectCollisions = true;
                accel = 1f;
            }
        }
        else if (state == "ForceMovement")
        {   
            if (follow != null) 
            {
                forceMove = follow.transform.position;
            }
        	rb.isKinematic = false;
            moveTo = forceMove;
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
        if (state != "ForceMovement") follow = null;
        if (state != "ForcedMovent_Idle") rb.constraints = RigidbodyConstraints.None;
        if (state != "Spawn" && state != "ForcedMovent_Idle" && state != "OnPlayer_Idle")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed * accel;
            rb.velocity = direction;
            velo = rb.velocity;
        }
        else{
            rb.velocity = new Vector3(0, rb.velocity.y ,0);
            velo = rb.velocity;
        }
    }

    public void ObtainSpiritLand()
    {
    	Physics.IgnoreCollision(pl.GetComponent<Collider>(), GetComponent<Collider>(), true);
        state = "OnPlayer";
        accel = 5f;
    }

    public void ReleaseSpiritLand()
    {
    	Physics.IgnoreCollision(pl.GetComponent<Collider>(), GetComponent<Collider>(), false);
        state = "ReturnToSpawn";
    }

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
