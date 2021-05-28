using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject target;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<ButtonTrigger>().state != "Closed"){
        	DestroyObject(gameObject);
        }
    }
}
