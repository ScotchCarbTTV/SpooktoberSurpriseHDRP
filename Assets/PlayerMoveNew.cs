using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveNew : MonoBehaviour
{
    // character controller component reference

    CharacterController cTroller;

    private Vector3 playerVel;

    private bool grounded = true;

    [SerializeField] private float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityVal;

    private void Awake()
    {
        if(!TryGetComponent<CharacterController>(out cTroller))
        {
            Debug.Log("You need a character controller attached here, kill yourself");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        grounded = cTroller.isGrounded;

        if (grounded && playerVel.y < 0)
        {
            playerVel.y = 0f;
        }

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveDir = new Vector3();

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveDir += transform.forward.normalized * input.z;
        moveDir += transform.right.normalized * input.x;
        cTroller.Move(moveDir * Time.deltaTime * speed);        

        if(Input.GetButton("Jump") && grounded)
        {
            playerVel.y += Mathf.Sqrt(jumpHeight * -3f * gravityVal);
        }

        playerVel.y += gravityVal * Time.deltaTime;
        cTroller.Move(playerVel * Time.deltaTime);
    }

}
