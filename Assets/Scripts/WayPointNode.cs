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
        //scan for the nearby waypoints
        Collider[] neigh = Physics.OverlapSphere(transform.position, checkRadius); 

       //create list of neighbourinos
       foreach(Collider n in neigh)
        {
            if(n.TryGetComponent<WayPointNode>(out WayPointNode node))
            {
                neighbours.Add(node);
            }
        }
    }

    private void Start()
    {
        //deactivate the collider
        scollider.enabled = false;

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

        WayPointNode node = neighbours[randomInt];
        return node;
    }

}
