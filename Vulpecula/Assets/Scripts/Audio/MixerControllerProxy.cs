using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerControllerProxy : MonoBehaviour
{
    public void SetMasterVolume(float volume)
    {
        MixerController.instance.SetMasterVolume(volume);
    }
}
