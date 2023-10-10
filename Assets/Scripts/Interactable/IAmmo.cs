using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmmo : MonoBehaviour, IInteraction
{
    [SerializeField] string ammoID;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        //trigger the 'add ammo event'
        //passing it the ammoID that this has (string)        
        EventManager.addAmmoEvent(ammoID);
        gameObject.SetActive(false);
    }
}
