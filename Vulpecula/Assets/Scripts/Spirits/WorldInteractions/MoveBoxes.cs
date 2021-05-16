﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    Rigidbody rb;
    public SpiritHandler script;
    public float magnitude;

    private Vector3 pastPos;
    private bool move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("delayedContraint", 1.0f);
    }

    
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && script.StrongActive) {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            direction.y /=  4;
            rb.AddForce(direction * magnitude);
        }
    }    

    private void delayedContraint(){
        rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }
}
