using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMapIcon : MonoBehaviour
{
    [SerializeField] Transform spiritTarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(spiritTarget.position.x, transform.position.y, spiritTarget.position.z);
    }
}
