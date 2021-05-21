using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoodlerManager : MonoBehaviour
{
    public static NoodlerManager instance = null;

    public List<SpiritNoodler> noodlers = new List<SpiritNoodler>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
