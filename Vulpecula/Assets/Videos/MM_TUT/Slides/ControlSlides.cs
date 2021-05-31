using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlSlides : MonoBehaviour
{
    public Image image;
    public Sprite[] slides;

    private int NextIndex = 0;
    public bool endGame;

    void Start()
    {
    	GameManager.instance.setMouseLock(false);
    }

    public void nextImage()
    {	
    	if (NextIndex >= slides.Length){
            if (endGame) { 
                SceneManager.LoadScene(0);
            }
    		else 
            {
                StartCoroutine(GameManager.instance.NextScene());
            	GameManager.instance.setMouseLock(true);
            }
    	}
    	else image.sprite = slides[NextIndex++];
    }

    public void skip(){
        if (endGame) { 
            SceneManager.LoadScene(0);
        }
        else 
        {
            GameManager.instance.StopFadeOut();
            StartCoroutine(GameManager.instance.NextScene());
            GameManager.instance.setMouseLock(true);
        }
    }
}
