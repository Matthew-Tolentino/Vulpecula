using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    Rigidbody rb;
    public SpiritHandler script;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.isKinematic = !script.strongSpirit;
    }

    
        
}
