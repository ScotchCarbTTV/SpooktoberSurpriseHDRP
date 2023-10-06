using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHealthPack : MonoBehaviour, IInteraction
{
    [SerializeField] int healthGiven;

    public void Activate()
    {
        Debug.Log($"Gained {healthGiven} health... but the code is not implemented fully yet ya dingus");
        //EventManager.gainHealthEvent(healthGiven);
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
