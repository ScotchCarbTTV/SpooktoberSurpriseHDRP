using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    private Image keyImage;

    [SerializeField] string keyID;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<Image>(out keyImage))
        {
            Debug.LogError("Gotta have an image component attached buddy");
        }

        keyImage.enabled = false;

        EventManager.keyAquiredEvent += ToggleOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //toggle the UI object on
    private void ToggleOn(string _keyID)
    {
        if (_keyID == keyID)
        {
            keyImage.enabled = true;
        }
    }

    private void OnDestroy()
    {
        EventManager.keyAquiredEvent -= ToggleOn;
    }
}
