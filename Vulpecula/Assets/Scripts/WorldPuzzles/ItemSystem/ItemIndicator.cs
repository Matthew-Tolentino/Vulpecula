using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndicator : MonoBehaviour
{
    public Light light;
    public float freqency = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        light.intensity = Mathf.Abs(Mathf.Sin(Time.time * freqency));
    }
}
