using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDisplayer : MonoBehaviour, IInteraction
{
    private bool displaying = false;

    [SerializeField] float zoomSpeed;
    [SerializeField] float maxCarryDist;

    [SerializeField] private Transform clueHolder;

    public void Activate()
    {
        if(displaying == false)
        {
            displaying = true;
        }
        else
        {
            displaying = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayClue();
    }

    private void DisplayClue()
    {
        if (displaying)
        {
            transform.position = Vector3.Lerp(transform.position, ClueZoomPos.Instance.transform.position, zoomSpeed * Time.deltaTime);
            transform.LookAt(PlayerMove.Instance.gameObject.transform.position + new Vector3 (0, 1, 0));
            
            if(Vector3.Distance(clueHolder.position, transform.position) > maxCarryDist)
            {
                displaying = false;
            }
        }
        else
        {
            if(transform.position != clueHolder.position)
            {
                transform.position = Vector3.Lerp(transform.position, clueHolder.position, zoomSpeed * Time.deltaTime);
                
            }
            if (transform.rotation != clueHolder.rotation)
            {
                transform.rotation = clueHolder.rotation;
            }
        }

    }
   
}
