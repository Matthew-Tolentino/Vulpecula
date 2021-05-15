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

    void Start()
    {
    	GameManager.instance.setMouseLock(false);
    }

    public void nextImage()
    {	
    	if (NextIndex >= slides.Length){
    		StartCoroutine(GameManager.instance.NextScene());
        	GameManager.instance.setMouseLock(true);
    	}
    	else image.sprite = slides[NextIndex++];
    }

    public void skip(){
        StartCoroutine(GameManager.instance.NextScene());
        GameManager.instance.setMouseLock(true);
    }
}
