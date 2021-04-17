using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JumpMechanic : MonoBehaviour
{

    private CharacterController con;

    private float dirY;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float jumpSpeed = 2.5f;

    private void Start()
    {
        con = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = new Vector3(0, 0, 0);
        if (con.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                dirY = jumpSpeed;
            }
        }

        dirY -= gravity * Time.deltaTime;

        direction.y = dirY;

        con.Move(direction * moveSpeed * Time.deltaTime);
        


        
    }

}
