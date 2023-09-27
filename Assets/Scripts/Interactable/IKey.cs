using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKey : MonoBehaviour, IInteraction
{
    //string for key ID fuckit

    [SerializeField] string keyID;

    public void Activate()
    {
        //send out the 'picked up *this keyID* event'
        EventManager.keyAquiredEvent(keyID);

        //deactivate the attached object

        gameObject.SetActive(false);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
