using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawControl : MonoBehaviour
{
	public int leftWeight;
	public int rightWeight;

	public float rotationMaximum;
	public float speed; 
    // Start is called before the first frame update
    void Start()
    {
        leftWeight = 0;
        rightWeight = 0;
    }

    // Update is called once per frame
    void Update()
    {
		float currentAngle = transform.eulerAngles.x;
		if(currentAngle > 180) currentAngle -= 360;
		
        if (leftWeight > rightWeight && currentAngle < rotationMaximum)
        {
        	//Quaternion f = transform.localRotation;
        	//f.x += speed * Time.deltaTime;
        	//transform.localRotation = f;
            transform.Rotate(speed*Time.deltaTime, 0, 0);
        }
        else if (leftWeight < rightWeight && currentAngle > -1f * rotationMaximum)
        {
        	//Quaternion f = transform.localRotation;
        	//f.x -= speed * Time.deltaTime;
        	//transform.localRotation = f;
            transform.Rotate(-speed*Time.deltaTime, 0, 0);
        }
    }
}
