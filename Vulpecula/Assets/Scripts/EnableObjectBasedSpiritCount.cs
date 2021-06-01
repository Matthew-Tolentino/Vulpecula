using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectBasedSpiritCount : MonoBehaviour
{
    public SpiritHandler list;
    public int count;
    public bool enableOnCount;
    public GameObject target;

    void Start()
    {
        target.SetActive(!enableOnCount);
    }


    // Update is called once per frame
    void Update()
    {
        bool check = list.SpiritList.Count == count;
        if (!enableOnCount) check = !check;

        target.SetActive(check);
    }
}
