using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour, IInteraction
{

    //is this a locked door bool
    [SerializeField] bool lockedDoor;
    [SerializeField] bool oneWayDoor;
    
    [SerializeField] string lockID;

    bool open = false;

    private Animator animC;

    //player has key bool
    private bool hasKey;

    private void Awake()
    {
        if(!TryGetComponent<Animator>(out animC))
        {
            Debug.LogError("You need to attach an animator to this object homie");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //subscribe to player has key event
        EventManager.keyAquiredEvent += Unlock;

        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Unlock(string keyID)
    {
        if (keyID == lockID)
        {
            hasKey = true;
        }
    }

    public void Activate()
    {
        if (lockedDoor && hasKey == true)
        {
            //open the door by activating animation or some bullshit idk
            Debug.Log("The door opens");
            if (!open)
            {
                animC.Play("Base Layer.DoorOpen");
                open = true;
            }
            else
            {
                animC.Play("Base Layer.DoorClose");
                open = false;
            }
        }
        else if (!lockedDoor)
        {
            //open the door by activating animation or some bullshit idk
            Debug.Log("The door opens");
            if (!open)
            {
                animC.Play("Base Layer.DoorOpen");
                open = true;
            }
            else
            {
                animC.Play("Base Layer.DoorClose");
                open = false;
            }
        }
        else
        {
            Debug.Log($"You need the {lockID} key");
            //animC.Play("Base Layer.DoorOpen");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerMove>(out PlayerMove pMove))
        {
            if (oneWayDoor)
            {
                hasKey = true;
            }
        }
        
    }

    private void OnDestroy()
    {
        EventManager.keyAquiredEvent -= Unlock;
    }
}
