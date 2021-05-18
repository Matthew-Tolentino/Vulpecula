using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempExtendedRange : MonoBehaviour
{
    [SerializeField] private Collider c;
    [SerializeField] private SpriritMovement_Land script;

    // Update is called once per frame
    void Update()
    {
        if (script.state != "Spawn")
        {
        	Destroy(c);
        	Destroy(this);
        }
    }
}
