using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //creating singleton for this???
    public static EventManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance= this;
        }
        else
        {
            Debug.LogError("There should oly be one EventManager script in the scene!");
        }
    }


    //event for a key being acquired
    public delegate void KeyAquired(string keyID);
    public static KeyAquired keyAquiredEvent;

    //store an AI event
    public delegate void StoreAI(EnemyAI enemyAI);
    public static StoreAI storeAIEvent;

    //retrieve an AI event
    public delegate void RetrieveAI(EnemyAI enemyAI);
    public static RetrieveAI retreiveAIEvent;

    //add self to waypointmanager list event
    public delegate void AddWayPoint(WayPointNode node);
    public static AddWayPoint addWayPointEvent;

    //event 'announce that I exist' evvent
    public delegate void RegisterAI(EnemyAI enemyAI);
    public static RegisterAI registerAI;

    //interaction prompt toggle
    public delegate void ToggleInteractPrompt(bool toggle);
    public static ToggleInteractPrompt toggleInteractPromptEvent;


    //DEBUGHITMARK spawning event
    public delegate void SPAWNDEBUGHITMARKER(Vector3 hit);
    public static SPAWNDEBUGHITMARKER SPAWNDEBUGHITMARKEREVENT;

}
