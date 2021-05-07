using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhileNotInSW : MonoBehaviour
{
    public MoveToSpiritWorld target;
    public GameObject setObject;
    public string getState;
    void Start()
    {
        setObject.SetActive(false);
        getState = setObject.GetComponent<SpriritMovement_Land>().state;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.state == "Human") setObject.SetActive(false);
        else setObject.SetActive(true);

        getState = setObject.GetComponent<SpriritMovement_Land>().state;
        if (getState != "Spawn") Destroy(this);
    }
}
