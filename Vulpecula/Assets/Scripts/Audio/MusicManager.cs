using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform);
    }

    private void Start()
    {
        PlayMusic(defaultMusicIndex);
    }

    private string[] musicFNs = new string[] {"event:/Music/Menu Music",
        "event:/Music/Level 1 BGM",
        "event:/Music/Level 2 BGM" };

    public enum eMusicIndices
    {
        Null = -1,
        MainMenu = 0,
        Tutorial,
        Level1
    }

    [SerializeField]
    private eMusicIndices defaultMusicIndex = eMusicIndices.Null;

    private eMusicIndices currentMusicIndex = eMusicIndices.Null;

    private FMOD.Studio.EventInstance musicInstance;

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusicIfNotAlready(eMusicIndices musicIndex)
    {
        if (currentMusicIndex != musicIndex)
            PlayMusic(musicIndex);
    }
    public void PlayMusic(eMusicIndices musicIndex)
    {
        currentMusicIndex = musicIndex;

        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        if (musicIndex == eMusicIndices.Null)
            return;

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicFNs[(int)musicIndex]);

        musicInstance.start();
        musicInstance.release();
    }

    public void SetMusicSegment(int segmentID)
    {
        musicInstance.setParameterByName("Music Segment", segmentID);
    }
}
