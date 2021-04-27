using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class VisionEffect : MonoBehaviour
{
    public GameObject v;
    private Volume volume;

    private Vignette _Vig;

    private bool seenEffect = false;

    void Start()
    {
        volume = v.GetComponent<Volume>();
        _Vig = v.GetComponent<Vignette>();
        
        _Vig.intensity.value = 0.715f;
    }


    void Update()
    {
        if (seenEffect)
        {
            _Vig.intensity.value = 0.715f;
        }
        else
        {
            _Vig.intensity.value = 0.715f;
        }
    }
}
