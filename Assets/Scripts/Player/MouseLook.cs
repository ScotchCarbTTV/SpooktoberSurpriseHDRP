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
            mouseDir = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity, Input.GetAxisRaw("Mouse Y") * sensitivity); //will take the mouse directions of x and y and store them in the vector 2
                                                                                                                          //then it multiplies the input by the sensitivity.

            //calculate the smoothing
            smoothing = Vector2.Lerp(smoothing, mouseDir, 1 / drag); //lerp is linear interpolation - will go from Vector A to Vector B within Vector T
            result += smoothing; // will store smoothing as result
            result.y = Mathf.Clamp(result.y, -80, 80); //will prevent the player from looking too far up or down and inverting themselves (disorienting)

            transform.localRotation = Quaternion.AngleAxis(-result.y, Vector3.right);
            character.rotation = Quaternion.AngleAxis(result.x, character.transform.up);
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
