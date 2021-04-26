using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFirstSpirit : MonoBehaviour
{
    public SpiritMovement_Floating target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 newPos = transform.position;
        newPos.y += Mathf.Sin(Time.time*2)/150;
        transform.position = newPos;

        if (target.state != "Spawn") Destroy(gameObject);
    }
}
