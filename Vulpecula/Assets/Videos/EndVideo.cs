using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndVideo : MonoBehaviour
{
    public Image image;
    public Sprite lastSlide;

    private bool once = true;
    public GameObject picture;
    public GameObject Next;
    public GameObject Skip;

    public GameObject newSkip;
    public GameObject returnToMenu;

    void Awake()
    {
        gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
        picture.SetActive(false);
        newSkip.SetActive(false);
        returnToMenu.SetActive(false);
    }


    void Update()
    {
        var vd = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        if (once){
            once = false;
            vd.loopPointReached += EndReached;
        }
    }

    public void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        newSkip.SetActive(false);
        image.sprite = lastSlide;
        returnToMenu.SetActive(true);

        picture.SetActive(false);
        //SceneManager.LoadScene(0);
    }

    public void startVideo(){
        Next.SetActive(false);
        Skip.SetActive(false);
        newSkip.SetActive(true);
        picture.SetActive(true);
        gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
    }


}
