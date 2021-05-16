using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    [SerializeField]
    private MusicManager.eMusicIndices musicIndex = MusicManager.eMusicIndices.Null;

    [SerializeField]
    private bool continueIfAlreadyPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        if (continueIfAlreadyPlaying)
            MusicManager.instance.PlayMusicIfNotAlready(musicIndex);
        else
            MusicManager.instance.PlayMusic(musicIndex);
    }
}
