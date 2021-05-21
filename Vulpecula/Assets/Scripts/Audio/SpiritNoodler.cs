using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritNoodler : MonoBehaviour
{
    FMOD.Studio.EVENT_CALLBACK soundPlayedCallback;
    FMOD.Studio.EventInstance musicInstance;

    [SerializeField]
    private bool playOnStart = true;

    [SerializeField]
    private float volume = 0.5f;

    [SerializeField]
    private SCALE_TYPES scaleType = SCALE_TYPES.PENTATONIC;

    [SerializeField]
    int minNote = -12;
    [SerializeField]
    int maxNote = 13;

    [SerializeField]
    float minTremulo = 0f;
    [SerializeField]
    float maxTremulo = 0.6f;

    private bool isPaused = true;

    private int nextSemitone;
    private bool needsSemitoneUpdate = true;

    private enum SCALE_TYPES { PENTATONIC, MINOR, CHROMATIC };

    private static int[] PentatonicIntervals = { 0, 2, 4, 7, 9, 12 };
    private static int[] MinorIntervals = { 0, 2, 3, 5, 7, 9, 10, 12 };
    // chromatic

    private static int NUM_NOTES = 4;

    private System.Random rand = new System.Random();

    private static int ClampToBaseOctive(int semitoneInterval, out int octavesOffset)
    {
        octavesOffset = 0;
        while (semitoneInterval > 12)
        {
            octavesOffset++;
            semitoneInterval -= 12;
        }
        while (semitoneInterval < 0)
        {
            octavesOffset--;
            semitoneInterval += 12;
        }
        return semitoneInterval;
    }
    private static int ClampToScale(int semitoneInterval, SCALE_TYPES scale)
    {
        int octavesOffset;
        semitoneInterval = ClampToBaseOctive(semitoneInterval, out octavesOffset);
        switch (scale)
        {
            case SCALE_TYPES.PENTATONIC:
                if (System.Array.Exists(PentatonicIntervals, element => element == semitoneInterval))
                    break;
                for (int i = 0; i < PentatonicIntervals.Length; ++i)
                {
                    if (semitoneInterval <= PentatonicIntervals[i])
                    {
                        System.Random rand = new System.Random();
                        if (rand.NextDouble() < 0.5 || i == 0)
                            semitoneInterval = PentatonicIntervals[i];
                        else
                            semitoneInterval = PentatonicIntervals[i - 1];
                    }
                }
                break;
            case SCALE_TYPES.MINOR:
                if (System.Array.Exists(MinorIntervals, element => element == semitoneInterval))
                    break;
                for (int i = 0; i < MinorIntervals.Length; ++i)
                {
                    if (semitoneInterval <= MinorIntervals[i])
                    {
                        System.Random rand = new System.Random();
                        if (rand.NextDouble() < 0.5 || i == 0)
                            semitoneInterval = MinorIntervals[i];
                        else
                            semitoneInterval = MinorIntervals[i - 1];
                    }
                }
                break;
            case SCALE_TYPES.CHROMATIC:
                // keep all notes
                break;
            default:
                break;
        }
        semitoneInterval += 12 * octavesOffset;
        return semitoneInterval;
    }

    // Start is called before the first frame update
    void Start()
    {
        NoodlerManager.instance.noodlers.Add(this);

        if (playOnStart)
        {
            PlayNextNote();
            isPaused = false;
        }
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

    void PlayNextNote()
    {
        musicInstance.release();

        int noteID = rand.Next(0, NUM_NOTES);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Spirits/Hum/Note " + noteID.ToString().PadLeft(2, '0'));
        var test = gameObject.GetComponent<Rigidbody>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(musicInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());

        //FMOD.ATTRIBUTES_3D Attribs = new FMOD.ATTRIBUTES_3D();
        //musicInstance.set3DAttributes();

        musicInstance.setVolume(volume);

        int semitones = rand.Next(minNote, maxNote);
        semitones = ClampToScale(semitones, scaleType);
        musicInstance.setParameterByName("Pitch", SemitoneToPitchVar((float)semitones));

        float tremuloDepth = (float)rand.NextDouble() * (maxTremulo - minTremulo) + minTremulo;
        musicInstance.setParameterByName("Tremulo Depth", tremuloDepth);

        musicInstance.start();
    }

    static float SemitoneToPitchVar(float semitones)
    {
        return semitones / 24;
    }

    public void StartPlaying()
    {
        isPaused = false;
    }
    public void StopPlaying()
    {
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
            return;

        musicInstance.getPlaybackState(out var state);
        if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            PlayNextNote();
        }
    }
}
