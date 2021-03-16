using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
//using System;

public class SpiritNoodler : MonoBehaviour
{

    FMOD.Studio.EVENT_CALLBACK soundPlayedCallback;
    FMOD.Studio.EventInstance musicInstance;

    [SerializeField]
    private bool playOnStart = true;

    private int nextSemitone;
    private bool needsSemitoneUpdate = true;

    private enum SCALE_TYPES { PENTATONIC };

    private static int[] PentatonicIntervals = {0, 2, 4, 7, 9, 12};

    private static float TREMULOMAX = 0.6f;

    private static int ClampToBaseOctive(int semitoneInterval)
    {
        while (semitoneInterval > 12)
        {
            semitoneInterval -= 12;
        }
        while (semitoneInterval < 0)
        {
            semitoneInterval += 12;
        }
        return semitoneInterval;
    }
    private static int ClampToScale(int semitoneInterval, SCALE_TYPES scale)
    {
        semitoneInterval = ClampToBaseOctive(semitoneInterval);
        switch (scale)
        {
            case SCALE_TYPES.PENTATONIC:
                if (System.Array.Exists(PentatonicIntervals, element => element == semitoneInterval))
                    return semitoneInterval;
                for (int i = 0; i < PentatonicIntervals.Length; ++i)
                {
                    if (semitoneInterval < PentatonicIntervals[i])
                        return PentatonicIntervals[i];
                }
                break;
            default:
                return semitoneInterval;
        }
        return semitoneInterval;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        soundPlayedCallback = new FMOD.Studio.EVENT_CALLBACK(SoundPlayedCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Spirits/Test Hum 2");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(musicInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());

        // Pin the class that will store the data modified during the callback
        //timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        //musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(soundPlayedCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.SOUND_PLAYED);

        if (playOnStart)
            musicInstance.start();
    }

    void OnDestroy()
    {
        musicInstance.setUserData(System.IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }

    //void OnGUI()
    //{
    //    GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.currentMusicBar, (string)timelineInfo.lastMarker));
    //}

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT SoundPlayedCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, System.IntPtr instancePtr, System.IntPtr paramsPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);
        System.Random rand = new System.Random();
        int semitones = rand.Next(-12, 13);
        semitones = ClampToScale(semitones, SCALE_TYPES.PENTATONIC);
        instance.setParameterByName("Pitch", SemitoneToPitchVar((float)semitones)); //(float)UnityEngine.Random.Range(-1, 1)

        float tremuloDepth = (float)rand.NextDouble() * TREMULOMAX;
        instance.setParameterByName("Tremulo Depth", tremuloDepth);

        return FMOD.RESULT.OK;
    }

    static float SemitoneToPitchVar(float semitones)
    {
        return semitones / 24;
    }

    public void StartPlaying()
    {
        musicInstance.setTimelinePosition(0);
        musicInstance.start();
    }
    public void StopPlaying()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
