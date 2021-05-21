using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicParams : MonoBehaviour
{

    private enum eTriggerTypes
    {
        OnObjectStart = 0,
        OnObjectDestroy,
        OnPlayerTriggerEnter,
        OnPlayerTriggerExit,
    }

    [SerializeField]
    private eTriggerTypes triggerType = eTriggerTypes.OnObjectStart;

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

    void SetParam()
    {
        switch (paramType)
        {
            case eParamTypes.SEGMENT:
                SetSegment();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (triggerType == eTriggerTypes.OnObjectStart)
            SetParam();
    }

    private void OnDestroy()
    {
        if (triggerType == eTriggerTypes.OnObjectDestroy)
            SetParam();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerType == eTriggerTypes.OnPlayerTriggerEnter && other.CompareTag("Player"))
            SetParam();
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerType == eTriggerTypes.OnPlayerTriggerExit && other.CompareTag("Player"))
            SetParam();
    }
}
