using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoNotDestroySounds : MonoBehaviour
{

    [SerializeField]
    public DoNotDestroySound.eSoundTag soundTag = DoNotDestroySound.eSoundTag.NONE;

    public enum eTriggerTypes
    {
        ONOBJECTSTART = 0,
        ONOBJECTDESTROY,
    }

    [SerializeField]
    public eTriggerTypes triggerType = eTriggerTypes.ONOBJECTSTART;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerType == eTriggerTypes.ONOBJECTSTART)
        {
            foreach (var sound in DoNotDestroySound.FindAllDoNotDestroySoundsByTag(soundTag))
            {
                Destroy(sound.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (triggerType == eTriggerTypes.ONOBJECTDESTROY)
        {
            foreach (var sound in DoNotDestroySound.FindAllDoNotDestroySoundsByTag(soundTag))
            {
                Destroy(sound.gameObject);
            }
        }
    }
}
