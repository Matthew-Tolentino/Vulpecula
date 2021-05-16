using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicParams : MonoBehaviour
{

    private enum eTriggerTypes
    {
        ONOBJECTSTART = 0,
        ONOBJECTDESTROY,
    }

    [SerializeField]
    private eTriggerTypes triggerType = eTriggerTypes.ONOBJECTSTART;

    private enum eParamTypes
    {
        SEGMENT = 0,
    }

    [SerializeField]
    private eParamTypes paramType = eParamTypes.SEGMENT;

    [SerializeField]
    private float value = 0;

    void SetSegment()
    {
        MusicManager.instance.SetMusicSegment((int)value);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (triggerType == eTriggerTypes.ONOBJECTSTART)
        {
            switch (paramType)
            {
                case eParamTypes.SEGMENT:
                    SetSegment();
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        if (triggerType == eTriggerTypes.ONOBJECTDESTROY)
        {
            switch (paramType)
            {
                case eParamTypes.SEGMENT:
                    SetSegment();
                    break;
            }
        }
    }
}
