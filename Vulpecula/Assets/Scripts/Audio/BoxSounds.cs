using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSounds : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float horizontalVelocityTriggerThreshold = 30;
    [SerializeField]
    private float horizontalVelocityFullVolumeThreshold = 25;

    [FMODUnity.EventRef]
    public string eventFN = "";

    private FMOD.Studio.EventInstance moveSoundsInstance;

    [SerializeField]
    private float volumeMultiplier = 1;

    [SerializeField]
    private FMODUnity.StudioEventEmitter externalSound = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVector = new Vector3(1, 0, 1);
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.Scale(horizontalVector);

        if (horizontalVelocity.sqrMagnitude >= horizontalVelocityTriggerThreshold)
        {
            PlayMoveSound();
        }

        if (moveSoundsInstance.isValid())
        {
            float moveVol = Mathf.Clamp(horizontalVelocity.sqrMagnitude / horizontalVelocityFullVolumeThreshold, 0, 1);
            moveSoundsInstance.setVolume(moveVol * volumeMultiplier);
        }
    }

    private void PlayMoveSound()
    {
        if (moveSoundsInstance.isValid())
        {
            moveSoundsInstance.getPlaybackState(out var state);
            if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                return;
        }

        if (externalSound != null)
        {
            if (!externalSound.IsPlaying())
                externalSound.Play();
        }

        moveSoundsInstance = FMODUnity.RuntimeManager.CreateInstance(eventFN);

        moveSoundsInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        moveSoundsInstance.start();
        moveSoundsInstance.release();
    }
}
