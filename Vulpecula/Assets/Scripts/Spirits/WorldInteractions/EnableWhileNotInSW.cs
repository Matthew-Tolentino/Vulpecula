using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhileNotInSW : MonoBehaviour
{
    public MoveToSpiritWorld target;
    public GameObject setObject;
    
    void Start()
    {
        setObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.state == "Human") setObject.SetActive(true);
        else setObject.SetActive(false);
    }
}
