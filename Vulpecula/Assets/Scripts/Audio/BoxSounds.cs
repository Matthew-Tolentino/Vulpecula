using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSounds : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float horizontalVelocityThreshold = 5;

    private FMOD.Studio.EventInstance moveSoundsInstance;

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

        if (horizontalVelocity.sqrMagnitude >= horizontalVelocityThreshold)
        {
            PlayMoveSound();
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

        moveSoundsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Box Dragging");

        moveSoundsInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        moveSoundsInstance.start();
        moveSoundsInstance.release();
    }
}
