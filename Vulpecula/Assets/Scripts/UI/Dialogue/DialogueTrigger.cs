using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Control the number of times this dialogue hint can be triggered.")]
    public int repeat = 1;
    public Dialogue dialogue;

    // Track how many times dialogue has triggered
    private int tracker = 0;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    public void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Player") && tracker < repeat)
        {
            TriggerDialogue();
            tracker++;
        }
    }
}
