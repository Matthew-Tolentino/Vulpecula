using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfCollected : MonoBehaviour
{
    public SpiritMovement_Floating target;
    public GameObject dialogue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.state != "Spawn") 
        {
            Destroy(dialogue);
            Destroy(gameObject);
        }
    }
}
