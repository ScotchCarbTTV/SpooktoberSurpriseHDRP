using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] float interactionDistance;

    private bool canInteract = true;

    int lMask = 1 << 10;
    

    // Start is called before the first frame update
    void Start()
    {
        lMask = ~lMask;
        InvokeRepeating("InteractDisplayToggler", 0.25f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player can interact currently
        if (canInteract)
        {
            //check if they clicked
            if (Input.GetButtonDown("Interact"))
            {
                RaycastHit hit;

                //raycast from camera straight ahead 
                if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, lMask))
                {
                    if(hit.collider.TryGetComponent(out IInteraction interaction))
                    {
                        interaction.Activate();
                    }
                }

                //if it hits something with the IInteraciton then call interaction
            }  
        }
    }

    private void InteractDisplayToggler()
    {
        RaycastHit hit;
        

        //raycast from camera straight ahead 
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, lMask))
        {
            if (hit.collider.TryGetComponent(out IInteraction interaction))
            {
                EventManager.toggleInteractPromptEvent(true);
            }
            else
            {
                EventManager.toggleInteractPromptEvent(false);
            }
        }
        else
        {
            EventManager.toggleInteractPromptEvent(false);
        }
    }

}

public interface IInteraction
{
    void Activate();
   
}
