using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    //maintain a list of the waypoints that exist
    [SerializeField] private List<WayPointNode> wayPoints = new List<WayPointNode>();

    public static WayPointManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("ONLY ONE WAYPOINTMANAGER IN THE SCENE RETARD");
        }

        //subscribe to the add waypointnode event
        EventManager.addWayPointEvent += AddWayPoint;
    }

    //return a waypoint with the nearest position
    public WayPointNode FetchNearestWayPoint(Vector3 pos)
    {
        //set a local variable for the current lowest distance
        float lowestDist = Mathf.Infinity;

        //local variable for the nearest node
        WayPointNode nearestNode = null;

        //loop through all the waypoints  and check if the distance they are from the position being
        //checked is less than the current lowest difficulty
        for(int i = 0; i < wayPoints.Count; i++)
        {
            //if the distance is lower...
            if(Vector3.Distance(wayPoints[i].transform.position, pos) < lowestDist)
            {
                //update the current lowest distance
                lowestDist = Vector3.Distance(wayPoints[i].transform.position, pos);
                //update the nearest node
                nearestNode = wayPoints[i];
            }
        }

        //return the node that's nearest
        return nearestNode;
    }
    
    
    private void AddWayPoint(WayPointNode node)
    {
        wayPoints.Add(node);
    }

    private void OnDestroy()
    {
        EventManager.addWayPointEvent -= AddWayPoint;
    }
}
