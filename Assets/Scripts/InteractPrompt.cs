using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] GameObject interactPrompt;

    // Start is called before the first frame update
    void Start()
    {
        //subscribe to the interact prompt thing
        EventManager.toggleInteractPromptEvent += ToggleInteractDisplay;
    }

    private void ToggleInteractDisplay(bool toggle)
    {
        interactPrompt.SetActive(toggle);
    }

    private void OnDestroy()
    {
        //unsubsc
        EventManager.toggleInteractPromptEvent -= ToggleInteractDisplay;
    }
}
