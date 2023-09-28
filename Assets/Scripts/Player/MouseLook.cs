using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //public PauseMenu pauseMenu;

    //public variable which is used to adjust sensitivity
    public float sensitivity = 2.5f;
   //public variable which is used to adjust 'drag' of the mouse, ie acceleration/delay
    public float drag = 1.5f;
    //public bool to allow us to turn mouse look on and off
    public bool lookEnabled = true;

    [SerializeField] float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;

    private Transform character; //will store the transform info for the player character
    private Vector2 mouseDir; //will store mouse inputs
    private Vector2 smoothing; //will store calculations for applying smoothing
    private Vector2 result; //will be the final value applied to camera rotation

    
    public bool CursorToggle
    {
        set
        { if(value == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void Awake()
    {
        character = transform.parent; //will grab the parent's function and set the camnera's transform to be the same
        CursorToggle = false;
    }


    void Update()
    {
        if (lookEnabled == true)
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
            character.transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
            transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        }

        /*
        if(PauseMenu.GameIsPaused == true)
        {
         CursorToggle = true;
        }
        else if (PauseMenu.GameIsPaused == false)
        {
        CursorToggle = false;
        
        }*/
    }
}
