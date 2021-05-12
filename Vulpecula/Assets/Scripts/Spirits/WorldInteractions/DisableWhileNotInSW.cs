using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhileNotInSW : MonoBehaviour
{
    public MoveToSpiritWorld target;
    public GameObject setObject;
    
    private string getState;
    public bool isFloating;
    void Start()
    {
        setObject.SetActive(false);
        if (!isFloating) getState = setObject.GetComponent<SpriritMovement_Land>().state;
        else getState = setObject.GetComponent<SpiritMovement_Floating>().state;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.state == "Human") setObject.SetActive(false);
        else setObject.SetActive(true);

        if (!isFloating) getState = setObject.GetComponent<SpriritMovement_Land>().state;
        else getState = setObject.GetComponent<SpiritMovement_Floating>().state;
        if (getState != "Spawn") Destroy(this);
    }
}
