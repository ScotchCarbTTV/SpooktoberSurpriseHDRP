using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueZoomPos : MonoBehaviour
{
    public static ClueZoomPos Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Can only have 1 clue zoom pos in the scene bruh");
        }
    }
}
