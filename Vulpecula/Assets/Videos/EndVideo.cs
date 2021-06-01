using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndVideo : MonoBehaviour
{
    public ControlSlides pull;
    private bool once = true;
    void Update()
    {
        var vd = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        if (once){
            once = false;
            vd.loopPointReached += EndReached;
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        pull.skip();

    }
}
