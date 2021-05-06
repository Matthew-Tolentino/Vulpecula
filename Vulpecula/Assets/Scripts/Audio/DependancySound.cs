using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependancySound : MonoBehaviour
{

    [SerializeField]
    private List<FMODUnity.StudioEventEmitter> notPlayingSounds;
    [SerializeField]
    private int durationToWait = 10; // in ms

    public void Play()
    {
        foreach(var emitter in notPlayingSounds)
        {
            if (emitter.IsPlaying())
            {
                emitter.EventInstance.getTimelinePosition(out int currentPos);
                if (currentPos <= durationToWait)
                    return;
            }
        }

        GetComponent<FMODUnity.StudioEventEmitter>().Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
