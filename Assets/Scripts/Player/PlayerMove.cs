using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //player speed variable
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float accelForce = 5f;

    [SerializeField] float decelSpeed = 3f;
    [SerializeField] float maxSpeed = 10f;

    //the acceleration time and force for the player's jump
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpAccelTime = 2f;

    [SerializeField] float fallSpeed;

    private Rigidbody rbody;

    private bool grounded;

    public static PlayerMove Instance;

    Vector3 vel = Vector3.zero;

    [SerializeField] float wallDetect;

    private Vector3 startPos;

    [SerializeField] float zKillLimit;

    private void OnEnable()
    {
        SetStartPos();
    }

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody>(out rbody))
        {
            Debug.Log("Kill yourself");
        }
    }

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Only one PlayerMove in the scene, dummy");
        }

        InvokeRepeating("ZKill", 0.5f, 0.5f);
    }


    void Update()
    {
        //player press W go forward huuurrrrr
        PlayerMoveInput();                          
    }

    private void PlayerMoveInput()
    {        
        Vector3 moveDir = new Vector3();

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveDir += transform.forward.normalized * input.z;
        moveDir += transform.right.normalized * input.x;

        //use raycast to check if the player is trying to move through a solid object then block their movement
        RaycastHit hit;
        if(Physics.Raycast(transform.position, moveDir, out hit, wallDetect, 100, QueryTriggerInteraction.Ignore))
        {
            moveDir = Vector3.zero;
            vel = moveDir;
        }

        if (grounded)
        {
           vel = moveDir;

            if (Input.GetButtonDown("Jump"))
            {
                rbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }

            if (vel.x != 0 || vel.z != 0)
            {
                //rbody.velocity = Vector3.Lerp(rbody.velocity, moveDir * moveSpeed, Time.deltaTime * accelForce);

                transform.position += vel * Time.deltaTime * accelForce;
            }
            
        }
        else
        {
            rbody.velocity = Vector3.Lerp(rbody.velocity, new Vector3(0, fallSpeed, 0), decelSpeed * 0.25f * Time.deltaTime);
            vel = Vector3.Lerp(vel, Vector3.zero, decelSpeed * Time.deltaTime);
            transform.position += vel * Time.deltaTime * accelForce;
        }

    }


    public void SetStartPos()
    {
        startPos = transform.position;
    }

    private void ZKill()
    {
        //Debug.LogError("peak fuck ur feelings jeff");
        if (transform.position.y < zKillLimit)
        {
            //Debug.LogError("mname jeff");
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            grounded = false;
        }
    }

}
