// Code based on Brackeys: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=783s
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Makes DialogueManager into a singleton
    public static DialogueManager instance;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;

    public Animator animator;

    public bool isOpen = false;

    private Queue<string> sentences;

    public bool sentenceIsDone = true;
    private bool finishSentence = false;

    // Make sure there is only 1 DialogueManager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // TODO: Implement overall input manager
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        DisplayNextSentence();
    //    }
    //}

    public void StartDialogue(Dialogue dialogue)
    {
        isOpen = true;
        animator.SetBool("isOpen", true);

        // Set mouse state to canvas so you can click next button
        GameManager.mouseState = GameManager.MouseState.canvas;

        nameTxt.SetText(dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentenceIsDone)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            sentenceIsDone = false;
            string sentence = sentences.Dequeue();

            // Problem can happen if other coroutines are running
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    public void FinishSentence()
    {
        finishSentence = true;
    }

    IEnumerator TypeSentence (string sentence)
    {
        string txt = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (finishSentence) break;

            txt += letter;
            dialogueTxt.SetText(txt);
            yield return null;
        }

        dialogueTxt.SetText(sentence);
        finishSentence = false;
        sentenceIsDone = true;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        isOpen = false;

        GameManager.mouseState = GameManager.MouseState.game;
    }
}
