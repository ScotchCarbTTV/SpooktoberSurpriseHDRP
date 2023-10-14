using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointNode : MonoBehaviour
{
    [SerializeField] float checkRadius;

    //rigidbody variable
    private Rigidbody rbody;

    //spherecollider variable
    private SphereCollider scollider;

    //list of neighbourinos
    [SerializeField] private List<WayPointNode> neighbours = new List<WayPointNode>();


    private void OnEnable()
    {
        //grab a reference to the rigidbody
        if(!TryGetComponent<Rigidbody>(out rbody))
        {
            Debug.LogError("Need an rbody attached little bro");
        }

        //grab a reference to the spherecollider
        if(!TryGetComponent<SphereCollider>(out scollider))
        {
            Debug.LogError("Need a sphere collider attached little bro");
        }

    }

    private void Awake()
    {      
        //FindNeighbours();
    }

    private void Start()
    {
        InvokeRepeating("FindNeighbours", 2.1f, 5f);

        //deactivate the collider
        //scollider.enabled = false;

        //invoke the 'add self to waypointmanager list' event
        Invoke("RegisterWayPoint", 2f);

    }

    private void RegisterWayPoint()
    {
        EventManager.addWayPointEvent(this);
    }

    public WayPointNode GetRandomNeighbour()
    {
        int randomInt = Random.Range(0, neighbours.Count);

        if (randomInt < neighbours.Count)
        {
            WayPointNode node = neighbours[randomInt];
            return node;
        }
        else return null;
    }

    public void FindNeighbours()
    {
        //scan for the nearby waypoints
        List<WayPointNode> neigh = WayPointManager.Instance.GetWayPointNodes();       

        //create list of neighbourinos
        foreach (WayPointNode n in neigh)
        {
                RaycastHit hit;
                Vector3 dir = n.transform.position - transform.position;

            int layerMask = 1 << 13;
            layerMask = ~layerMask;

                if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.TryGetComponent<WayPointNode>(out WayPointNode node))
                    {
                        if (!neighbours.Contains(node))
                        {
                            neighbours.Add(node);
                        }
                    }
                }
            
        }
    }

}
