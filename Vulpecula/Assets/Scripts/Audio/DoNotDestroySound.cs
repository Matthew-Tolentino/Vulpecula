using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroySound : MonoBehaviour
{
    public enum eSoundTag
    {
        NONE = 0,
        MUSIC,
    }

    [SerializeField]
    public eSoundTag soundTag = eSoundTag.NONE;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public static List<DoNotDestroySound> FindAllDoNotDestroySoundsByTag(eSoundTag tag)
    {
        List<DoNotDestroySound> returnList = new List<DoNotDestroySound>();
        foreach (var sound in FindObjectsOfType<DoNotDestroySound>())
        {
            if (sound.soundTag == tag)
                returnList.Add(sound);
        }
        return returnList;
    }
}
