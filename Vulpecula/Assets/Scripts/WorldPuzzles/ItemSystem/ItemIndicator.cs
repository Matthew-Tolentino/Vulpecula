using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndicator : MonoBehaviour
{
    public Light _light;
    public float freqency = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        _light.intensity = Mathf.Abs(Mathf.Sin(Time.time * freqency));
    }
}
