using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisibility : MonoBehaviour
{
    public GameObject target;
    public bool isVisible;

    public MoveToSpiritWorld trigger;
    public bool spiritSet;
    private string triggerCompare;
    private MeshRenderer ren;

    void Start()
    {
        if (spiritSet) triggerCompare = "Spirit";
        else triggerCompare = "Human";
        ren = target.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.state == triggerCompare) ren.enabled = true;
        else ren.enabled = false;
    }
}
