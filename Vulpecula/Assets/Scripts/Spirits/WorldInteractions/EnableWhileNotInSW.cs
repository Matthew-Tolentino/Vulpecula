using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhileNotInSW : MonoBehaviour
{
    public MoveToSpiritWorld target;
    public GameObject setObject;
    public bool inverted;
    
    void Start()
    {
        setObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        bool set = target.state == "Human";
        if (inverted) set = !(target.state == "Human");
        if (set) setObject.SetActive(true);
        else setObject.SetActive(false);
    }
}
