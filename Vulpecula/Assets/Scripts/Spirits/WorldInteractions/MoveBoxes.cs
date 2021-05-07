using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    Rigidbody rb;
    public SpiritHandler script;
    public float magnitude;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.isKinematic = !script.strongSpirit;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            direction.y /=  2;
            rb.AddForce(direction * magnitude);
        }
    }
    
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            direction.y /=  2;
            rb.AddForce(direction * magnitude);
        }
    }    
}
