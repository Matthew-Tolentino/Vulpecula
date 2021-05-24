using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMapIcon : MonoBehaviour
{
    [SerializeField] Transform spiritTarget;

    private Color alpha;

    private void Start() {
        alpha = GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(spiritTarget.position.x, transform.position.y, spiritTarget.position.z);
    }

    private void FixedUpdate() 
    {
        if (!spiritTarget.gameObject.activeSelf) {
            alpha.a = .6f;
        } 
        else {
            alpha.a = 1f;
        }
        GetComponent<Renderer>().material.SetColor("_Color", alpha);
    }
}
