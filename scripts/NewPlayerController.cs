using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private float xMove, zMove;
    private float ySpeed;
    public float playerSpeed = 5;
    public float rotationSpeed = 2;
    public float jumpForce = 10.0f;
    public float gravity = 14;
    public CharacterController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");
        

        if (player.isGrounded)
        {
            ySpeed = -gravity * Time.deltaTime;
            if(Input.GetKeyDown("space"))
                ySpeed = jumpForce;
        }

        else
            ySpeed -= gravity * Time.deltaTime;

        Vector3 moveVector = Vector3.zero;
        moveVector.x = xMove * playerSpeed;
        moveVector.y = ySpeed;
        moveVector.z = zMove * playerSpeed;

        player.Move(moveVector * Time.deltaTime);

    }
}
